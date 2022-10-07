using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManagePeople.Models
{
    public class Transations
    {
        [Key]
        public int TransationId { get; set; }

        [Column("IdAccount")]
        public int? IdAccount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Transation Date")]
        public DateTime TransationDate { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Account Holder")]
        public string AccountHolder { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Transation Details")]
        public string TransationDetails { get; set; }


        [ForeignKey(nameof(IdAccount))]
        [InverseProperty(nameof(Accounts.Transations))]
        public virtual Accounts IdAccountsNoNavigation { get; set; }

    }
}
