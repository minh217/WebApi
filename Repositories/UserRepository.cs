using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;
namespace WebApi.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context){
            _context = context;
        }
        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();
            return user;

        }
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(e => e.Email == email);
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(e => e.Id == id);
        }
    }
}