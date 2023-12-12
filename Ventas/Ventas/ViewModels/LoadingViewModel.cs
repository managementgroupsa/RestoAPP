using RestoPLUS.Services.Identity;
using RestoPLUS.Services.Routing;
using Splat;
using RestoPLUS.Views;

namespace RestoPLUS.ViewModels
{
    class LoadingViewModel : BaseViewModel
    {
        private readonly IRoutingService routingService;
        private readonly IIdentityService identityService;

        public LoadingViewModel(IRoutingService routingService = null, IIdentityService identityService = null)
        {
            this.routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            this.identityService = identityService ?? Locator.Current.GetService<IIdentityService>();
        }

        // Called by the views OnAppearing method
        public async void Init()
        {
            var isAuthenticated = await this.identityService.VerifyRegistration();
            if (isAuthenticated)
            {
                await this.routingService.NavigateTo("///DefaultPage");
            }
            else
            {
                await this.routingService.NavigateTo(nameof(LoginPage));
            }
        }
    }
}
