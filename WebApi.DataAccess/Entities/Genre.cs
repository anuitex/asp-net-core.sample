using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("Genres")]
    public class Genre: IBaseEntity
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
