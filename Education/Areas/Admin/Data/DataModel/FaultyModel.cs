using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Education.Areas.Admin.Data.DataModel
{
    public class FaultyModel
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public double Salary { get; set; }
        public string Qualification { get; set; }
        public DateTime Birthday { get; set; }
        public int UserId { get; set; }
    }
}