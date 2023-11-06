using System.ComponentModel.DataAnnotations.Schema;

namespace Bsynchro.Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public DateTime? TransactionDate { get; set; }

        public int SenderId { get; set; }

        public int RecipientId { get; set; }

        public double? Amount { get; set; }

        public virtual Account Recipient { get; set; } = null!;

        public virtual Account Sender { get; set; } = null!;
    }
}