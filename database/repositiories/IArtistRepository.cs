using System.Collections.Generic;
using Gqlpoc.Database.Poco;

namespace Gqlpoc.Database.Repositories
{
    public interface IArtistRepository
    {   
        Artist GetArtist(int id);
        
        List<Album> GetAlbums(int artistId);

        IEnumerable<Artist> GetArtists();
    }
}