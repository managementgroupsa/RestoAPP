using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPP.Services.Identity
{

    interface IIdentityService
    {
        Task<bool> VerifyRegistration();
        Task Authenticate();
    }
}
