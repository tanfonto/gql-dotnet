using Gqlpoc.Database.Repositories;
using Gqlpoc.Gql.Model.Query;
using Gqlpoc.Gql.Model.Utils;
using GraphQL.Types;

namespace Gqlpoc.Gql.Model.Mutation
{
    public class ArtistMutation : ObjectGraphType<object>
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistMutation(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
            
            Name = "Mutation";

            Field<ArtistType>(
                "createArtist",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ArtistInputType>> { Name = "artist" }
                ),
                resolve: ctx =>
                {
                    var artist = ctx.GetArgument<Artist>("artist");
                    
                    return new Artist{
                        Name = artist.Name                        
                    };
                }
            );
        }
    }
}