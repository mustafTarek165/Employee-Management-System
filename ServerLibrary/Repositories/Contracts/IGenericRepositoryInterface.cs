using BaseLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IGenericRepositoryInterface<T>
    {
       public Task<List<T>> GetAll();
       public Task<T?> GetById(int id);
        public Task<GeneralResponse> Insert(T item);
        public Task<GeneralResponse> Update(T item);
        public Task<GeneralResponse> DeleteById(int id);
    }
}
