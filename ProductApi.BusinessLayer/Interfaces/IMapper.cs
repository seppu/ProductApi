using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.BusinessLayer.Interfaces
{
    public interface IProductMapper
    {
        T Map<T>(object obj);
        TDest Map<TSource, TDest>(TSource src, TDest dest);
        TDest Map<TSource, TDest>(TSource src);
    }
}
