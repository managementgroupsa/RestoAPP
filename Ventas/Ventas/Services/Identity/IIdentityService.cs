using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Services.Identity
{

    interface IIdentityService
    {
        Task<bool> VerifyRegistration();
        Task Authenticate();
    }
}
