using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApi.DataAccess.Entities
{
    [Table("AspNetRoles")]
    public class AccountRole: IdentityRole<long>
    {
        public AccountRole()
        {
        }

        public AccountRole(string name) : base(name)
        {
        }
    }
}
