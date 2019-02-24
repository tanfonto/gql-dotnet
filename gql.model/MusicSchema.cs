using System;
using Gqlpoc.Gql.Model.Mutation;
using Gqlpoc.Gql.Model.Query;
using GraphQL.Types;

namespace Gqlpoc.Gql.Model 
{
    public class MusicSchema : Schema
    {
        public MusicSchema(
            MusicQuery query,
            ArtistMutation mutation, 
            IGraphType[] relatedTypes)
        {
            RegisterTypes(relatedTypes);
            Query = query;
            Mutation = mutation;
        }
    }
}