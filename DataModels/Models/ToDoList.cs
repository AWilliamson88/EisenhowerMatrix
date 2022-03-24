using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Models
{
    public class ToDoList
    {
        public int ToDoListId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }
}
