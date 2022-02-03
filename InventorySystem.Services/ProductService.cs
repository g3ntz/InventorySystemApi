using InventorySystem.Core;
using InventorySystem.Core.Models;
using InventorySystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Product Create(Product model)
        {
            _unitOfWork.Products.Add(model);
            _unitOfWork.Commit();

            var newProduct = _unitOfWork.Products.GetById(model.Id);
            return newProduct;
        }

        public void Delete(Product model)
        {
            var existingProductDetails = _unitOfWork.ProductsDetails.GetById(model.ProductDetailsId);

            if(model.ProductStatusId == 1)
            {
                existingProductDetails.Stock -= 1;
                _unitOfWork.Products.Remove(model);
                _unitOfWork.Commit();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            _unitOfWork.Products.Include(x => x.ProductStatus, x => x.ProductDetails);
            return _unitOfWork.Products.GetAll();
        }

        public Product GetById(int id)
        {
            var test2 = _unitOfWork.Products.Include(new string[] {"ProductDetails"});
            return _unitOfWork.Products.GetById(id);
        }

        public void Update(int id, Product model)
        {
            var existingProduct = _unitOfWork.Products.GetById(model.Id);
            existingProduct.ProductDetailsId = model.ProductDetailsId;
            existingProduct.ProductStatusId = model.ProductStatusId;
            _unitOfWork.Commit();
        }
    }
}
