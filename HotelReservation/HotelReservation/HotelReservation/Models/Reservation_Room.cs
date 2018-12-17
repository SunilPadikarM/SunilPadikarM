namespace HotelReservation.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Reservation_Room : DbContext
    {
        public Reservation_Room()
            : base("name=Reservation_Room1")
        {
        }

        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .Property(e => e.RoomType)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.RoomName)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);
        }
    }
}
