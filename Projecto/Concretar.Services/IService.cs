using System.Collections.Generic;

namespace Concretar.Services
{
    public interface IService<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Create(T model);
        void Edit(T model);
        void Delete(int id);
    }
}
