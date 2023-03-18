using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ProductService:IProductService
    {
        private readonly IRepository _repository;
        public ProductService(IRepository repository)
        {
            _repository=repository; 
        }


        /// <summary>
        /// ProductRegister Service
        /// </summary>
        /// <param name="productRegister"></param>
        /// <returns></returns>
        public async Task<ServiceResult<bool>> ProductRegister(ProductRegister productRegister,Guid userId)
        {
            try
            {
                var sql = "INSERT INTO dbo.Product (NAME,Description,Price,CreatedBy,IsDeleted) VALUES(@Name,@Description,@Price,@userId,0)";
                await _repository.InvokeExecute(sql, new { productRegister.Name, productRegister.Description, productRegister.Price, userId });

                return new ServiceResult<bool>(true, "Product Registered");

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, ex.Message);

            }
        }

        /// <summary>
        /// GetProduct Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<ProductRegister>>> GetProduct(Guid id)
        {
            try
            {
                var checkSql = "SELECT Name,Description,Price FROM dbo.Product WHERE ProductId=@id";
                var productDetails = await _repository.InvokeQuery<ProductRegister>(checkSql, new { id });
                if (productDetails.Count() > 0)
                {
                    return new ServiceResult<IEnumerable<ProductRegister>>(productDetails, $"{productDetails.Count()} record(s) found");
                }
                else
                {
                    return new ServiceResult<IEnumerable<ProductRegister>>(null, "No record found", true);

                }

            }

            catch (Exception ex)
            {
                return new ServiceResult<IEnumerable<ProductRegister>>(ex, ex.Message);
            }
        }
        /// <summary>
        /// Delete Product Service
        /// </summary>
        /// <param name="id"></param>
        ///  /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ServiceResult<bool>> DeleteProduct(Guid id,Guid userId)
        {
            try
            {
                var sql = "Update  dbo.Product  set IsDeleted = 1 , ModifiedBy= @userId where ProductId=@id";
                await _repository.InvokeExecute(sql, new { userId, id });

                return new ServiceResult<bool>(true, "Product Deleted");

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, ex.Message);

            }
        }
        /// <summary>
        /// GetAllProducts Service
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<ProductRegister>>> GetAllProducts(int pageNumber,int pageSize)
        {
            try
            {
                var spName = "USP_GetProducts";
                var productDetails = await _repository.InvokeQuery<ProductRegister>(spName, new { pageNumber, pageSize },true);
                if (productDetails.Count() > 0)
                {
                    return new ServiceResult<IEnumerable<ProductRegister>>(productDetails, $"{productDetails.Count()} record(s) found");
                }
                else
                {
                    return new ServiceResult<IEnumerable<ProductRegister>>(null, "No record found", true);

                }

            }

            catch (Exception ex)
            {
                return new ServiceResult<IEnumerable<ProductRegister>>(ex, ex.Message);
            }
        }
        /// <summary>
        /// UpdateProduct  Service
        /// </summary>
        /// <param name="updateProduct"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ServiceResult<bool>> UpdateProduct(UpdateProduct updateProduct,Guid userId)
        {
            try
            {
                var spName = "USP_UpdateProduct";
              var result=  await _repository.InvokeExecute(spName, new { updateProduct.ProductId, updateProduct.Name, updateProduct.Description, updateProduct.Price, userId },true);
                if(result)
                {
                    return new ServiceResult<bool>(true, "Product Updated");
                }
                else
                {
                    return new ServiceResult<bool>(false, "Product Not Found",true);
                }
            



            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(ex, ex.Message);

            }
        }
    }
}
