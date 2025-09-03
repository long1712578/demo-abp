using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TodoApp.Controllers;

/* Inherit your controllers from this class.
 */
[Microsoft.AspNetCore.Components.Route("api/localization-demo")]
public abstract class TodoAppController : AbpControllerBase
{
    protected TodoAppController()
    {
        LocalizationResource = typeof(TodoAppResource);
    }
    [HttpGet("hello")]
    public string GetHello()
    {
        // Key "Hello" sẽ được dịch theo ngôn ngữ hiện tại
        return "hello";
    }
}
