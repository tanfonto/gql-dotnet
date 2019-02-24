using System.Collections.Generic;
using System.Linq;
using Gqlpoc.Database;
using Gqlpoc.Database.Poco;
using NPoco;
using NPoco.Linq;

namespace Gqlpoc.Database.Repositories
{
    public class SqliteArtistRepository : IArtistRepository
    {
        private readonly IDatabase _db;

        public SqliteArtistRepository(IDatabase db)
        {
            _db = db;
        }

        public List<Album> GetAlbums(int artistId)
        {
            var result = _db.Query<Album>(
                        @"select 
                            AlbumId as Id,
                            Title 
                        from Albums
                        where Albums.ArtistId = @0", artistId);

            return result.ToList();
        }

        public Artist GetArtist(int id)
        {   
            var result = _db.Query<Artist>(
                    @"select 
                        ArtistId as Id,
                        Name 
                    from Artists
                    where Artists.ArtistId = @0", id)
                .SingleOrDefault();

            return result;
        }

        public IEnumerable<Artist> GetArtists()
        {
            var result = _db.Query<Artist>(
                    @"select 
                        ArtistId as Id,
                        Name 
                    from Artists");
            
            return result;
        }
    }
}
