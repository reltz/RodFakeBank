using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using RodBankAPI.DAL;

namespace RodBankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return new List<Account> { };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "call a valid api!";
        }

       [HttpGet("balance/{id}")]
        public double getBalance(int id)
        {
            DatabaseController db = new DatabaseController();
            System.Diagnostics.Debug.WriteLine(db.getAccount(id));
            return db.getAccount(id).Balance;
        }

        [HttpPost]
        public string PostTransferRequest(Transfer t)
        {
            DatabaseController db = new DatabaseController();
            try
            {
                db.doTransfer(t);
                return "Success!";
            } catch(System.Exception e)
            {
                return "Exception ocurred" + e.Message;
            }
        }
        // format of request!!
        //        {
        //	"sendingAccountId": 1,
        //	"receivingAccountId" : 2,
        //	"amountToTransfer" : 150.0
        //}

        [HttpPost("deposit")]
        public void DepositMoney(OneTransaction t)
        {
            DatabaseController db = new DatabaseController();
            try
            {
                db.addMoney(t.accountId, t.amount);
            } catch (System.Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        // format of request!!
        //        {
        //	"accountId": 1,
        //	"amount" : 2,
        //	}

        [HttpPost("draw")]
        public void DrawMoney(OneTransaction t)
        {
            DatabaseController db = new DatabaseController();
            try
            {
               db.takeMoney(t.accountId, t.amount);
            } catch(System.Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
