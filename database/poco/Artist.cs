using System.Collections.Generic;
using PetaPoco.NetCore;

namespace Gqlpoc.Database.Poco
{
    [TableName("artists")]
    public class Artist
    {
        [Column("ArtistId")]
        public int Id { get; set; }

        public string Name { get; set; }

        [ResultColumn]
        public List<Album> Albums { get; set; }
    }
}