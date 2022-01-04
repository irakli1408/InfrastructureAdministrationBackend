using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.DataModels
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public List<User> user { get; set; }
    }
}
