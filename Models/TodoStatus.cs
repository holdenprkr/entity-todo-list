using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Todo.Models
{
    public class TodoStatus
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
