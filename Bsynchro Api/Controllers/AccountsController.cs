using Bsynchro.Domain.Entities;
using Bsynchro.Services.DTOs;
using Bsynchro.Services.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpPost("AddAccount")]
        public async Task<ActionResult> AddAccount([FromBody] UserInfoDTO userInfo)
        {

            int result = await customerManager.AddAccount(userInfo.CustomerId, userInfo.InitalCredit);
            if(result == 0)
            {
                return BadRequest();
            }
            if(result == -1)
            {
                return BadRequest("Account is Opened! the initail Credit > balance");
            }
            return Ok("The Account is opened Successfully with id = "+ result);
            
        }
        [HttpGet("GetUser")]
        public async Task<ActionResult> GetUserInfo(int customerId)
        {
            var x = await customerManager.GetFullCustomer(customerId);

            string json = JsonConvert.SerializeObject(x, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return Ok(json);


            /*CustomerDTO customer = new CustomerDTO
            {
                Name = x.Name,
                SurName = x.Surname,
                balance = (double)x.Balance,
                SendedTransaction = new List<Transaction>(),
                RecievedTransacton = new List<Transaction>()
            };*/


        }
    }
}
