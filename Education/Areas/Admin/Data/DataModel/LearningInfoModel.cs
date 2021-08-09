using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Education.Areas.Admin.Data.DataModel
{
    public class LearningInfoModel
    {
        [Key]
        public int Id { get; set; }
        public float Point { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }
        public int ExamId { get; set; }
        public int BatchId { get; set; }
        public int CandicateId { get; set; }
    }
}