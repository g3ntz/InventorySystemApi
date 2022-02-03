using InventorySystem.Core.Models;
using InventorySystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private InventoryDbContext InventoryDbContext
        {
            get { return Context as InventoryDbContext; }
        }

        public ProductRepository(InventoryDbContext context)
            : base(context)
        { }

        public override IRepository<Product> IncludeAll()
        {
            throw new NotImplementedException();
        }
    }
}
