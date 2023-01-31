using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    /// <summary>
    /// Interface for API response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityResponse<T>
    {
        T Data { get; set; }
        Meta Meta { get; set; }
    }

    public interface IEntitiesResponse<T>
    {
        List<T> Data { get; set; }
        Meta Meta { get; set; }
    }

}
