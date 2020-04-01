using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;

namespace MongoDbTest.Infrastructure.Services
{
    public class AccountServices: IAccountServices
    {
        private readonly IMongoDbAccountRepository _accountRepository;

        public AccountServices(IMongoDbAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<Account>> GetAllAsync() =>
            await _accountRepository.GetAllAsync();

        public async Task<Account> GetByIdAsync(string id) =>
            await _accountRepository.GetByIdAsync(id);

        public async Task<Account> GetOneAsync(Expression<Func<Account, bool>> filter) =>
            await _accountRepository.GetOneAsync(filter);

        public async Task<Account> AddAsync(Account account) =>
            await _accountRepository.InsertOneAsync(account);

        public async Task<bool> ReplaceAsync(string id, Account accountIn) =>
            await _accountRepository.ReplaceOneAsync(accountIn);
        
        public async Task<bool> UpdateProductsAsync(Account accountIn) =>
            await _accountRepository.UpdateSetAsync(accountIn, "products", accountIn.Products);

        public async Task<bool> DeleteAsync(Account accountIn) =>
            await _accountRepository.DeleteAsync(accountIn.Id);

        public async Task<bool> DeleteAsync(string id) =>
            await _accountRepository.DeleteAsync(id);
    }
}
