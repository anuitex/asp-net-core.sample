using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApi.DataAccess.Entities
{
    [Table("AspNetUsers")]
    public class Account: IdentityUser<long>, IBaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string ProfilePictureURL { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/220px-User_icon_2.svg.png";

        [Required]
        public GenderType Gender { get; set; }
    }

    public enum GenderType
    {
        Undefined = 0,
        Male = 1,
        Female = 2
    }
}
