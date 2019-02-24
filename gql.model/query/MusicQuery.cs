using System.Linq;
using Gqlpoc.Database.Repositories;
using Gqlpoc.Gql.Model.Utils;
using GraphQL.Types;

namespace Gqlpoc.Gql.Model.Query
{
    public class MusicQuery : ObjectGraphType
    {
        private readonly IArtistRepository _artistRepository;
        
        public MusicQuery(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
            Define();
        }

        protected virtual void Define()
        {
            Field<ArtistType>(
                "artist",
                arguments: new QueryArguments(
                    new IntArgument { Name = "id" }
                ),
                resolve: ctx => {
                    var id = ctx.GetArgument<int>("id");
                    var artist = _artistRepository.GetArtist(id);
                    return new Artist
                    {
                        Id = artist.Id,
                        Name = artist.Name
                    };
                }
            );

            Field<ListGraphType<ArtistType>>(
                "artists",
                resolve: ctx => {
                    var artists = _artistRepository.GetArtists();
                    return artists.Select(a => 
                        new Artist
                        {
                            Id = a.Id,
                            Name = a.Name
                        }
                    );
                }
            );
        }
    }
}