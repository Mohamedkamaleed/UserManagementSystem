using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagmentSystem.Models
{
    public class GroupsViewModel
    {
        public int PK_Id { get; set; }
        public string Name { get; set; }
        public List<UsersViewModel> Users { get; set; }
        public List<ActionsViewModel> Actions { get; set; }

        public GroupsViewModel()
        {
            Users = new List<UsersViewModel>();
            Actions = new List<ActionsViewModel>();

        }
    }
}