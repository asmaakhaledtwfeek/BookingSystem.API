using BookingSystem.Domin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.Data.Config
{
    public class BookingRoomConfig : IEntityTypeConfiguration<BookingRoom>
    {
        public void Configure(EntityTypeBuilder<BookingRoom> builder)
        {
            builder.HasKey(x => new { x.BookingId, x.RoomId });
        }
    }
}
