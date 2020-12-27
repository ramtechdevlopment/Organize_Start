using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;

namespace Organize.IndexedDB
{
    public class IndexedDB : IPersistanceService
    {
        private IJSRuntime _jsRuntime;

        public IndexedDB(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;

        }

        public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> whereExpression) where T : BaseEntity
        {
            var tableName = typeof(T).Name;
            //js stores
            var entities = await _jsRuntime.InvokeAsync<T[]>("organizeIndexedDB.getAllAsync", tableName);
            return entities.Where(whereExpression.Compile());
        }

        public async Task<int> InsertAsync<T>(T entity) where T : BaseEntity
        {
            var tableName = typeof(T).Name;
            var serializedEntity = SerializeAndRemoveArraysAndNavigationProperties(entity);

            var id = await _jsRuntime.InvokeAsync<int>("organizeIndexedDB.addAsync", tableName, serializedEntity);
            return id;
        }

        private string SerializeAndRemoveArraysAndNavigationProperties<T>(T entity)
        {
            var stringWithoutNavigationProperties = JsonConvert.SerializeObject(entity);
            return stringWithoutNavigationProperties;
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            var tableName = typeof(T).Name;
            Console.WriteLine(tableName);
            var serializedEntity = SerializeAndRemoveArraysAndNavigationProperties(entity);
            await _jsRuntime.InvokeVoidAsync("organizeIndexedDB.putAsync", tableName, serializedEntity, entity.Id);
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            var tableName = typeof(T).Name;
            await _jsRuntime.InvokeVoidAsync("organizeIndexedDB.deleteAsync", tableName, entity.Id);
        }

        public async Task InitAsync()
        {
            await _jsRuntime.InvokeVoidAsync("organizeIndexedDB.initAsync");
        }

        public Task<User> AuthenticateAndGetUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
