using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Author> FindAuthorByName(string name)
        {
            return GetQueryable().Where(x => x.Name.Equals(name)).FirstOrDefaultAsync();
        }
    }
}
