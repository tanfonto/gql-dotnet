using System.Collections.Generic;
using Gqlpoc.Database.Poco;
using Gqlpoc.Database.Repositories;

namespace Tests.TestData
{
    public class InMemoryArtistRepository : IArtistRepository
    {
        public List<Album> GetAlbums(int artistId)
        {
            return new List<Album> 
            {
                new Album 
                {
                    Title = "First album"    
                },
                new Album 
                {
                    Title = "Second album"    
                }
            };
        }

        public Artist GetArtist(int id)
        {
            return new Artist 
            {
                Id = 1,
                Name = "Meh"
            };
        }

        public IEnumerable<Artist> GetArtists()
        {
            throw new System.NotImplementedException();
        }
    }
}