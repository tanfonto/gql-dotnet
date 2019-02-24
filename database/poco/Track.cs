using PetaPoco.NetCore;

namespace Gqlpoc.Database.Poco
{
    [TableName("tracks")]
    public class Track
    {
        [Column("TrackId")]
        public int Id { get; set; }

        public string Name { get; set; }

        public Album Album { get; set; }

        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int Bytes { get; set; }

        public decimal UnitPrice { get; set; }
    }
}