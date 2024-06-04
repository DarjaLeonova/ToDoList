// using DAL.Data;
// using Microsoft.EntityFrameworkCore;
//
// namespace PL;
//
// public class Startup
// {
//     public Startup(IConfiguration configuration)
//     {
//         Configuration = configuration;
//     }
//     
//     public IConfiguration Configuration { get; }
//
//     public void ConfigureServices(IServiceCollection services)
//     {
//         services.AddDbContext<ApplicationDbContext>(options =>
//             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
//
//         services.AddControllersWithViews();
//     }
// }