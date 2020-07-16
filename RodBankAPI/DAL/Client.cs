using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RodBankAPI.DAL
{
    [Table("CLIENT")]
    public partial class Client
    {
        public Client()
        {
            Account = new HashSet<Account>();
        }

        [Key]
        [Column("ClientID")]
        public int ClientId { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string City { get; set; }

        [InverseProperty("AccountClient")]
        public virtual ICollection<Account> Account { get; set; }
    }
}
