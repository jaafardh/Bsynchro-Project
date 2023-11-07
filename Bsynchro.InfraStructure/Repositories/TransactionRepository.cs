using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories;
using Bsynchro.InfraStructure.Data;
using Bsynchro.InfraStructure.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Bsynchro.InfraStructure.Repositories
{
    public class TransactionRepository : Repository<Transaction, BsynchroDatabaseContext>, ITransactionRepository
    {
        private readonly BsynchroDatabaseContext dbContext;
        public TransactionRepository(BsynchroDatabaseContext dbContext, ILogger logger) : base(dbContext, logger)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Transact(Transaction transaction)
        {
            if (transaction == null) { return 0; }
            if (transaction.Amount == null) { return 0; }
            var Sender = await db.Accounts.FindAsync(transaction.SenderId);
            if(Sender == null) { return 0; }
            var Recipient = await db.Accounts.FindAsync(transaction.RecipientId);
            if(Recipient == null) { return 0; }
            if (Sender.Balance < transaction.Amount) { return -1; }
            Sender.Balance -= transaction.Amount;
            Recipient.Balance += transaction.Amount;
            transaction.TransactionDate = DateTime.Now;
            db.Transactions.Add(transaction);
            Sender.TransactionSenders.Add(transaction);
            Recipient.TransactionRecipients.Add(transaction);
            db.Accounts.UpdateRange(Sender, Recipient);
            return await db.SaveChangesAsync();
        }

        public List<Transaction> GetCustomerRecipientTransactions(int[] accountsId)
        {
            if (accountsId == null) { return new List<Transaction>(); }
            if(accountsId.Length == 0) { return new List<Transaction>(); }
            var transcripts = db.Transactions.Where(tx => accountsId.Contains(tx.RecipientId)).ToList();
            return transcripts;
        }
        public List<Transaction> GetCustomerSenderTransactions(int[] accountsId)
        {
            if (accountsId == null) { return new List<Transaction>(); }
            if (accountsId.Length == 0) { return new List<Transaction>(); }
            var transcripts = db.Transactions.Where(tx => accountsId.Contains(tx.SenderId)).ToList();
            return transcripts;
        }
    }
}
