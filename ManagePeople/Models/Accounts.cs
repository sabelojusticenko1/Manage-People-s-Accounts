using ManagePeople.Models.StatusEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManagePeople.Models
{
    public class Accounts
    {
        [Key]
        public int IdAccount { get; set; }

        [Column("IdPerson")]
        public int? IdPerson { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNo { get; set; }

        [Required]
        [Display(Name = "Account Holder")]
        public string AccountHolder { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Required]
        [Display(Name = "Status")]
        public StatusEnum Status { get; set; }

        [ForeignKey(nameof(IdPerson))]
        [InverseProperty(nameof(Person.Accounts))]
        public virtual Person IdPersonNoNavigation { get; set; }
        public virtual ICollection<Transations> Transations { get; set; }

    }
}
