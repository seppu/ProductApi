using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.BusinessLayer.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger<BaseService> Logger;
        public BaseService(IMapper mapper, ILogger<BaseService> logger)
        {
            Mapper = mapper;
            Logger = logger;
        }
    }
}
