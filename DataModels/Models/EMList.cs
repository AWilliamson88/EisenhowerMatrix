using System.ComponentModel.DataAnnotations;

namespace DataModels.Models
{
    public class EMList
    {
        public int EMListId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        public ICollection<EMListItem> EMListItems { get; set; } = new List<EMListItem>();
    }
}
