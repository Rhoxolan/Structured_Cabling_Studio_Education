using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StructuredCablingStudio.Contexts;
using StructuredCablingStudio.Entities;
using StructuredCablingStudio.Interceptors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddScoped<ConfigureSessionContextInterceptor>()
	.AddDbContext<ApplicationContext>((sp, opt) =>
	{
		opt.UseSqlServer(builder.Configuration.GetConnectionString("CablingConfigurationsDB"))
		.AddInterceptors(sp.GetRequiredService<ConfigureSessionContextInterceptor>());
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
