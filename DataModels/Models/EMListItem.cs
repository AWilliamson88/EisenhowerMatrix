using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.Models
{
    [Table("EMListItems")]
    public class EMListItem
    {
        public int EMListItemId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        public bool IsComplete { get; set; }
    }
}
