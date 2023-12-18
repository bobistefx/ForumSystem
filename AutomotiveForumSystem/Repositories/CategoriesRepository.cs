using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Repositories.Contracts;

namespace AutomotiveForumSystem.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationContext applicationContext;
        public CategoriesRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
    }
}
