using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    public interface IRestDataReader<out T> where T : class
    {
        T Get();
    }
}
