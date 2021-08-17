using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL
{
    public class ClassRoomFaulty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Status { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual ClassRoom ClassRoom { get; set; }
        public int FaultyId { get; set; }
        [ForeignKey("FaultyId")]
        public virtual Faulty Faulty { get; set; }
    }
}
