using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("RefreshTokens")]
    public class RefreshToken: IBaseEntity
    {
        [Key]
        public long Id { get; set; }

        public long AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [Dapper.Contrib.Extensions.Write(false)]
        public virtual Account Account { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
