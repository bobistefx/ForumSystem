using AutomotiveForumSystem.Data;

namespace AutomotiveForumSystem.Repositories.Contracts
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
