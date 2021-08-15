using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Areas.Admin.Data
{
    public class PermissionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BusinessId { get; set; }
        public bool IsGranted { get; set; }
    }
}