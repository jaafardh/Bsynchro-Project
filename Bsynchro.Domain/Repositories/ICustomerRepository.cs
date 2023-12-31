﻿using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<List<Account>> AddAccount(int customerId);
        int Count();  
    }
}
