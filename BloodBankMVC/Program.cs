using BloodBankMVC.Services;

namespace BloodBankMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            // Register your services for Dependency Injection
            builder.Services.AddScoped<DonationCenterService>(); // Add DonationCenterService
            builder.Services.AddScoped<BloodTypeService>(); // Add BloodTypeService
            builder.Services.AddHttpClient<BloodTypeService>(client =>
            {
                client.BaseAddress = new Uri("http://bloodb-recip-f7mi93nfbhap-411048257.ca-central-1.elb.amazonaws.com/"); // Replace with the APIGee actual API proxy? URL
            });
            builder.Services.AddHttpClient<DonationCenterService>(client =>
            {
                client.BaseAddress = new Uri("http://bloodb-recip-f7mi93nfbhap-411048257.ca-central-1.elb.amazonaws.com/"); // Replace with the APIGee actual API proxy? URL
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

            // Register services
            builder.Services.AddControllersWithViews();
            
        }

    }
}
