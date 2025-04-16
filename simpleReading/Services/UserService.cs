using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using simpleReading.Context;
using simpleReading.Interfaces;
using simpleReading.Models;
using static BCrypt.Net.BCrypt;

namespace simpleReading.Services
{
    public class UserService(
        AppDbContext context
        ) : IUserService
    {
        private readonly AppDbContext _context = context;

        public async Task<AuthenticationResult> GetUserByLoginCredentials(string emailOrUsername, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(predicate: x => x.Email == emailOrUsername.ToLower().Trim() || x.Username == emailOrUsername);
            if (user == null || !VerifyPasswd(password, user.Password)) return new AuthenticationResult(false, "Usuário não encontrado! Verifique o email e senha.");
                
            return new AuthenticationResult(true, "Bem vindo!", user);
        }

        public async Task<AuthenticationResult> Create(User input)
        {
            var user = await _context.User.FirstOrDefaultAsync(
                predicate: x => x.Username == input.Username.ToLower().Trim() || x.Email == input.Email.ToLower().Trim());
            if (user != null)
                return new AuthenticationResult(false, "Apelido e/ou email já em uso.");

            input.Password = HashPasswd(input.Password);
            
            await _context.User.AddAsync(input);
            await _context.SaveChangesAsync();

            return new AuthenticationResult(true, "Usuário cadastrado com sucesso.", input);
        }

        public string HashPasswd(string password)
        {
            return HashPassword(password);
        }

        public bool VerifyPasswd(string passwd, string hash)
        {
            return Verify(passwd, hash);
        }


    }
}
