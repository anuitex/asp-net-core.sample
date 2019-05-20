using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("Books")]
    public class Book: IBaseEntity
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int PublicationYear { get; set; }
    }
}
