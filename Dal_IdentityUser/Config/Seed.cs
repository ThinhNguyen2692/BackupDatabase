using DalBackup.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalBackup.Config
{
    public static class Seed
    {
        public static void Init(AdminLayout_VuexyContext context) {
            InitialData.GetAdminUsers().ForEach(u => context.Users.Add(u));

            InitialData.GetRoles().ForEach(r => context.Roles.Add(r));

            InitialData.GetUsersInRoles().ForEach(ur => context.Set<IdentityUserRole<Guid>>().Add(ur));
        }
    }
}
