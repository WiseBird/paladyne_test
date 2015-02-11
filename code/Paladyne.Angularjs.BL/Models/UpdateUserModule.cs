using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paladyne.Angularjs.BL.Models
{
    public class UpdateUserModule
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ModuleId { get; set; }
        [Required]
        public string ModuleName { get; set; }
    }
}
