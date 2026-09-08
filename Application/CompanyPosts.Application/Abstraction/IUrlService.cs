using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPost.Application.Abstraction
{
    public interface IUrlService
    {
        string GetProductionUrl();
        string GetLocalUrl();
    }
}
