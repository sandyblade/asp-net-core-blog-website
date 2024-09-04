using backend.Models.Entities;
using backend.Models.Repositories.Interfaces;

namespace backend.Models.Repositories.Implements
{
    public class UserService : IUserRepository
    {
        private readonly AppDbContext _db;
        private static string defaultPassword = "P@ssw0rd!123";
        private static int maxUser = 10;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public void CreateInitial()
        {
            int Total = _db.User.Count();
            int Max = maxUser;

            if (Total == 0)
            {
                for(int i = 1; i <= Max; i++)
                {
                    List<String> Genders = new List<string> { "M", "F" };
                    string Gender = Genders.OrderBy(s => Guid.NewGuid()).First();
                    string JobName = JobTitle.GetData().OrderBy(s => Guid.NewGuid()).First();
                    string Email = Faker.Internet.Email();
                    string Password = BCrypt.Net.BCrypt.HashPassword(defaultPassword);
                    User user = new User() { Email = Email, Password = Password };
                    user.Phone = Faker.Phone.Number().ToString();
                    user.Confirmed = 1;
                    user.FirstName = Faker.Name.First();
                    user.LastName = Faker.Name.Last();
                    user.Gender = Gender;
                    user.JobTitle = JobName;
                    user.Facebook = Faker.Internet.UserName();
                    user.LinkedIn = Faker.Internet.UserName();
                    user.Twitter = Faker.Internet.UserName();
                    user.Instagram = Faker.Internet.UserName();
                    user.Country = Faker.Address.Country();
                    user.Address = Faker.Address.StreetAddress();
                    user.AboutMe = Faker.Lorem.Paragraph();
                    _db.Add(user);
                }
                _db.SaveChanges();
            }

        }
    }
}
