using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("AuthorBooks")]
    public class AuthorBook: IBaseEntity
    {
        public long Id { get; set; }

        public long AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        [Dapper.Contrib.Extensions.Write(false)]
        public virtual Author Author { get; set; }

        public long BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        [Dapper.Contrib.Extensions.Write(false)]
        public virtual Book Book { get; set; }
    }
}
