using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
        public virtual ICollection<UserUseCase> UserUseCases { get; set; } = new HashSet<UserUseCase>();
    }
}
