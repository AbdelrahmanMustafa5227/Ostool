using Ostool.Application.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Exceptions
{
    public class ValidationFailureException : Exception
    {
        public List<ValidationError> ValidationFailures { get; set; }

        public ValidationFailureException(
           List<ValidationError> validationFailures,
            string message = "One or more validation errors has occured") : base(message)
        {
            ValidationFailures = validationFailures;
        }
    }
}