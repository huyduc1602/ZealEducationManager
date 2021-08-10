using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL
{
    public class Bussiness
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
