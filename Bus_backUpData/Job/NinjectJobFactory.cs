using Quartz.Simpl;
using Quartz.Spi;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Quartz.Impl;
using Bus_backUpData.Interface;
using Bus_backUpData.Services;
using ModelProject.Models;
using Microsoft.Extensions.Options;
using DalBackup.Interface;
using DalBackup.Services;
using DalBackup.Repository;
using DalStoredProcedure.Interface;
using DalStoredProcedure.Services;
using AutoMapper;
using DalBackup.Data;
using Ninject.Extensions.NamedScope;
using Ninject.Extensions.ContextPreservation;
using Ninject.Web.Common;
using DalBackup.Config;

namespace Bus_backUpData.Job
{
    public class NinjectJobFactory : SimpleJobFactory
    {
        readonly IKernel _kernel;

        public NinjectJobFactory(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                // this will inject dependencies that the job requires
                return (IJob)this._kernel.Get(bundle.JobDetail.JobType);
            }
            catch (Exception e)
            {
                throw new SchedulerException(string.Format("Problem while instantiating job '{0}' from the NinjectJobFactory.", bundle.JobDetail.Key), e);
            }
        }
    }
    public class Nin
    {
        public IKernel InitializeNinjectKernelAsync()
        {
            var kernel = new StandardKernel();

            

            // setup Quartz scheduler that uses our NinjectJobFactory
            kernel.Bind<IScheduler>().ToMethod( x => {
                var builder = SchedulerBuilder.Create();
                builder.UsePersistentStore(options =>
                {
                    options.UseClustering();
                    options.UseSQLite(sqlServerOptions =>
                    {
                        sqlServerOptions.ConnectionString = Setting.ConnectionSQLite;
                    });
                    options.UseSerializer<JsonObjectSerializer>();
                });
                var scheduler = builder.BuildScheduler().Result;
                scheduler.JobFactory = new NinjectJobFactory(kernel);
                return scheduler;
            });
       
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoModelMapperProfile>(); // Thêm profile AutoMapper của bạn
                                                         // Thêm các ánh xạ khác nếu cần
            });                   
            // add our bindings as we normally would (these are  kernel.Bind<IOptions<SettingEmail>>();
            kernel.Bind<AdminLayout_VuexyContext>().ToSelf().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IDalHistoryFTP>().To<DalHistoryFTP>();
            kernel.Bind<IMapper>().ToMethod(ctx => new Mapper(config)).InSingletonScope();
            kernel.Bind<IDalDatabaseConnect>().To<DalDatabaseConnect>();
            kernel.Bind<IDalConfigurationBackUp>().To<DalConfigurationBackUp>();
            kernel.Bind<IDalStoredProcedureServices>().To<DalStoredProcedureServices>();
            kernel.Bind<IDalServerConnect>().To<DalServerConnect>();
            kernel.Bind<IBusHistoryFTP>().To<BusHistoryFTP>();
            kernel.Bind<IBusConfigurationInformation>().To<BusConfigurationInformation>();
            kernel.Bind<IBusConfigViewModel>().To<BusConfigViewModel>();
            kernel.Bind<IBusFTP>().To<BusFTP>();
            kernel.Bind<IBusConfigServer>().To<BusConfigServer>();
            kernel.Bind<IBusStoredProcedureServices>().To<BusStoredProcedureServices>();
           

            // etc.

            return kernel;
        }
    }
}
