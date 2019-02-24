using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Gqlpoc.Database.Repositories;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using NPoco;
using System.Data.Common;
using Gqlpoc.Gql.Model;
using Gqlpoc.Gql.Model.Query;
using Gqlpoc.Gql.Model.Mutation;
using Gql.Model;
using DatabaseContext = NPoco.Database;

namespace Gqlpoc.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }   
        
        //The whole idea of expecting AspNet runtime to dispose your resources is flawed.
        //This is especially relevent for db connections, can use it that way as its only 
        //a PoC but DON'T DO IT AT HOME!
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<DbConnection>(_ => {
                var conn = new SqliteConnection(Configuration["connectionStrings:sqlite"]);
                conn.Open();
                return conn;
            });
            services.AddScoped<IDatabase, DatabaseContext>();
            services.AddScoped<IArtistRepository, SqliteArtistRepository>();
            
            this.ConfigureGqlTypes(services);
        }

        protected virtual void ConfigureGqlTypes(IServiceCollection services) 
        {
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.Scan(scan => 
            {
                scan.FromAssemblyOf<IAssemblyInfo>()
                    .AddClasses(classes => classes.AssignableTo(typeof(ObjectGraphType<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
                
                scan.FromAssemblyOf<IAssemblyInfo>()
                    .AddClasses(classes => classes.AssignableTo(typeof(InputObjectGraphType)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.AddScoped<MusicQuery>();
            services.AddScoped<ArtistMutation>();

            var provider = services.BuildServiceProvider();
            var schemaTypes = provider.GetServices<IGraphType>();
            services.AddScoped<ISchema>(_ => 
                new MusicSchema(
                    provider.GetService<MusicQuery>(), 
                    provider.GetService<ArtistMutation>(), 
                    schemaTypes.ToArray()
                ));
            }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseDefaultFiles();      
            app.UseStaticFiles();
        }
    }
}
