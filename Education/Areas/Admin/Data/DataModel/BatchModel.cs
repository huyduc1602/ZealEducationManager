using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Education.Areas.Admin.Data.DataModel
{
    public class BatchModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Batch name is not left blank")]
        public string Name { get; set; }
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Batch start date is not left blank")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Range(1, 3, ErrorMessage = "You must choose the Status")]
        [Display(Name = "Status")]
        public int Status { get; set; }
        [Range(1, 10000, ErrorMessage = "You must choose the Course")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        [Range(1, 10000, ErrorMessage = "You must choose the Faulty")]
        [Display(Name = "Faulty")]
        public int FaultyId { get; set; }
    }
}