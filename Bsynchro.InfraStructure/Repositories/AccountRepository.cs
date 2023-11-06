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

        public int[] GetCustomerAccountsId(int customerID)
        {
            var accounts = dbContext.Accounts.Where(x => x.CustomerId == customerID).ToList();
            //for(int i =0; i < accounts.Count; i++)
            //{
            //    accounts[i].TransactionSenders = dbContext.Transactions.Where(x=>x.SenderId == accounts[i].AccountId).ToList();
            //    accounts[i].TransactionRecipients = dbContext.Transactions.Where (x=>x.RecipientId == accounts[i].AccountId).ToList();
            //}
            int[] ids = new int[accounts.Count];
            for(int i = 0; i < accounts.Count; i++)
            {
                ids[i] = accounts[i].AccountId;
            }
            return ids;
        }
    }
}
