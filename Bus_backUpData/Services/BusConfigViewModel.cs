using Bus_backUpData.Data;
using Bus_backUpData.Func;
using Bus_backUpData.Interface;
using Bus_backUpData.Models;
using Bus_backUpData.ViewModels;
using HRM.SC.Core.Security;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bus_backUpData.Services.AutoModelMapper;

namespace Bus_backUpData.Services
{
    public class BusConfigViewModel : IBusConfigViewModel
    {

        public Context _context { get; set; }
        private IBusConfigurationBackUp _BusconfigurationBackUp { get; set; }
        public BusConfigViewModel(Context Context, IBusConfigurationBackUp busconfigurationBackUp)
        {
            _context = Context;
            Setting.DatabaseName = _context.Database.GetDbConnection().Database;
            _BusconfigurationBackUp = busconfigurationBackUp;   
        }
        /// <summary>
        /// Get all danh sach cau hinh
        /// </summary>
        /// <returns></returns>
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel()
        {
            var ConfigurationBackUp = _BusconfigurationBackUp.LoadJsonBackUp();
            var mapper = MapperConfig<ConfigurationBackUp, ConfigurationBackUpViewModel>.InitializeAutomapper();
            var ConfigurationBackUpViewModel = mapper.Map<List<ConfigurationBackUp>, List<ConfigurationBackUpViewModel>>(ConfigurationBackUp);
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
            if (IsJob(jobname) == false) return new List<JobHistoryViewModel>();
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
                            JobHistoryViewModel.job_id = Guid.Parse(reader[1].ToString());
                            JobHistoryViewModel.job_name = reader[2].ToString();
                            JobHistoryViewModel.run_status = int.Parse(reader[8].ToString());
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
        public bool IsJob(string JobName)
        {
            var SysJobListName = _context.Database.SqlQueryRaw<string>("select name from msdb.dbo.sysjobs").ToList();
            var SysJobName = SysJobListName.FirstOrDefault(x => x.ToLower() == JobName.ToLower());
            if (SysJobName == null) { return false; }
            return true;
        }
        
        
    }
}
