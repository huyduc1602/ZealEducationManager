using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string  Name { get; set; }
        public int StudyTime { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }

        public string Detail { get; set; }
        public string Image { get; set; }
        public int MaximumCandicate { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<ClassRoom> ClassRooms { get; set; }
    }
}
