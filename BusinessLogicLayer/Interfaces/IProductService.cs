using BusinessLogicLayer.Common;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResult<bool>> ProductRegister(ProductRegister productRegister, Guid userId);
        Task<ServiceResult<IEnumerable<ProductRegister>>> GetProduct(Guid id);
        Task<ServiceResult<bool>> DeleteProduct(Guid id, Guid userId);
        Task<ServiceResult<IEnumerable<ProductRegister>>> GetAllProducts(int pageNumber, int pageSize);
        Task<ServiceResult<bool>> UpdateProduct(UpdateProduct updateProduct, Guid userId);
    }
}
