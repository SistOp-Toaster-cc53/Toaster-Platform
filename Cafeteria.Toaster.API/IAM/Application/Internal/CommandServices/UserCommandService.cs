using ACME.LearningCenterPlatform.API.IAM.Application.Internal.OutboundServices;
using ACME.LearningCenterPlatform.API.IAM.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.IAM.Domain.Model.Commands;
using ACME.LearningCenterPlatform.API.IAM.Domain.Repositories;
using ACME.LearningCenterPlatform.API.IAM.Domain.Services;
using Cafeteria.Toaster.API.Shared.Domain.Repositories;
using System.Threading.Tasks;
using System;

namespace ACME.LearningCenterPlatform.API.IAM.Application.Internal.CommandServices
{
    /// <summary>
    /// The user command service
    /// </summary>
    /// <remarks>
    /// This class is used to handle user commands.
    /// </remarks>
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IHashingService _hashingService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the UserCommandService.
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="tokenService">The token service</param>
        /// <param name="hashingService">The hashing service</param>
        /// <param name="unitOfWork">The unit of work</param>
        public UserCommandService(
            IUserRepository userRepository,
            ITokenService tokenService,
            IHashingService hashingService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _hashingService = hashingService ?? throw new ArgumentNullException(nameof(hashingService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Handle sign in command
        /// </summary>
        /// <param name="command">The sign in command</param>
        /// <returns>The authenticated user and the JWT token</returns>
        public async Task<(User user, string token)> Handle(SignInCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var user = await _userRepository.FindByUsernameAsync(command.Username);

            if (user == null || !_hashingService.VerifyPassword(command.Password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password");
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                throw new ArgumentNullException(nameof(user.Username));
            }

            var token = _tokenService.GenerateToken(user);

            return (user, token);
        }

        /// <summary>
        /// Handle sign up command
        /// </summary>
        /// <param name="command">The sign up command</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(SignUpCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            
            if (String.IsNullOrEmpty(command.Username) || String.IsNullOrEmpty(command.Password))
            {
                throw new Exception("Username or password cannot be null");
            }

            if (_userRepository.ExistsByUsername(command.Username))
            {
                throw new Exception($"Username {command.Username} is already taken");
            }

            var hashedPassword = _hashingService.HashPassword(command.Password);
            var user = new User(command.Username, hashedPassword);

            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred while creating user: {e.Message}");
            }
        }
    }
}
