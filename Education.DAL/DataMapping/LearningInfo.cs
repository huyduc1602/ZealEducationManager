using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL
{
    public class LearningInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Point { get; set; }
        public int Number { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int? ExamId { get; set; }
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }
        public int? RoomId { get; set; }
        [ForeignKey("RoomId")]
        public ClassRoom ClassRoom { get; set; }
        public int CandicateId { get; set; }
        [ForeignKey("CandicateId")]
        public Candicate Candicate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
