using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers
{
    public class ChangePasswordDto
    {
        public string IdUser { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string CreatedBy { get; set; }
    }
}
