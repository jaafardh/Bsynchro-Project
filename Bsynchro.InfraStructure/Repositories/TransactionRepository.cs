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
            if (transaction == null) { return -3; }
            if (transaction.Amount == null) { return -4; }
            var Sender = await db.Accounts.FindAsync(transaction.TransactionId);
            if(Sender == null) { return -5; }
            var Recipient = await db.Accounts.FindAsync(transaction.RecipientId);
            if(Recipient == null) { return -6; }
            if (Sender.Balance < transaction.Amount) { return -7; }
            Sender.Balance -= transaction.Amount;
            Recipient.Balance += transaction.Amount;
            transaction.TransactionDate = DateTime.Now;
            db.Transactions.Add(transaction);
            Sender.TransactionSenders.Add(transaction);
            Recipient.TransactionRecipients.Add(transaction);
            db.Accounts.UpdateRange(Sender, Recipient);
            return await db.SaveChangesAsync();
        }
    }
}
