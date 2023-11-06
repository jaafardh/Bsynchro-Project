using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public double? Balance { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
