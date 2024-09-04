using backend.Models.Repositories.Interfaces;

namespace backend.Models
{
    public class AppSeed
    {
        private readonly IUserRepository _userRepository;

        public AppSeed(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void run()
        {
            _userRepository.CreateInitial();
        }

    }
}
