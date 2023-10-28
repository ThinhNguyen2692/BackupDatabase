using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels.ViewModelIdentity
{
    public class ResultLoginViewModel
    {
        public bool Succeeded { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsChangPassWord { get; set; }
        public string Area {  get; set; }
        public string UserId {  get; set; }
        public string Code {  get; set; }
    }
}
