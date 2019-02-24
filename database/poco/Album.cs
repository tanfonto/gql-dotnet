using System.Collections.Generic;
using PetaPoco.NetCore;

namespace Gqlpoc.Database.Poco
{
    [TableName("albums")]
    public class Album
    {
        [Column("AlbumId")]
        public int Id { get; set; }

        public string Title { get; set; }

        public Artist Artist { get; set; }
    }
}