using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;

namespace TorchFireFilms.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(GetNewTableName(entity.GetTableName()));

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(property.GetColumnName()));
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(key.GetName()));
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(key.GetConstraintName()));
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(index.GetDatabaseName()));
                }
            }

            builder.Entity<ApplicationUser>().Property(u => u.CreatedDate).HasDefaultValueSql("now()");
            builder.Entity<ApplicationUser>().Property(u => u.ModifiedDate).HasDefaultValueSql("now()");

            builder.Entity<ApplicationRole>().Property(u => u.CreatedDate).HasDefaultValueSql("now()");
            builder.Entity<ApplicationRole>().Property(u => u.ModifiedDate).HasDefaultValueSql("now()");

            builder.Entity<ApplicationUserClaim>().Property(u => u.CreatedDate).HasDefaultValueSql("now()");
            builder.Entity<ApplicationUserClaim>().Property(u => u.ModifiedDate).HasDefaultValueSql("now()");
            builder.Entity<ApplicationUserClaim>().Property(u => u.Deleted).HasDefaultValueSql("false");

            builder.Entity<ApplicationUserRole>().Property(u => u.CreatedDate).HasDefaultValueSql("now()");
            builder.Entity<ApplicationUserRole>().Property(u => u.ModifiedDate).HasDefaultValueSql("now()");
            builder.Entity<ApplicationUserRole>().Property(u => u.Deleted).HasDefaultValueSql("false");

            builder.Entity<ApplicationRoleClaim>().Property(u => u.CreatedDate).HasDefaultValueSql("now()");
            builder.Entity<ApplicationRoleClaim>().Property(u => u.ModifiedDate).HasDefaultValueSql("now()");
            builder.Entity<ApplicationRoleClaim>().Property(u => u.Deleted).HasDefaultValueSql("false");
        }

        private string GetNewTableName(string name)
        {
            return NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(
                name.TrimEnd('s').Replace("AspNet", "Identity"));
        }
    }
}
