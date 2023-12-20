using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ModelProject.Models;
using ModelProject.ViewModels;

namespace Bus_backUpData.Services
{
    public class AutoModelMapper : Profile
    {
        public class MapperConfig<TSource, TDestination> where TDestination : class
        {
            public static Mapper InitializeAutomapper()
            {
                //Provide all the Mapping Configuration
                var config = new MapperConfiguration(cfg =>
                {
                    //Configuring Employee and EmployeeDTO
                    cfg.CreateMap<TSource, TDestination>();
                    cfg.CreateMap<BackUpSettingViewModel, BackUpSetting>();
                    cfg.CreateMap<FTPSettingViewModel, FTPSetting>();
                    cfg.CreateMap<ScheduleBackupViewModel, ScheduleBackup>();
                    cfg.CreateMap<EmailConfirmationViewModel, EmailConfirmation>();

                    cfg.CreateMap<BackUpSetting, BackUpSettingViewModel>();
                    cfg.CreateMap<FTPSetting, FTPSettingViewModel>();
                    cfg.CreateMap<ScheduleBackup, ScheduleBackupViewModel>();
                    cfg.CreateMap<EmailConfirmation, EmailConfirmationViewModel>();

                    cfg.CreateMap<IDataReader, JobHistoryViewModel>();

                    //Any Other Mapping Configuration ....
                });
                //Create an Instance of Mapper and return that Instance
                var mapper = new Mapper(config);
                return mapper;
            }
        }
    }
}
