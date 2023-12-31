﻿using AutoMapper;
using ModelProject.Models;
using ModelProject.ViewModels;
using ModelProject.ViewModels.ViewModelConnect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Services
{
    public class AutoModelMapperProfile : Profile
    {
        public AutoModelMapperProfile()
        {
            // Định nghĩa các bản đồ ánh xạ giữa các đối tượng
            CreateMap<ServerConnect, ServerConnectViewModel>()
                 .ForMember(dest => dest.DatabaseConnectViewModels, opt => opt.MapFrom(src => GetDatabaseConnectViewModels(src.DatabaseConnects.ToList())));
            CreateMap<ConfigurationBackUpViewModel, ConfigurationBackUp>();
                
			CreateMap<ConfigurationBackUp, ConfigurationBackUpViewModel>()
                .ForMember(dest => dest.DatabaseConnectViewModel, opt => opt.MapFrom(src => GetDatabaseConnectViewModel(src.DatabaseConnect)));

            CreateMap<BackUpSettingViewModel, BackUpSetting>();
			CreateMap<FTPSettingViewModel, FTPSetting>();
			CreateMap<ScheduleBackupViewModel, ScheduleBackup>();
			CreateMap<EmailConfirmationViewModel, EmailConfirmation>();

			CreateMap<BackUpSetting, BackUpSettingViewModel>();
			CreateMap<FTPSetting, FTPSettingViewModel>();
			CreateMap<ScheduleBackup, ScheduleBackupViewModel>();
			CreateMap<EmailConfirmation, EmailConfirmationViewModel>();

			CreateMap<IDataReader, JobHistoryViewModel>();
			CreateMap<DatabaseConnect, DatabaseConnectViewModel>()
                .ForMember(dest => dest.ServerName, otp => otp.MapFrom(src => src.ServerConnects.ServerName));
		
			// Thêm các bản đồ khác nếu cần
		}

        public List<DatabaseConnectViewModel> GetDatabaseConnectViewModels(List<DatabaseConnect>? databaseConnects)
        {
            if (databaseConnects == null) {
                return new List<DatabaseConnectViewModel>();
            }
            var res = databaseConnects.Select(x => new DatabaseConnectViewModel() {
                DatabaseName = x.DatabaseName,
                ServerName = x.ServerConnects.ServerName,
                Id = x.Id

        }).ToList();
            return res;
        }
        public DatabaseConnectViewModel GetDatabaseConnectViewModel(DatabaseConnect databaseConnect)
        {
            var res = new DatabaseConnectViewModel();
            if(databaseConnect != null )
            {
                res.DatabaseName = databaseConnect.DatabaseName;
                res.Id = databaseConnect.Id;
                if (databaseConnect.ServerConnects != null) {
                    res.ServerName = databaseConnect.ServerConnects.ServerName; 
                    
                }
            }
            return res;
        }   
    }
}
