using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories;
using Bsynchro.InfraStructure.Data;
using Bsynchro.InfraStructure.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Bsynchro.InfraStructure.Repositories
{
    public class AccountRepository : Repository<Account, BsynchroDatabaseContext>, IAccountRepository
    {
        private readonly BsynchroDatabaseContext dbContext;
        public AccountRepository(BsynchroDatabaseContext dbContext, ILogger logger) : base(dbContext, logger)
        {
            this.dbContext = dbContext;
        }

        public List<Account> GetCustomerAccounts(int customerID)
        {
            var accounts = dbContext.Accounts.Where(x => x.CustomerId == customerID).ToList();
            for(int i =0; i < accounts.Count; i++)
            {
                accounts[i].TransactionSenders = dbContext.Transactions.Where(x=>x.SenderId == accounts[i].AccountId).ToList();
                accounts[i].TransactionRecipients = dbContext.Transactions.Where (x=>x.RecipientId == accounts[i].AccountId).ToList();
            }
            return accounts;
        }
    }
}
