using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL.DataMapping
{
    class GroupPermission
    {
        public int Id { get; set; }
        public int GroupUserId { get; set; }
        [ForeignKey("GroupUserId")]
        public GroupUser GroupUser { get; set; }
        
    }
}
