using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paladyne.Angularjs.DAL.Entities
{
    public enum Permissions
    {
        Prohibit,
        See,
        Edit
    }

    public class UserModule
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }
        [Key]
        [Column(Order = 1)]
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public Permissions Permission { get; set; }

        public virtual User User { get; set; }

        public string GranterId { get; set; }
        [ForeignKey("GranterId")]
        public virtual User Granter { get; set; }
    }
}