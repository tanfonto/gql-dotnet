using GraphQL.Types;

namespace Gqlpoc.Gql.Model.Query
{
    public class AlbumType : ObjectGraphType<Album>
    {
        public AlbumType()
        {
            Name = "Album";

            Field(x => x.Title).Description("Album title");
        }
    }
}