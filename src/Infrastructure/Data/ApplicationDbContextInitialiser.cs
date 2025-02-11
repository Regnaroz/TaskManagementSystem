using System.Runtime.InteropServices;
using SampleProject.Domain.Constants;
using SampleProject.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleProject.Domain.Enums;

namespace SampleProject.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var roles = new List<ApplicationRole>()
        {
            new()
            {
               Name = Roles.Administrator,
               EnumRoleId = RoleEnum.Administrator
            },
             new()
            {
               Name = Roles.User,
               EnumRoleId = RoleEnum.User
            }

        };
        foreach (var role in roles)
        {
            if (_roleManager.Roles.All(r => r.EnumRoleId != role.EnumRoleId))
            {
                await _roleManager.CreateAsync(role);
            }
        }

        var administratorRole = roles.Where(r => r.EnumRoleId == RoleEnum.Administrator).First();
        var userRole = roles.Where(r => r.EnumRoleId == RoleEnum.User).First();
        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost",FirstName = "Admin" , LastName = "Admin" };
        var userId = string.Empty;
        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            userId = _userManager.Users.First(c => c.UserName!.Equals(administrator.UserName)).Id;
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        if (!_context.Tasks.Any())
            {
                _context.Tasks.Add(new UserTask()
                {
                    Title = "First Task",
                    Description="First Task Description",
                    Status = Status.NotStarted,
                    Priority = PriorityLevel.Medium,
                    DueDate = new DateTime(2025,12,31),
                    UserId = userId
                });

                await _context.SaveChangesAsync();
            }
    }
}
