using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.Domain.Entities
{
    public class Account
    {
        public int AccountId { get; set; }

        public double? Balance { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;

        public virtual ICollection<Transaction> TransactionRecipients { get; set; } = new List<Transaction>();

        public virtual ICollection<Transaction> TransactionSenders { get; set; } = new List<Transaction>();
    }
}
