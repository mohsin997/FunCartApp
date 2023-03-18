using FunCart.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FunCart.Controllers
{
   
    [ApiController]
    public class BaseController : ControllerBase
    {
      /// <summary>
      /// Getting UserId From Jwt
      /// </summary>
        readonly IHttpContextAccessor _contextAccessor;
        private Guid? _userId { get; set; }
        public BaseController(IHttpContextAccessor ca)
        {
            _contextAccessor = ca;
        }

        public Guid UserId
        {
            get
            {
                if (!_userId.HasValue)
                {
                    _userId = ExtractUserId();
                }
                return _userId.Value;
            }

            set { _userId = value; }
        }
        private Guid ExtractUserId()
        {
            var userId = new Guid(_contextAccessor.HttpContext.User.FindFirst("userId").Value);
            if (userId != Guid.Empty)
                return userId;
            else
                throw new BadRequestException("UserId is missing");
        }
    }
}
