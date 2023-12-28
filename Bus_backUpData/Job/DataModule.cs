using DalBackup.Data;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ninject.Extensions.NamedScope;

namespace Bus_backUpData.Job
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            Bind<AdminLayout_VuexyContext>().ToSelf().InCallScope();
        }
    }
}
