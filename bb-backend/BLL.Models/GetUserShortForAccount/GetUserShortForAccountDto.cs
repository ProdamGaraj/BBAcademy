using Infrastructure.Models.Enum;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.GetUserForAccount
{
    public class GetUserShortForAccountDto
    {

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Organisation { get; set; }
        public string RecommendedBy { get; set; }
        public int Rating { get; set; }
    }
}
