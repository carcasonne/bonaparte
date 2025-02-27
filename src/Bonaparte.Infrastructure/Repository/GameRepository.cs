using System.Security.Claims;
using Bonaparte.Core;
using Bonaparte.Core.EntityFramework;
using Bonaparte.Core.Interfaces.Repository;
using Bonaparte.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Bonaparte.Infrastructure.Repository;

public class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthorizationService _authorizationService;
    private readonly ClaimsPrincipal _user;

    public GameRepository(
        ApplicationDbContext context, 
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _authorizationService = authorizationService;
        _user = httpContextAccessor.HttpContext?.User ?? throw new InvalidOperationException("User context not available");
    }

    public async Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return await _context.Games.ToListAsync();
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await _context.Games.FindAsync(id);
    }

    public async Task<Game> CreateGameAsync(Game game)
    {
        // Check if user can create games
        var authResult = await _authorizationService.AuthorizeAsync(
            _user, null, AuthConstants.Policies.CanCreateGames);
            
        if (!authResult.Succeeded)
        {
            throw new UnauthorizedAccessException("User is not authorized to create games");
        }

        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task UpdateGameAsync(Game game)
    {
        // Check if user is admin or game creator
        var authResult = await _authorizationService.AuthorizeAsync(
            _user, null, AuthConstants.Policies.CanCreateGames);
            
        if (!authResult.Succeeded)
        {
            throw new UnauthorizedAccessException("User is not authorized to update games");
        }

        _context.Entry(game).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGameAsync(int id)
    {
        // Admin only operation
        var authResult = await _authorizationService.AuthorizeAsync(_user, 
            null, 
            AuthConstants.Policies.RequireAdmin);
            
        if (!authResult.Succeeded)
        {
            throw new UnauthorizedAccessException("User is not authorized to delete games");
        }

        var game = await _context.Games.FindAsync(id);
        if (game != null)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }
}