using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FunCart.Controllers
{
   
    [Route("api/v1")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;

        }

        /// <summary>
        /// LogIn Controller
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
    
        [HttpPost("GetToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
         
            var response=await _userService.LogIn(loginRequest);
            if (response.IsSuccess)
                return Ok(response);
            return NotFound(response);

        }
     
    }


}
