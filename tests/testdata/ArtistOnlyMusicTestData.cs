using System.Collections;
using System.Collections.Generic;
using Gqlpoc.Gql.Model;

namespace Tests.TestData
{
    public class ArtistOnlyMusicTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator() 
        {
            yield return new object[]  
            {
                @"query Music($id: Int!) { 
                    artist(id: $id) { 
                        id 
                        name 
                    } 
                }",
                new Dictionary<string, object>
                {
                    ["id"] = 1
                }
            };

            yield return new object [] 
            {
                @"query { 
                    artist(id: 1) { 
                        id 
                        name 
                    } 
                }",
                null
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}