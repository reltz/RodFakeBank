using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using RodBankAPI.DAL;

namespace RodBankAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return new List<Account> { };
        }

        [HttpGet("{id}")]
        public Account Get(int id)
        {
            DatabaseController db = new DatabaseController();
            return db.getAccount(id);
        }

        [HttpPost]
        public string AddAccount(Account ac)
        {
            try
            {
                DatabaseController db = new DatabaseController();
                Client c = db.getClient(ac.AccountClientId);
                ac.AccountClient = c;

                db.createAccount(ac);
                return "Success!";
            }
            catch (Exception e)
            {
                return "Exception ocurred" + e.Message;
            }
        }
        /*
         * {"AccountType": "...",
         * "Balance": int,
         * "AccountClientId" : int
         * }
         * */

        [HttpDelete("{id}")]
        public string DeleteAccount(int id)
        {
            try
            {
                DatabaseController db = new DatabaseController();
                db.deleteAccount(id);
                return "success";
              }
            catch (Exception e)
            {
                return "Exception ocurred" + e.Message;
            }
        }
    }
}