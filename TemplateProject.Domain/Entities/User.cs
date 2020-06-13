using TemplateProject.Entities.Model.Base;
using System;

namespace TemplateProject.Entities.Model
{
    public class User : BaseEntity<Guid>
    {
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
