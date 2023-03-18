using BusinessLogicLayer.Common;
using BusinessLogicLayer.Models;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {

        Task<ServiceResult<string>> LogIn(LoginRequest loginRequest);

    }
}
