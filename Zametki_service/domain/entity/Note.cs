using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Zametki_service.domain.entity
{
    public class Note
    {
        [Key]
        public readonly int Id;
        [Required]
        public readonly int RelatedUserId;
        [Required]
        public string Title;
        [Required]
        public string Text;
    }
}
