using Dapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataLayer.Service
{
    public class RepositoryService : IRepository
    {
        private readonly ConnectionString _options;

        public RepositoryService(IOptions<ConnectionString> options)
        {
            _options = options.Value;


        }
        public async Task<IEnumerable<T>> InvokeQuery<T>(string query, object param, bool isStoreProcedure = false) where T : class
        {
            using (IDbConnection _connection = new SqlConnection(_options.FunCart))
            {

                if (isStoreProcedure)
                    return await _connection.QueryAsync<T>(query, param, null, null, CommandType.StoredProcedure);
                else
                    return await _connection.QueryAsync<T>(query, param);
            }
        }

        public async Task<IEnumerable<T>> InvokeQuery<T>(string query, bool isStoreProcedure = false) where T : class
        {
            using (IDbConnection _connection = new SqlConnection(_options.FunCart))
            {

                if (isStoreProcedure)
                    return await _connection.QueryAsync<T>(query, null, null, null, CommandType.StoredProcedure);
                else
                    return await _connection.QueryAsync<T>(query);
            }
        }


        public async Task<bool> InvokeExecute(string query, object param, bool isStoreProcedure = false)
        {
            using (IDbConnection _connection = new SqlConnection(_options.FunCart))
            {
                int res;
                if (isStoreProcedure)
                    res= await _connection.ExecuteAsync(query, param, null, null, CommandType.StoredProcedure);
                else
                    res= await _connection.ExecuteAsync(query, param);

                return true?res>0:false;
            }
        }
        public async Task<Guid> InvokeExecuteQuery(string query, object param, bool isStoreProcedure = false)
        {
            using (IDbConnection _connection = new SqlConnection(_options.FunCart))
            {

                if (isStoreProcedure)
                    return await _connection.ExecuteScalarAsync<Guid>(query, param, null, null, CommandType.StoredProcedure);
                else
                    return await _connection.ExecuteScalarAsync<Guid>(query, param);

            }
        }
    }
}
