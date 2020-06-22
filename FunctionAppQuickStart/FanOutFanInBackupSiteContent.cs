using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Tutorials.DurableFunctions
{

    //Scenario overview
    //In this sample, the functions upload all files under a specified directory recursively into blob storage. They also count the total number of bytes that were uploaded.
    //It's possible to write a single function that takes care of everything. The main problem you would run into is scalability. A single function execution can only run on a single virtual machine, so the throughput will be limited by the throughput of that single VM. Another problem is reliability. If there's a failure midway through, or if the entire process takes more than 5 minutes, the backup could fail in a partially completed state. It would then need to be restarted.
    //A more robust approach would be to write two regular functions: one would enumerate the files and add the file names to a queue, and another would read from the queue and upload the files to blob storage. This approach is better in terms of throughput and reliability, but it requires you to provision and manage a queue. More importantly, significant complexity is introduced in terms of state management and coordination if you want to do anything more, like report the total number of bytes uploaded.
    public static class FanOutFanInBackupSiteContent
    {

        [FunctionName("E2_BackupSiteContent")]
        public static async Task<long> Run([OrchestrationTrigger] IDurableOrchestrationContext backupContext)
        {
            string rootDirectory = backupContext.GetInput<string>()?.Trim();
            if (string.IsNullOrEmpty(rootDirectory))
            {
                rootDirectory = Directory.GetParent(typeof(FanOutFanInBackupSiteContent).Assembly.Location).FullName;
            }

            string[] files = await backupContext.CallActivityAsync<string[]>("E2_GetFileList", rootDirectory);

            var tasks = new Task<long>[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                tasks[i] = backupContext.CallActivityAsync<long>("E2_CopyFileToBlob", files[i]);
            }

            await Task.WhenAll(tasks);

            long totalBytes = tasks.Sum(t => t.Result);
            return totalBytes;
        }

        [FunctionName("E2_GetFileList")]
        public static string[] GetFileList([ActivityTrigger] string rootDirectory, ILogger log)
        {
            log.LogInformation($"Searching for files under '{rootDirectory}'...");
            string[] files = Directory.GetFiles(rootDirectory, "*", SearchOption.AllDirectories);
            log.LogInformation($"Found {files.Length} file(s) under {rootDirectory}.");

            return files;
        }

        [FunctionName("E2_CopyFileToBlob")]
        public static async Task<long> CopyFileToBlob([ActivityTrigger] string filePath, Binder binder, ILogger log)
        {
            long byteCount = new FileInfo(filePath).Length;

            // strip the drive letter prefix and convert to forward slashes
            string blobPath = filePath.Substring(Path.GetPathRoot(filePath).Length).Replace('\\', '/');
            string outputLocation = $"backups/{blobPath}";

            log.LogInformation($"Copying '{filePath}' to '{outputLocation}'. Total bytes = {byteCount}.");

            // copy the file contents into a blob
            using (Stream source = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream destination = await binder.BindAsync<CloudBlobStream>(new BlobAttribute(outputLocation, FileAccess.Write)))
            {
                await source.CopyToAsync(destination);
            }

            return byteCount;
        }


    }
}
