using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Services
{
    public class AccountServices: IAccountServices
    {
        private readonly IMongoDbAccountRepository _mongoDbRepository;

        public AccountServices(IMongoDbAccountRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task<IEnumerable<Account>> GetAsync() =>
            await _mongoDbRepository.GetAsync();

        public async Task<Account> GetAsync(string id) =>
            await _mongoDbRepository.GetAsync(id);

        public async Task<IEnumerable<Account>> GetAsync(Account accountFilter) =>
            await _mongoDbRepository.GetAsync(accountFilter);

        public async Task<Account> CreateAsync(Account account) =>
            await _mongoDbRepository.CreateAsync(account);

        public async Task<bool> ReplaceAsync(string id, Account accountIn) =>
            await _mongoDbRepository.ReplaceAsync(accountIn);
        
        public async Task<bool> UpdateSetAsync(Account accountIn, string name, object value) =>
            await _mongoDbRepository.UpdateSetAsync(accountIn, name, value);

        public async Task<bool> DeleteAsync(Account accountIn) =>
            await _mongoDbRepository.DeleteAsync(accountIn.Id);

        public async Task<bool> DeleteAsync(string id) =>
            await _mongoDbRepository.DeleteAsync(id);
    }
}
