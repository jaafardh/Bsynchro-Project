using Bsynchro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.Services.DTOs
{
    public class CustomerDTO
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public double balance { get; set; }
        public List<Transaction> SendedTransaction { get; set; }
        public List<Transaction> RecievedTransacton { get; set; }
    }
}
