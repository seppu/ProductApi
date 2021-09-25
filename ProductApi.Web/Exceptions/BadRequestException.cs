using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Web.Exceptions
{
    public class BadRequestException : Exception
    {
        public ModelStateDictionary ModelState { get; set; }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(ModelStateDictionary modelState, string message) : base(message)
        {
            ModelState = modelState;
        }

        public string ToErrorDetails() =>
            ModelState != null
                ? $"{Message}{Environment.NewLine}{string.Join(Environment.NewLine, ModelState.Values.Select(x => string.Join(Environment.NewLine, x.Errors.Select(e => e.ErrorMessage))))}"
                : Message;
    }
}
