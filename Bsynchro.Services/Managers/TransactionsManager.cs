using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.Services.Managers
{
    public class TransactionsManager
    {
        private readonly ITransactionRepository transactionRepository;
        public TransactionsManager(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
        public async Task<List<Transaction>> GetTransactions()
        {
            return (await transactionRepository.GetAllAsync()).ToList();
        }
        public async Task<Transaction> GetTransaction(int id)
        {
            return await transactionRepository.FindAsync(id);
        }
        public async Task<int> AddTransaction(Transaction transaction)
        {
            return await transactionRepository.InsertAsync(transaction);
        }
        public async void AddTransactionRange(IEnumerable<Transaction> transactions)
        {
            await transactionRepository.InsertRangeAsync(transactions);
        }
        public async Task<int> DeleteTransaction(int id)
        {
            return await transactionRepository.DeleteAsyncById(id);
        }
        public async void DeleteTransactionRange(IEnumerable<Transaction> transactions)
        {
            await transactionRepository.DeleteRangeAsync(transactions, false);
        }
        public async Task<int> UpdateTransaction(Transaction transaction)
        {
            return await transactionRepository.UpdateAsync(transaction);
        }
    }
}
