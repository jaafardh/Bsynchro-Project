using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.Services.DTOs
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }

        public DateTime? TransactionDate { get; set; }

        public int SenderId { get; set; }

        public int RecipientId { get; set; }

        public double? Amount { get; set; }
    }
}
