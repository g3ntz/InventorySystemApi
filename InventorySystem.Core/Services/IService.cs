using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Services
{
    public interface IService<Model> where Model : class
    {
        IEnumerable<Model> GetAll();
        Model GetById(int id);
        Model Create(Model model);
        void Update(int id, Model model);
        void Delete(Model model);
    }
}
