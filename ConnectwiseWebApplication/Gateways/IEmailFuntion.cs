using ConnectwiseWebApplication.Models;

namespace ConnectwiseWebApplication.Controllers
{
    public interface IEmailFuntion
    {

        public  Task<MasterEmail> GetEmailById(int id);
    }
}
