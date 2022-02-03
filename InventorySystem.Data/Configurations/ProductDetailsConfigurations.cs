using InventorySystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Configurations
{
    public class ProductDetailsConfigurations : IEntityTypeConfiguration<ProductDetails>
    {
        public void Configure(EntityTypeBuilder<ProductDetails> builder)
        {
            //builder.Navigation(pd => pd.ProductCategory).AutoInclude();
            //builder.Navigation(pd => pd.Owner).AutoInclude();
        }
    }
}
