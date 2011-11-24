using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetFlow.Features.Infrastructure
{
    internal static class ApplicationUrl
    {
        public static Uri HomePage()
        {
            return new Uri("http://localhost:4000/");
        }
    }
}
