using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityApp.Api.Data;

namespace SecurityApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public DataController(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }


        [HttpGet]
        public IActionResult Get()
        {
            string title = "Behnam Hadipanah- KotamGroup.ir";
            IDataProtector dataProtector=_dataProtectionProvider.CreateProtector("DataController");
            string protectedTitle = dataProtector.Protect(title);
            string unProtectedTitle = dataProtector.Unprotect(protectedTitle);


            DataProtection data = new DataProtection();
            data.Title = title;
            data.ProtectedTitle = protectedTitle;
            data.UnProtectedTitle = unProtectedTitle;

            return Ok(data);
        }
    }
}
