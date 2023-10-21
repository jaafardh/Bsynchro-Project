using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories;
using Bsynchro.InfraStructure.Data;
using Bsynchro.InfraStructure.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Bsynchro.InfraStructure.Repositories
{
    public class CustomerRepository : Repository<Customer, BsynchroDatabaseContext>, ICustomerRepository
    {
        public CustomerRepository(BsynchroDatabaseContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
