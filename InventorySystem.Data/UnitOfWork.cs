using System.Linq;
using System.Threading.Tasks;
using InventorySystem.Core;
using InventorySystem.Core.Repositories;
using InventorySystem.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;
        private UserRepository _userRepository;
        private ProductDetailsRepository _productDetailsRepository;
        private ProductRepository _productRepository;

        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);
        public IProductDetailsRepository ProductsDetails => _productDetailsRepository = _productDetailsRepository ?? new ProductDetailsRepository(_context);
        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);


        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;
            var test = _context.ProductDetails.AsNoTracking().Include(x => x.Products).ToList();
            var test2 = _context.ProductDetails.ToList();
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}