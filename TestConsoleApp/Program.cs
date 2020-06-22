using System;
using System.Collections.Generic;
using IdGen;

namespace TestConsoleApp
{
    class Program
    {
        static List<long> list1 = new List<long>();
        static HashSet<long> set = new HashSet<long>();


        static void Main(string[] args)
        {
            Console.WriteLine("part 1 default ID gen");
            var generator = new IdGenerator(0);// 41, 10 , 12
            for (int i = 0; i < 100; i++)
                list1.Add(generator.CreateId());
            for (int i = 0; i < 100; i++)
                Console.WriteLine(list1[i]);

            Console.WriteLine("part 2 timespan");
            for (int i = 0; i < 10000; i++)
            {
                if (!set.Add(DateTime.Now.Ticks))
                    throw new Exception("Duplicate detected");// why no duplicates
            }

            foreach (var l in set)
                Console.WriteLine(l);


            Console.WriteLine("part 3 B4P ID gen");
            list1 = new List<long>();
            var epoch = new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            // Create a mask configuration of 49 bits for timestamp (unit milliseconds, 17.4 years runtime),
            // 11 for generator-id (2048 generators) and 3 for sequence (8 ids per millisecond)
            var mc = new MaskConfig(49, 7, 7);
            var b4PGenerator = new IdGenerator(2, epoch, mc);
            for (int i = 0; i < 100; i++)
                list1.Add(b4PGenerator.CreateId());
            for (int i = 0; i < 100; i++)
                Console.WriteLine(list1[i]);

            Console.ReadLine();


        }
    }
}
