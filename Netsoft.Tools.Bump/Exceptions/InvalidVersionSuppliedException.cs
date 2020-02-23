using System;
using System.Collections.Generic;
using System.Text;

namespace Netsoft.Tools.Bump.Exceptions
{
    public class InvalidVersionSuppliedException : Exception
    {
        public InvalidVersionSuppliedException(string message) : base(message)
        {

        }
    }
}
