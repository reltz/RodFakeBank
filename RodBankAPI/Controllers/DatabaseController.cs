using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RodBankAPI.DAL;

namespace RodBankAPI.Controllers
{
    public class DatabaseController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //Account
        public DAL.Account getAccount(int id)
        {
            try
            {
                Account found = db.Account.Find(id);
                if (found != null)
                {
                    found.updateAccount();
                    return found;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //Client
        public Client getClient(int id)
        {
            Client found = db.Client.Find(id);
            return found;
        }

        public Client createClient(Client cl)
        {
            try
            {
                db.Client.Add(cl);
                db.SaveChanges();
                return cl;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public Boolean deleteClient(int id)
        {
            try
            {
                Client toDelete = db.Client.Find(id);

                // Manual cascade delete!
                List<Account> accounts = db.Account.Where(a => a.AccountClientId == id).ToList();
                foreach (Account ac in accounts)
                {
                    db.Account.Remove(ac);

                }
                db.SaveChanges();
                // end of cascade delete for accounts

                db.Client.Remove(toDelete);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        //CREATE ACCOUNT
        public Account createAccount(Account account)
        {
            try
            {
                db.Account.Add(account);
                db.SaveChanges();
                return account;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public Boolean deleteAccount(int id)
        {
            try
            {
                Account toDelete = db.Account.Find(id);
                db.Account.Remove(toDelete);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }


        // Transaction api
        public void addMoney(int accountId, double amount)
        {
            Debug.WriteLine(accountId + " is account");
            Debug.WriteLine(amount + " is amount");
            try
            {
                DAL.Account receiveingMoney = db.Account.Find(accountId);
                receiveingMoney.Balance += amount;
                db.Account.Update(receiveingMoney);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public double takeMoney(int accountId, double amount)
        {
            DAL.Account givingMoney = db.Account.Find(accountId);
            if (amount > givingMoney.Balance)
            {
                return 0;
            }

            givingMoney.Balance -= amount;
            db.Account.Update(givingMoney);
            db.SaveChanges();
            return amount;
        }


        public void doTransfer(Transfer t)
        {
            try
            {
                double toTransfer = this.takeMoney(t.sendingAccountId, t.amountToTransfer);
                this.addMoney(t.receivingAccountId, toTransfer);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }
        }
    }
}