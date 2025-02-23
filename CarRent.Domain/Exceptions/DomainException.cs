using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Domain.Exceptions
{
    [ExcludeFromCodeCoverage(Justification = "Custom exception without additional logic")]
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
