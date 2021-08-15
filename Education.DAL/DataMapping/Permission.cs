using Education.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BussinessId { get; set; }
        [ForeignKey("BussinessId")]
        public Bussiness Bussiness { get; set; }
        public int Status { get; set; }
        public ICollection<GroupPermission> GroupPermissons { get; set; }
    }
}
