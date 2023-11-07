using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories;
using Bsynchro.Services.DTOs;
using Bsynchro.Services.Mappers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.Services.Managers
{
    public class CustomersManager
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly IAccountRepository accountRepository;
        public CustomersManager(ICustomerRepository customerRepository, ITransactionRepository transactionRepository,IAccountRepository accountRepository)
        {
            this.customerRepository = customerRepository;
            this.transactionRepository = transactionRepository;
            this.accountRepository = accountRepository;
        }
        public async Task<List<Customer>> GetCustomers()
        {
            return (await customerRepository.GetAllAsync()).ToList();
        }
        public async Task<Customer> GetCustomer(int id)
        {
            return await customerRepository.FindAsync(id);
        }
        public async Task<int> AddCustomer(Customer customer)
        {
            return await customerRepository.InsertAsync(customer);
        }
        public async void AddCustomerRange(IEnumerable<Customer> customers)
        {
            await customerRepository.InsertRangeAsync(customers);
        }
        public async Task<int> DeleteCustomer(int id)
        {
            return await customerRepository.DeleteAsyncById(id);
        }
        public async void DeleteCustomerRange(IEnumerable<Customer> customers)
        {
            await customerRepository.DeleteRangeAsync(customers, false);
        }

        public async Task<CustomerDTO> GetFullCustomer(int customerId)
        {
            var customer = await customerRepository.FindAsync(customerId);
            if(customer == null) { return null; }
            int[] accountsIds = accountRepository.GetCustomerAccountsId(customerId);
            List<Transaction> sended = transactionRepository.GetCustomerSenderTransactions(accountsIds);
            List<TransactionDTO> sendedDTO = new List<TransactionDTO>();
            for(int i = 0; i < sended.Count; i++)
            {
                sendedDTO.Add(ObjectMapper.Mapper.Map<TransactionDTO>(sended[i]));
            }
            List<Transaction> recieved = transactionRepository.GetCustomerRecipientTransactions(accountsIds);
            List<TransactionDTO> recievedDTO = new List<TransactionDTO>();
            for(int i = 0; i < recieved.Count; i++)
            {
                recievedDTO.Add(ObjectMapper.Mapper.Map<TransactionDTO>(recieved[i]));
            }
            CustomerDTO customerDTO = new CustomerDTO
            {
                Name = customer.Name,
                SurName = customer.Surname,
                balance = (double)customer.Balance,
                SendedTransaction = sendedDTO,
                RecievedTransacton = recievedDTO
            };
            return customerDTO;
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            return await customerRepository.UpdateAsync(customer);
        }
        public async Task<int> AddAccount(int customerId,double initialCredit)
        {
            var result = await customerRepository.AddAccount(customerId);
            if (result == null) { return 0; }  //failed to add account
            var accounts = result.ToList();
            if (accounts.Count < 2) { return 0; } // failed in accounts
            if (initialCredit > 0)
            {
                Transaction transaction = new Transaction
                {
                    SenderId = accounts[0].AccountId,
                    RecipientId = accounts.Last().AccountId,
                    Amount = initialCredit,
                };
                 int transResult = await transactionRepository.Transact(transaction);
                if (transResult > 0) // success
                {
                    return accounts.Last().AccountId;
                }
                else
                {
                    return transResult;
                }
            }
            else
            {
                return accounts.Last().AccountId;
            }
        }
        public bool IsEmpty()
        {
            return customerRepository.Count() == 0;
        }
    }
}
