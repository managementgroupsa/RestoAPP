using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ventas.Services.Identity
{
    class IdentityServiceStub : IIdentityService
    {
        public Task Authenticate()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerifyRegistration()
        {
            await Task.Delay(1000);

            string cToken = Application.Current.Properties["Token"] as string;

            if (!string.IsNullOrEmpty(cToken))
                return true;
            else
                return false;

        }
    }
}
