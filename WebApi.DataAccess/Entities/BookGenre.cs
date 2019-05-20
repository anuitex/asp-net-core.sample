using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("BookGenres")]
    public class BookGenre: IBaseEntity
    {
        public long Id { get; set; }

        public long BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        [Dapper.Contrib.Extensions.Write(false)]
        public virtual Book Book { get; set; }

        public long GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        [Dapper.Contrib.Extensions.Write(false)]
        public virtual Genre Genre { get; set; }
    }
}
