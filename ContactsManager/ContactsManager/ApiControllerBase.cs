
namespace ContactsManager
{
    public class ApiControllerBase
    {
        protected IRepository Repo { get; private set; }

        protected ApiControllerBase()
        {
            Repo = new Repository();
        }

    }
}
