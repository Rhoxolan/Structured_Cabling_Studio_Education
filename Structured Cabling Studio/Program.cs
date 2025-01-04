using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using StructuredCablingStudio.Binders.CalculationBinders;
using StructuredCablingStudio.Contexts;
using StructuredCablingStudio.Entities;
using StructuredCablingStudio.Filters.CalculationFilters;
using StructuredCablingStudio.Filters.LocalizationFilters;
using StructuredCablingStudio.Interceptors;
using StructuredCablingStudio.Services.CalculationServices.CalculationService;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(opt =>
{
	opt.ModelBinderProviders.Insert(0, new StructuredCablingStudioParametersModelBinderProvider());
	opt.ModelBinderProviders.Insert(0, new ConfigurationCalculateParametersModelBinderProvider());
	opt.ModelBinderProviders.Insert(0, new CalculateDTOModelBinderProvider());
})
	.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
	.AddDataAnnotationsLocalization();

builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddScoped<ConfigureSessionContextInterceptor>()
	.AddTransient<ICalculationService, CalculationService>()
	.AddScoped<SetLocalizationCookiesActionFilterAttribute>()
	.AddScoped<SetStructuredCablingStudioParametersActionFilterAttribute>()
	.AddScoped<SetConfigurationCalculateParametersActionFilterAttribute>()
	.AddScoped<SetCalculateDTOActionFilterAttribute>()
	.AddScoped<SetGetCalculateFormViewModelActionFilterAttribute>()
	.AddScoped<SetGetCalculateFormViewDataActionFilterAttribute>()
	.AddScoped<SetStrictComplianceWithTheStandartActionFilterAttribute>()
	.AddScoped<DiapasonActionFilterAttribute>()
	.AddScoped<StructuredCablingStudioParametersResultFilterAttribute>()
	.AddScoped<ConfigurationCalculateParametersResultFilterAttribute>()
	.AddScoped<CalculateDTOResultFilterAttribute>()
	.AddScoped<SetRecommendationsAvailabilityActionFilterAttribute>()
	.AddScoped<SetCalculationParametersActionFilterAttribute>()
	.AddDbContext<ApplicationContext>((sp, opt) =>
	{
		opt.UseSqlServer(builder.Configuration.GetConnectionString("StructuredCablingStudioDatabase"))
		.AddInterceptors(sp.GetRequiredService<ConfigureSessionContextInterceptor>());
	})
	.AddLocalization(opt => opt.ResourcesPath = "Resources")
	.Configure<RequestLocalizationOptions>(opt =>
	{
		var supportedCultures = new[]
		{
			new CultureInfo("ru"),
			new CultureInfo("en"),
			new CultureInfo("uk")
		};
		opt.DefaultRequestCulture = new RequestCulture("en");
		opt.SupportedCultures = supportedCultures;
		opt.SupportedUICultures = supportedCultures;
	})
	.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Calculation}/{action=Calculate}/{id?}");

app.Run();
