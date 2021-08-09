using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Education.Areas.Admin.Data.DataModel
{
    public class CandicateModel
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Display(Name = "Parent Name")]
        public string ParentName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Display(Name= "Parent Phone")]
        public string ParentPhone { get; set; }
        public string Image { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }
        public int UserId { get; set; }
    }
}