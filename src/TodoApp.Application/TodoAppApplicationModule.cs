using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApp;

[DependsOn(
    typeof(TodoAppDomainModule),
    typeof(TodoAppApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(KafkaFlowIntegrationModule)
    )]
public class TodoAppApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TodoAppApplicationModule>();
        });

        // Đăng ký các service cho HttpClient
        context.Services.AddHttpClient();
        context.Services.AddTransient<Application.Http.IHttpClientService, Application.Http.HttpClientService>();
        context.Services.AddTransient<Application.Http.ISevagoApiClient, Application.Http.SevagoApiClient>();
    }
}
