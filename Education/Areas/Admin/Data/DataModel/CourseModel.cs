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
        public int StudyTime { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double SalePrice { get; set; }
        public string Detail { get; set; }
        [Required]
        public HttpPostedFileBase Image { get; set; }
        [Required]
        public int MaximumCandicate { get; set; }
        public int UserId { get; set; }
    }
}