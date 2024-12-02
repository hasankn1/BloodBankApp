using BloodBankMVC.Services;

namespace BloodBankMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register services in the DI container
            builder.Services.AddControllersWithViews();

            // Register your services for Dependency Injection
            builder.Services.AddScoped<DonationCenterService>(); // Add DonationCenterService
            builder.Services.AddScoped<BloodTypeService>(); // Add BloodTypeService

            // Register HttpClient services with base addresses
            builder.Services.AddHttpClient<BloodTypeService>(client =>
            {
                client.BaseAddress = new Uri("http://bloodb-recip-f7mi93nfbhap-411048257.ca-central-1.elb.amazonaws.com/"); // Update with correct URL
            });
            builder.Services.AddHttpClient<DonationCenterService>(client =>
            {
                client.BaseAddress = new Uri("http://bloodb-recip-f7mi93nfbhap-411048257.ca-central-1.elb.amazonaws.com/"); // Update with correct URL
            });

            // Register CORS policy to allow all origins, methods, and headers (be cautious in production)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Enable CORS with the policy "AllowAll"
            app.UseCors("AllowAll");

            // Other middlewares
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // Default route configuration
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Run the app
            app.Run();
        }
    }
}
