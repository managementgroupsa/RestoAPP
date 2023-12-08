using RestoAPP.Services.Identity;
using RestoAPP.Services.Routing;
using Splat;
using RestoAPP.Views;

namespace RestoAPP.ViewModels
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
