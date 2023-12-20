using Bus_backUpData.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AdminLayout_Vuexy.Components
{
    public class MenuConnectDatabaseViewComponent : ViewComponent
    {
        private readonly IBusConfigServer _busConfig;
        public MenuConnectDatabaseViewComponent(IBusConfigServer busConfig) {
            _busConfig = busConfig;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = _busConfig.GetServerConnectViewModel();
            return View("MenuConnectDatabase", viewModel);
        }
    }
}
