using Bus_backUpData.Data;
using ModelProject.Func;
using Bus_backUpData.Interface;
using ModelProject.Models;
using ModelProject.ViewModels;
using HRM.SC.Core.Security;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bus_backUpData.Services.AutoModelMapper;
using DalBackup.Interface;
using DalStoredProcedure.Interface;
using AutoMapper;

namespace Bus_backUpData.Services
{
    public class BusConfigViewModel : IBusConfigViewModel
    {

        public Context _context;
        private readonly IDalConfigurationBackUp _dalConfigurationBackUp;
        private readonly IDalStoredProcedureServices _dalStoredProcedureServices;
        private readonly IBusConfigurationInformation _busConfigurationInformation;
		private readonly IMapper _mapper;
		public BusConfigViewModel(Context Context,  IDalConfigurationBackUp dalConfigurationBackUp, IDalStoredProcedureServices dalStoredProcedureServices, IBusConfigurationInformation busConfigurationInformation, IMapper mapper)
        {
            _context = Context;
            Setting.DatabaseName = _context.Database.GetDbConnection().Database;
            _dalConfigurationBackUp = dalConfigurationBackUp;
			_dalStoredProcedureServices = dalStoredProcedureServices;
            _busConfigurationInformation = busConfigurationInformation;
            _mapper = mapper;
		}
        /// <summary>
        /// Get all danh sach cau hinh
        /// </summary>
        /// <returns></returns>
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel(string DatabaseName = null)
        {
            var data = _dalConfigurationBackUp.GetData();
            if(!string.IsNullOrEmpty(DatabaseName))
            data = data.Where(x => x.DatabaseConnect.DatabaseName == DatabaseName).ToList();
            var mapper = MapperConfig<ConfigurationBackUp, ConfigurationBackUpViewModel>.InitializeAutomapper();
            var ConfigurationBackUpViewModel = mapper.Map<List<ConfigurationBackUp>, List<ConfigurationBackUpViewModel>>(data);
            ConfigurationBackUpViewModel.ForEach(x => { x.BackUpSetting.Name = _context.Database.GetDbConnection().Database; });
            return ConfigurationBackUpViewModel;
        }


        /// <summary>
        /// get cấu hình theo JobName
        /// </summary>
        /// <param name="JobName"></param>
        /// <returns></returns>
        public ConfigurationBackUpViewModel GetConfigurationBackUpViewModelByJobName(string JobName)
        {
            var ConfigurationBackUpViewModels = GetConfigurationBackUpViewModel();
            var ConfigurationBackUpViewModel = ConfigurationBackUpViewModels.FirstOrDefault(x => x.BackupName == JobName);
            if (ConfigurationBackUpViewModel == null) return new ConfigurationBackUpViewModel();
            ConfigurationBackUpViewModel.BackUpViewModels = ConfigurationBackUpViewModels.Select(x => new BackUpViewModel() { Id = x.Id, Name = x.BackupName }).ToList();
            ConfigurationBackUpViewModel.JobHistoryViewModels = GetJobHistoryViewModels(JobName);
            ConfigurationBackUpViewModel.FTPSetting.PassWord = string.IsNullOrEmpty(ConfigurationBackUpViewModel.FTPSetting.PassWord) ? string.Empty : Encryption.DecryptV2(ConfigurationBackUpViewModel.FTPSetting.PassWord);
            return ConfigurationBackUpViewModel;
        }

        /// <summary>
        /// Get viewhistory Job
        /// </summary>
        /// <param name="jobname"></param>
        /// <returns></returns>
        public List<JobHistoryViewModel> GetJobHistoryViewModels(string jobname)
        {
            if (_busConfigurationInformation.IsJob(jobname) == false) return new List<JobHistoryViewModel>();
            //var SqlParametersJobHistory = new List<SqlParameter>();
            //SqlParametersJobHistory.Add(new SqlParameter("@job_name", jobname));
            //var tesst = new {  };
            // var JobHistory = _context.Database.SqlQueryRaw<>("exec msdb.dbo.sp_help_jobhistory null, @job_name", new SqlParameter("job_name", jobname));

            List<JobHistoryViewModel> JobHistoryViewModels = new List<JobHistoryViewModel>();

            using (SqlConnection connection = new SqlConnection(Setting.ConnectionStrings))
            {
                connection.Open();

                string queryString = $"exec msdb.dbo.sp_help_jobhistory null, @job_name, null, null, null, null , null ,null, null , null , null , null , null , null, @mode";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    // Create a parameterized query
                    command.Parameters.AddWithValue("@job_name", jobname);
                    command.Parameters.AddWithValue("@mode", "FULL");
                    //command.Parameters.AddWithValue("@mode", "FULL");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var JobHistoryViewModel = new JobHistoryViewModel();
                            JobHistoryViewModel.instance_id = int.Parse(reader[0].ToString());
                            JobHistoryViewModel.job_id = Guid.Parse(reader[1].ToString());
                            JobHistoryViewModel.job_name = reader[2].ToString();
                            JobHistoryViewModel.message = reader[7].ToString();
                            JobHistoryViewModel.run_status = (RunStatus)int.Parse(reader[8].ToString());
                            JobHistoryViewModel.run_date = int.Parse(reader[9].ToString());
                            JobHistoryViewModel.run_time = int.Parse(reader[10].ToString());
                            JobHistoryViewModel.run_duration = int.Parse(reader[11].ToString());
                            JobHistoryViewModel.server = reader[16].ToString();
                            JobHistoryViewModels.Add(JobHistoryViewModel);
                        }
                    }
                }
            }
            //IQueryable<sysjobhistory> orders = _context.Database.SqlQuery<sysjobhistory>($"select *  from msdb.dbo.sysjobhistory");
            return JobHistoryViewModels;
        }
      
    }
}
