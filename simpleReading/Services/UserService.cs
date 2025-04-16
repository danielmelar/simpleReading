using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using simpleReading.Context;
using simpleReading.Interfaces;
using simpleReading.Models;

namespace simpleReading.Services
{
    public class UserService(
        AppDbContext context
        ) : IUserService
    {
        private readonly AppDbContext _context = context;

        public async Task<AuthenticationResult> GetUserByLoginCredentials(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(predicate: x => x.Email == email && x.Password == password);
            if (user == null) return new AuthenticationResult(false, "Usuário não encontrado! Verifique o email e senha.");
                
            return new AuthenticationResult(true, "Bem vindo!", user);
        }

        public async Task<AuthenticationResult> Create(User input)
        {
            var user = await _context.User.FirstOrDefaultAsync(
                predicate: x => x.Username == input.Username || x.Email == input.Email);
            if (user != null)
                return new AuthenticationResult(false, "Apelido e/ou email já em uso.");
            
            await _context.User.AddAsync(input);
            await _context.SaveChangesAsync();

            return new AuthenticationResult(true, "Usuário cadastrado com sucesso.", input);
        }
    }
}
