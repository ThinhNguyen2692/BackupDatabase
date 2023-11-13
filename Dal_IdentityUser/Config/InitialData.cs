using DalBackup.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalBackup.Config
{
    public static class InitialData
    {
        public static List<AdminLayout_VuexyUser> GetAdminUsers()
        {
            var users = new List<AdminLayout_VuexyUser>
            {
                //admin pass: abc123@ -> change later
                new AdminLayout_VuexyUser
                {
                    Id = Guid.NewGuid().ToString().ToLower(),
                    UserName = "Admin",
                    Email = "thinh.nguyenngoc@vnresource.org",
                    PasswordHash = "AF924q+3egq51yXasR/R3JYrcJAD7VzH48vt7ZzSyRf/5X+rC5TGvjoS4dUq6wNv4w==",
                }
            };
            return users;
        }

        public static List<IdentityRole> GetRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString().ToLower(),
                    Name = "Admin"
                }
            };
        }

        public static List<IdentityUserRole<Guid>> GetUsersInRoles()
        {
            var usersInRoles = new List<IdentityUserRole<Guid>>();

            var adminUsers = GetAdminUsers();
            var adminRole = GetRoles().SingleOrDefault(r => r.Name == "Admin");
            if (adminRole != null)
            {
                foreach (var adminUser in adminUsers)
                {
                    usersInRoles.Add(new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse(adminRole.Id),
                        UserId = Guid.Parse(adminUser.Id)
                    });
                }
            }
            return usersInRoles;
        }
    }
}
