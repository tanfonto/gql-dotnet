using System.Collections.Generic;
using System.Threading.Tasks;
using Gqlpoc.Gql.Model;
using Gqlpoc.Gql.Model.Query;
using Gqlpoc.Web;   
using GraphQL;
using GraphQL.Types;
using Tests.Fixtures;
using Xunit;
using Xunit.Abstractions;
using Tests.TestData;

namespace Tests
{
    public class MusicQueryTests : IClassFixture<MusicQueryFixture>
    {
        private readonly MusicQueryFixture _fixture;
       
        private readonly ITestOutputHelper _output;

        public MusicQueryTests(MusicQueryFixture fixture, ITestOutputHelper output)
        {
            _output = output;
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(FullGraphMusicTestData))]
        [Trait("test", "unit")]
        public async Task WhenArtistIdIsProvided_ThenArtistOfThatIdIsReturned(
            string query,
            Dictionary<string, object> variables)
        {
                var gqlQuery = new GqlInput 
                {
                    Query = query,
                    Variables = variables
                };
            
            var result = await RunQueryAsync(gqlQuery);
            var artist = (Dictionary<string, object>)result["artist"];
            
            Assert.Equal(artist["id"], 1);
            Assert.Equal(artist["name"], "Meh");
        }

        [Theory]
        [ClassData(typeof(FullGraphMusicTestData))]
        [Trait("test", "unit")]
        public async Task WhenAlbumsFieldIsIncluded_ThenAlbumsAreReturned(
            string query,
            Dictionary<string, object> variables)
        {
                var gqlQuery = new GqlInput 
                {
                    Query = query,
                    Variables = variables
                };
            
            var result = await RunQueryAsync(gqlQuery);
            var artist = (Dictionary<string, object>)result["artist"];
            //because static typing is such a helpful thing and everyone should use it everywhere!
            var albums = ((object[])artist["albums"]);
       
            Assert.NotNull(artist["albums"]);
            Assert.Equal(albums.Length, 2);
        }

        [Theory]
        [ClassData(typeof(ArtistOnlyMusicTestData))]
        [Trait("test", "unit")]
        public async Task WhenAlbumsFieldIsNotIncluded_ThenAlbumsAreNotReturned(
            string query,
            Dictionary<string, object> variables)
        {
                var gqlQuery = new GqlInput 
                {
                    Query = query,
                    Variables = variables
                };
            
            var result = await RunQueryAsync(gqlQuery);
            var artist = (Dictionary<string, object>)result["artist"];
       
            Assert.False(artist.ContainsKey("albums"));
        }


        protected virtual async Task<dynamic> RunQueryAsync(GqlInput gqlQuery) 
        {
            var executionOptions = new ExecutionOptions
            {
                Schema = _fixture.Schema,
                Query = gqlQuery.Query,
                Inputs = gqlQuery.Variables == null ? null : new Inputs(gqlQuery.Variables)
            };

            return await _fixture.Executer
                .ExecuteAsync(executionOptions)
                .ContinueWith(x => (dynamic)x.Result.Data);
        }
    }
}