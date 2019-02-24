using GraphQL.Types;
using Gqlpoc.Gql.Model.Query;

namespace Gqlpoc.Gql.Model.Mutation
{
    public class ArtistInputType : InputObjectGraphType
    {
        public ArtistInputType()
        {
            Name = "ArtistInput";
            
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}