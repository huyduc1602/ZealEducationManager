using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int GroupUserId { get; set; }
        [ForeignKey("GroupUserId")]
        public GroupUser GroupUser { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<Candicate> Candicates { get; set; }
        public virtual ICollection<Batch> Batchs { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Faulty> Faulties { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Category> Categories { get; set; }


    }
}
