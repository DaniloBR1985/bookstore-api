using BookStoreApi.DTOs;
using BookStoreApi.Entities;
using BookStoreApi.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookStoreApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _hasher;
        private readonly JwtService _jwtService;

        public AuthService(IUserRepository usuarioRepository, IPasswordHasher<User> hasher, JwtService jwtService)
        {
            _userRepository = usuarioRepository;
            _hasher = hasher;
            _jwtService = jwtService;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepository.ExistsAsync(dto.Username))
                throw new Exception("Usuário já existe.");

            var user = new User
            {
                Username = dto.Username
            };

            user.PasswordHash = _hasher.HashPassword(user, dto.Password);
            await _userRepository.AddAsync(user);

            return _jwtService.GenerateToken(user.Username, user.Role);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByUserAsync(dto.Username)
                       ?? throw new Exception("Usuário ou senha inválidos.");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Usuário ou senha inválidos.");

            return _jwtService.GenerateToken(user.Username, user.Role);
        }
    }
}
