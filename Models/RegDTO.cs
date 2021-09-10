using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAuthorityProject.Models
{
    public class RegDTO
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage ="This Filed is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This Filed is Required")]
        public string LastName { get; set; }
      //  public int Email { get; set; }
      //  public int Password { get; set; }
        public DateTime Bithdate { get; set; }


        public int Status { get; set; }

      

    }
}
