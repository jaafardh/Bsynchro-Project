using Bsynchro.Domain.Entities;
using Bsynchro.Services.DTOs;
using Bsynchro.Services.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bsynchro_Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly CustomersManager customerManager;
        public AccountsController(CustomersManager customerManager)
        {
            this.customerManager = customerManager;
        }

        [HttpGet("AddAccount/{customerId}/{InitialCredit}")]
        public async Task<ActionResult> AddAccount(int customerId,double initalCredit)
        {
            int result = await customerManager.AddAccount(customerId, initalCredit);
            if(result == -1)
            {
                return BadRequest();
            }
            if(result == -2 | result == -3 | result == -4 | result == -5)
            {
                return StatusCode(501); // internal server error //Account Added but error in transaction 
            }
            if(result == -7)
            {
                return BadRequest("Account is Opened! the initail Credit > balance");
            }
            return Ok("The Account is opened Successfully");
            
        }
        [HttpGet("GetUser")]
        public async Task<ActionResult> GetUserInfo(int customerId)
        {
            var x = await customerManager.GetFullCustomer(customerId);
            
            CustomerDTO customer = new CustomerDTO
            {
                Name = x.Name,
                SurName = x.Surname,
                balance = (double)x.Balance,
                SendedTransaction = new List<Transaction>(),
                RecievedTransacton = new List<Transaction>()
            };
            for(int i= 0; i < x.Accounts.Count; i++)
            {
                for(int j = 0; j < x.Accounts[i].)
            }
        }
    }
}
