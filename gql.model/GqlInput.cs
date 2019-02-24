namespace Gqlpoc.Gql.Model
{
    public class GqlInput
    {
        public string OperationName { get; set; }

        public string NamedQuery { get; set; }
        
        public string Query { get; set; }
        
        public dynamic Variables { get; set; }
    }
}