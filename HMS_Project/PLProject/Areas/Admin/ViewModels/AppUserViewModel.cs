using DALProject.model;
using Microsoft.AspNetCore.Identity;
using System.CodeDom;

namespace PLProject.Areas.Admin.ViewModels
{
    public class AppUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<String> Roles { get; set; }
    }
}
