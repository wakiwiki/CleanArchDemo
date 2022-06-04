using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infra.Data.Context
{
    public class UniversityIdentityDBContext : IdentityDbContext
    {
        public UniversityIdentityDBContext(DbContextOptions<UniversityIdentityDBContext> options)
            : base(options)
        {
        }
    }
}
