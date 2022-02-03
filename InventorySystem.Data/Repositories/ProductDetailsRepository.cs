using InventorySystem.Core.Models;
using InventorySystem.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Repositories
{
    public class ProductDetailsRepository : Repository<ProductDetails>, IProductDetailsRepository
    {
        public InventoryDbContext InventoryDbContext
        {
            get { return Context as InventoryDbContext; }
        }

        public ProductDetailsRepository(InventoryDbContext context)
            : base(context)
        { }

        public override IRepository<ProductDetails> IncludeAll()
        {
            return base.Include(x => x.ProductCategory, x => x.Owner).Include(new[] { "Products.ProductStatus" });
        }
    }
}
