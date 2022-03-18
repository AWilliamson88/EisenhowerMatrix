using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Models
{
    public enum Priority
    {
        NONE, LOW, MED, HIGH
    }

    [Table("ToDoItems")]
    public class ToDoItem
    {
        public int ToDoItemId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = "New Item";
        public bool IsComplete { get; set; }
        public Priority Priority { get; set; } = Priority.NONE;

        //public int ToDoListId { get; set; }
        //public ToDoList ToDoList { get; set; }
    }
}
