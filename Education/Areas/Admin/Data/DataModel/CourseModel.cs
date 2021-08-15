using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Education.Areas.Admin.Data.DataModel
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        [Display(Name ="Study Time")]
        public int StudyTime { get; set; }
        [Required]
        [Display(Name = "Course Code")]
        public string Code { get; set; }
        [Required]
        [MinLength(0)]
        public double Price { get; set; }
        [Required]
        [MinLength(0)]
        [Display(Name = "Sale Price")]
        public double SalePrice { get; set; }
        [DataType(DataType.MultilineText)]
        public string Detail { get; set; }
        public HttpPostedFileBase Image { get; set; }
        [Required]
        [MinLength(1)]
        [Display(Name = "Maximum Candicate")]
        public int MaximumCandicate { get; set; }
        public int UserId { get; set; }
    }
}