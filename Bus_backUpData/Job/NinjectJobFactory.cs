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
    public static class Nin
    {
        public static IKernel InitializeNinjectKernel()
        {
            var kernel = new StandardKernel();

            // setup Quartz scheduler that uses our NinjectJobFactory
            kernel.Bind<IScheduler>().ToMethod(x =>
            {
                var sched = new StdSchedulerFactory().GetScheduler().Result;
                sched.JobFactory = new NinjectJobFactory(kernel);
                return sched;
            });

            // add our bindings as we normally would (these are  kernel.Bind<IOptions<SettingEmail>>();
            kernel.Bind<IBusHistoryFTP>().To<BusHistoryFTP>();
            kernel.Bind<IBusConfigurationBackUp>().To<BusConfigurationBackUp>();
            kernel.Bind<IBusConfigViewModel>().To<BusConfigViewModel>();
            kernel.Bind<IBusFTP>().To<BusFTP>();
           
            // etc.

            return kernel;
        }
    }
}
