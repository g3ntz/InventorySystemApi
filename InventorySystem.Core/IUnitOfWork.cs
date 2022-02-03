using System;
using System.Threading.Tasks;
using InventorySystem.Core.Repositories;

namespace InventorySystem.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProductDetailsRepository ProductsDetails { get; }
        IProductRepository Products { get; }
        bool Commit();
    }
}