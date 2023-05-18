using Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        public Task<Author> FindAuthorByName(string name);
    }
}
