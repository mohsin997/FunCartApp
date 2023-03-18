using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<T>> InvokeQuery<T>(string query, object param, bool isStoreProcedure = false) where T : class;
        Task<IEnumerable<T>> InvokeQuery<T>(string query, bool isStoreProcedure = false) where T : class;

        Task<bool> InvokeExecute(string query, object param, bool isStoreProcedure = false);
        Task<Guid> InvokeExecuteQuery(string query, object param, bool isStoreProcedure = false);


    }
}
