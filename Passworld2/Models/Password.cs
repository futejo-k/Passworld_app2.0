using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passworld.Models
{
    public class Password
    {
        [PrimaryKey]
        public int PId { get; set; }
        public string Usn { get; set; }
        public string Pwd { get; set; }
        public string Web { get; set; }
        public string Type { get; set; }

        public Password Clone() => MemberwiseClone() as Password;
    }
}
