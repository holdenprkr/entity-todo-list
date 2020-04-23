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
        public List<SelectListItem> TodoStatusOptions { get; set; }

    }
}
