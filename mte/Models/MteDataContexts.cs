namespace mte.Models
{
    using System.Data.Entity;

    public class MteDataContexts : DbContext
    {
        public MteDataContexts() : base("DefaultConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Routes>().HasRequired(p => p.PointStart).WithMany().HasForeignKey(k => k.PointStartId);
            modelBuilder.Entity<Routes>().HasRequired(p => p.PointStop).WithMany().HasForeignKey(k => k.PointStopId);
        }

        // Main
        public DbSet<Enterprises> Enterprises { get; set; }

        // Employers
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Employers> Employers { get; set; }

        // Cars
        public DbSet<CarBrands> CarBrands { get; set; }
        public DbSet<CarTypes> CarTypes { get; set; }
        public DbSet<Cars> Cars { get; set; }

        // Routes
        public DbSet<PointTypes> PointTypes { get; set; }
        public DbSet<Points> Points { get; set; }
        public DbSet<RouteTypes> RouteTypes { get; set; }
        public DbSet<Routes> Routes { get; set; }
        public DbSet<RoutePoints> RoutePoints { get; set; }

        // Boards
        public DbSet<WorkTypes> WorkTypes { get; set; }
        public DbSet<Boards> Boards { get; set; }
        public DbSet<BoardFlightLists> BoardFlightLists { get; set; }

        // WayBills
        public DbSet<WayBillStatuses> WayBillStatuses { get; set; }
        public DbSet<WayBills> WayBills { get; set; }
        public DbSet<WayBillTeams> WayBillTeams { get; set; }
        public DbSet<WayBillFlightLists> WayBillFlightLists { get; set; }

        // Smena
        public DbSet<Smenes> Smenes { get; set; }
    }
}