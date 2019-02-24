using System;
using NSubstitute;
using GraphQL.Types;
using GraphQL;

namespace Tests.Fixtures
{
    public class GraphQLFixture : IDisposable
    {
        public ISchema Schema { get; }       

        public IDocumentExecuter Doc { get; }

        public ExecutionErrors Errors { get; }

        public GraphQLFixture()
        {
            Schema = Substitute.For<ISchema>();
            Doc = Substitute.For<IDocumentExecuter>();
            Errors = new ExecutionErrors();
            Errors.Add(new ExecutionError("wrong"));
        }

        public void Dispose()
        {
            //noop
        }
    }
}