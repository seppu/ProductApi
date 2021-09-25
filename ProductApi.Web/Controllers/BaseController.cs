using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductApi.Web.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly ILogger Logger;

        public BaseController(ILogger logger)
        {
            Logger = logger;
        }
        protected void ThrowIfModelStateIsInvalid()
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException(ModelState, "Invalid request");
            }
        }
    }
}
