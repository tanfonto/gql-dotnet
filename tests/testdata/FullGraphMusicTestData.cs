using System.Collections;
using System.Collections.Generic;
using Gqlpoc.Gql.Model;

namespace Tests.TestData
{
    public class FullGraphMusicTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator() 
        {
            yield return new object[]  
            {
                @"query Music($id: Int!) { 
                    artist(id: $id) { 
                        id 
                        name 
                        albums { 
                            title 
                        } 
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
                        albums { 
                            title 
                        } 
                    } 
                }",
                null
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}