using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPost.Application.Exceptions
{
    public class ExpiredTokenException:Exception
    {
        public ExpiredTokenException(string message):base(message)
        {
            
        }
    }
}
