using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using to_do_backend.Models;
using to_do_backend.Utils;

namespace to_do_backend
{
    public class User
    {
        private User() { }

        public User(string password)
        {
            Password = password;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; private set; }
        public string Role { get; set; } = "user";
        public List<ToDoItem> Items { get; set; }
        public List<Challenge> Challenges { get; set; }

        public void setNewPassword(string password)
        {
            this.Password = password;
        }
    }
}
