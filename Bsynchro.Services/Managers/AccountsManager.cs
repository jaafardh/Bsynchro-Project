using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Bsynchro.Services.Managers
{
    public class AccountsManager
    {
        private readonly IAccountRepository accountRepository;
        public AccountsManager(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        public async Task<List<Account>> GetAccounts()
        {
            return (await accountRepository.GetAllAsync()).ToList();
        }
        public async Task<Account> GetAccount(int id)
        {
            return await accountRepository.FindAsync(id);
        }
        public async Task<int> AddAccount(Account account)
        {
            return await accountRepository.InsertAsync(account);
        }
        public async void AddAccountRange(IEnumerable<Account> accounts)
        {
            await accountRepository.InsertRangeAsync(accounts);
        }
        public async Task<int> DeleteAccount(int id)
        {
            return await accountRepository.DeleteAsyncById(id);
        }
        public async void DeleteAccountRange(IEnumerable<Account> accounts)
        {
            await accountRepository.DeleteRangeAsync(accounts, false);
        }
        public async Task<int> UpdateAccount(Account account)
        {
            return await accountRepository.UpdateAsync(account);
        }
    }
}
