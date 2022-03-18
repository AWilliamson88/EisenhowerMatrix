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
        public string Name { get; set; } = "New List";

        public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }
}
