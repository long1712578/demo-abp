using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace TodoApp.Controllers
{
    [Route("api/localization-demo")]
    public class DemoLocalizationController : AbpController
    {
        private readonly IStringLocalizer<DemoLocalizationController> _localizer;

        public DemoLocalizationController(IStringLocalizer<DemoLocalizationController> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet("hello")]
        public string GetHello()
        {
            // Key "Hello" sẽ được dịch theo ngôn ngữ hiện tại
            return _localizer["Hello"];
        }
    }
}
