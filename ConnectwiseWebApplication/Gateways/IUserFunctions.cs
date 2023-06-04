using ConnectwiseWebApplication.Models;

namespace ConnectwiseWebApplication.Gateways
{
    public interface IUserFunctions
    {
        public Task<int> ValidateUser(User user, HttpContext httpContext);
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUserById(int? id);
        public Task<int> CreateUser(User user,HttpContext httpContext);
		public Task<bool> DeleteUser(int userId);
		public Task<IEnumerable<Transaction>> GetUserTransactions(HttpContext httpContext);

        public Task<bool> UpdateUser(int userId, User user);
        public Task<bool> UpdateUserPassword(int userId, string password);
    }
	}


