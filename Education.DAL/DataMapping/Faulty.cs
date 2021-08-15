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
    public class Faulty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public string Qualification { get; set; }
        public DateTime Birthday { get; set; }
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<ClassRoom> ClassRooms { get; set; }
    }
}
