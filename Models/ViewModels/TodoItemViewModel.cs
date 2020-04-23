using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Todo.Models.ViewModels
{
    public class TodoItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int TodoStatusId { get; set; }
        public TodoStatus TodoStatus { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public List<SelectListItem> TodoStatusOptions { get; set; }

    }
}
