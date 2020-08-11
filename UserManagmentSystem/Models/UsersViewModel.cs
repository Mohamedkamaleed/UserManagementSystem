using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagmentSystem.Models
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public int OrdersConut { get; set; }
        public string Name { get; set; }
        public string Postition { get; set; }
        public string ProfilePicture { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }

        public List<GroupsViewModel> Groups { get; set; }

        public UsersViewModel()
        {
            Groups = new List<GroupsViewModel>();


        }
    }
}