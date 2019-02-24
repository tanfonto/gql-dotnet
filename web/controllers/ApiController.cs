using System;
using System.Threading.Tasks;
using Gqlpoc.Database.Repositories;
using GraphQL;
using GraphQL.Types;
using Gqlpoc.Gql.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Gqlpoc.Web.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {   
        private IDocumentExecuter _documentExecuter { get; set; }

        private readonly ISchema _schema;

        public ApiController(IDocumentExecuter documentExecuter, ISchema schema)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GqlInput input)
        {
            if (input == null) return BadRequest("Missing query content");

            var executionOptions = new ExecutionOptions 
            { 
                Schema = _schema, 
                Query = input.Query,
                Inputs = GraphQL.StringExtensions.ToInputs(input.Variables) 
            };  
            try
            {
                var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

                if (result.Errors?.Count > 0) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }        
        }
    }
}
