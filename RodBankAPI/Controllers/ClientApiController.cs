using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RodBankAPI.DAL;

namespace RodBankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientApiController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return new List<Client> { };
        }

        [HttpGet("{id}")]
        public Client Get(int id)
        {
            DatabaseController db = new DatabaseController();
            return db.getClient(id);
        }

        [HttpPost]
        public string AddClient(Client cl)
        {
            try
            {
                DatabaseController db = new DatabaseController();
                db.createClient(cl);
                return "Success!";
            } catch (Exception e)
            {
                return "Exception ocurred" + e.Message;
            }
        }
        /*
         * {"LastName": "...",
         * "FirstName": "...",
         * "City" : "CityName"
         * }
         * */

        [HttpDelete("{id}")]
        public string DeleteClient(int id)
        {
            try
            {
                DatabaseController db = new DatabaseController();
                Boolean success = db.deleteClient(id);
                if (success)
                {
                    return "Deleted!";
                } else
                {
                    return "Failed...";
                }
            } catch (Exception e)
            {
                return "Exception ocurred" + e.Message;
            }
        }

    }
}