/**
 * This file is part of the Sandy Andryanto Blog Application.
 *
 * @author     Sandy Andryanto <sandy.andryanto.blade@gmail.com>
 * @copyright  2024
 *
 * For the full copyright and license information,
 * please view the LICENSE.md file that was distributed
 * with this source code.
 */

using backend.Models;
using backend.Models.DTO;
using backend.Models.Repositories.Implements;
using backend.Models.Repositories.Interfaces;
using backend.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                options
                .UseSqlServer(Configuration.GetConnectionString("MainContext"))
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .EnableSensitiveDataLogging()
           );
           services.Configure<SettingDTO>(Configuration.GetSection("AppSettings"));
           services.AddScoped<IActivityRepository, ActivityService>();
           services.AddScoped<IArticleRepository, ArticleService>();
           services.AddScoped<ICommentRepository, CommentService>();
           services.AddScoped<INotificationRepository, NotificationService>();
           services.AddScoped<IUserRepository, UserService>();
           services.AddScoped<IViewerRepository, ViewerService>();
           services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            app.UseRouting();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Uploads")),
                RequestPath = "/Uploads"
            });

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthorization();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(x => x.MapControllers());
        }

    }
}
