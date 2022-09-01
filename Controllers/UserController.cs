using APICODERHOUSE.Controllers.DTOs;
using APICODERHOUSE.Models;
using APICODERHOUSE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace APICODERHOUSE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public static bool isUserLoggedIn = false;
        [HttpGet("UserData")]
        public User GetUserData(string name)
        {
            return UserHandler.GetUserData(name);
        }
        [HttpDelete]
        public bool DeleteUser([FromBody] int id)
        {
            if (isUserLoggedIn == true)
            {
                try
                {
                    return UserHandler.DeleteUser(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        [HttpPut]
        public bool UpdateUser([FromBody] PutUser user)
        {
            if (isUserLoggedIn == true)
            {
                try
                {
                    return UserHandler.UpdateUser(new User
                    {
                        id = TokenHandler.userToken,
                        name = user.name,
                        surname = user.surname,
                        userName = user.userName,
                        password = user.password,
                        email = user.email
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public bool CreateUser([FromBody] PostUser user)
        {
            try
            {
                return UserHandler.CreateUser(new User
                {
                    name = user.name,
                    surname = user.surname,
                    userName = user.userName,
                    password = user.password,
                    email = user.email
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpGet("Login")]
        public bool LoginMethod(string username, string password) 
        {
            try
            {
                User user = UserHandler.LoginMethod(username, password);
                if (user.name == null)
                {
                    return false;
                }
                else
                {
                    TokenHandler.UpdateUserToken(user.id);
                    isUserLoggedIn = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
