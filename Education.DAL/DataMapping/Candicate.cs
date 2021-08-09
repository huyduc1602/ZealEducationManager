using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL
{
    public class Candicate
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ParentName { get; set; }
        public string ParentPhone { get; set; }
        public string Image { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Birthday  { get; set; }
        public DateTime JoiningDate  { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public DateTime CreatedAt  { get; set; }
        public DateTime UpdatedAt  { get; set; }
    }
}
