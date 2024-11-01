using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTERFACES
{
    public interface IProductRepository
    {
        IEnumerable<IProduct> GetAllProducts();
        IProduct GetProductById(int id);
        void Add(IProduct product);
        void Update(IProduct product);
        void DeleteProduct(int id);
    }
}
