using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.Extensions.Configuration;
using DataLayer.Interfaces;
using System;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly string _key;

        public UserService(IRepository repository, IConfiguration configuration)
        {

            _repository = repository;
            _key = configuration["JWTKey"];

        }
      
        /// <summary>
        /// LogIn Service
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> LogIn(LoginRequest loginRequest)
        {
            try
            {
                var checkSql = "SELECT UserId FROM dbo.AppUser WHERE Email=@Email and Password=@Password";
                var userId = await _repository.InvokeExecuteQuery(checkSql, new { loginRequest.Email, loginRequest.Password });
                if (userId !=Guid.Empty)
                {
                    return new ServiceResult<string>($"Key: Bearer { JwtManager.JwtGenerator(userId, key: _key)}", "Login success");
                }
                else
                {
                    return new ServiceResult<string>(null, "Invalid credentials", true);

                }

            }

            catch (Exception ex)
            {
                return new ServiceResult<string>(ex, ex.Message);
            }
        }

    }
}
