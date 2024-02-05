using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passworld.Models
{
    public class Password
    {
        [PrimaryKey, AutoIncrement]
        public int PId { get; set; }
        public string Usn { get; set; }
        public string Pwd { get; set; }
        public string Web { get; set; }
        public string Type { get; set; }

        public Password Clone() => MemberwiseClone() as Password;

        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(Usn))
            {
                return (false, "Username is required.");
            }
            else if (string.IsNullOrWhiteSpace(Pwd))
            {
                return (false, "Password is required.");
            }
            else if (string.IsNullOrWhiteSpace(Web))
            {
                return (false, "Website/app is required.");
            }
            else if (string.IsNullOrWhiteSpace(Type))
            {
                return (false, "Type is required.");
            }
            return (true, null);
        }
    }
}
