using System.Linq;
using Gqlpoc.Database.Repositories;
using GraphQL.Types;

namespace Gqlpoc.Gql.Model.Query
{
    public class ArtistType : ObjectGraphType<Artist>
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistType(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
            Define();
        }

        protected virtual void Define() 
        {
            Name = "Artist";

            Field(x => x.Id).Description("The Id of the artist");
            Field(x => x.Name).Description("Artist name");
            Field<ListGraphType<AlbumType>>(
                "albums",
                resolve: ctx =>
                {
                    var albums = _artistRepository.GetAlbums(ctx.Source.Id);
                    return albums.Select(x => {
                        return new Album 
                        {
                            Title = x.Title
                        };
                    });
                });
        }
    }
}