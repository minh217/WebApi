using WebApi.Models;
namespace WebApi.Interfaces
{
    public interface IUserRepository
    {
        User Create(User User);
        User GetUserByEmail(string email);
    }
}