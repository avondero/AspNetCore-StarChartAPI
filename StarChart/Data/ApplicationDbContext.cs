namespace StarChart.Data
{
    #region Usings

    using Microsoft.EntityFrameworkCore;

    using StarChart.Models;

    #endregion

    public class ApplicationDbContext : DbContext
    {
        #region Constructeurs et destructeurs

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        #endregion

        #region Propriétés et indexeurs

        public DbSet<CelestialObject> CelestialObjects { get; set; }

        #endregion
    }
}
