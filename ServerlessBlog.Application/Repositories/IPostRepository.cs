using System.Text;
using System.Threading.Tasks;
using ServerlessBlog.Application.Models;
using ServerlessBlog.Application.Models.Documents;
using ServerlessBlog.Commands;

namespace ServerlessBlog.Application.Repositories
{
    public interface IPostRepository
    {
        Task Add(PostDocument postDocument);
    }
}
