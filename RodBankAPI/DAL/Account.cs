using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RodBankAPI.DAL
{
    [Table("ACCOUNT")]
    public partial class Account
    {
        [Key]
        public int AccountNumber { get; set; }
        [StringLength(35)]
        public string AccountType { get; set; }
        public double Balance { get; set; }
        [Column("AccountClientID")]
        public int AccountClientId { get; set; }

        [ForeignKey(nameof(AccountClientId))]
        [InverseProperty(nameof(Client.Account))]
        public virtual Client AccountClient { get; set; }

        public void updateAccount()
        {
            DatabaseContext db = new DatabaseContext();
            try
            {
                AccountClient = db.Client.Find(AccountClientId);
                System.Diagnostics.Debug.WriteLine(AccountClient.FirstName);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}
