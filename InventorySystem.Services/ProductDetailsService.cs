using AutoMapper;
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
    public class ProductDetailsService : IProductDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProductDetails Create(ProductDetails productDetails)
        {
            _unitOfWork.ProductsDetails.Add(productDetails);
            var success = _unitOfWork.Commit();

            if (success)
            {
                var products = new List<Product>();
                for (int i = 0; i < productDetails.Stock; i++)
                {
                    products.Add(
                        new Product()
                        {
                            ProductDetailsId = productDetails.Id,
                            ProductStatusId = 1 // Idle
                        }
                    );
                }
                _unitOfWork.Products.AddRange(products);
                _unitOfWork.Commit();
            }

            var newProductDetails = _unitOfWork.ProductsDetails.GetById(productDetails.Id);
            return newProductDetails;
        }

        public void Delete(ProductDetails model)
        {
            _unitOfWork.ProductsDetails.Remove(model);
            _unitOfWork.Commit();
        }

        public IEnumerable<ProductDetails> GetAll()
        {
            return _unitOfWork.ProductsDetails.Include(x => x.ProductCategory, x => x.Owner).GetAll();
        }

        public ProductDetails GetById(int id)
        {
            _unitOfWork.ProductsDetails.GetById(id);
            return _unitOfWork.ProductsDetails.IncludeAll().GetById(id);
        }

        public void Update(int id, ProductDetails model)
        {
            var existingProductDetails = _unitOfWork.ProductsDetails.GetById(id);

            var stockDifference = model.Stock - existingProductDetails.Stock;

            existingProductDetails.Stock = model.Stock;
            existingProductDetails.Name = model.Name;
            existingProductDetails.Description = model.Description;
            existingProductDetails.DateCreated = model.DateCreated;
            existingProductDetails.OwnerId = model.OwnerId;
            existingProductDetails.Price = model.Price;
            existingProductDetails.ProductCategoryId = model.ProductCategoryId;

            if(stockDifference > 0)
            {
                var products = new List<Product>();
                for (int i = 0; i < stockDifference; i++)
                {
                    products.Add(
                        new Product()
                        {
                            ProductDetailsId = model.Id,
                            ProductStatusId = 1 // Idle
                        }
                    );
                }
                _unitOfWork.Products.AddRange(products);
            }

            _unitOfWork.Commit();
        }
    }
}
