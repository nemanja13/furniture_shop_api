using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class UserDto : DtoBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public IEnumerable<int> AllowedUseCases { get; set; } = new List<int>();
    }
}
