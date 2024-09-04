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
using backend.Models.Repositories.Interfaces;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(opt => {
                        var sp = opt.ApplicationServices;
                        using (var scope = sp.CreateScope())
                        {
                            var user = scope.ServiceProvider.GetService<IUserRepository>();
                            new AppSeed(user).run();
                        }
                    });
                });
    }
}