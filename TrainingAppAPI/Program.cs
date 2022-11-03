using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TrainingAppAPI.Schema;
using TrainingAppAPI.Services;
using TrainingAppAPI.Services.Templates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

builder.Services.AddInMemorySubscriptions();

builder.Services.AddPooledDbContextFactory<TrainingDbContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<TemplateRepository>();

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    IDbContextFactory<TrainingDbContext> contextFactory =
        scope.ServiceProvider.GetRequiredService<IDbContextFactory<TrainingDbContext>>();

    using(TrainingDbContext context = contextFactory.CreateDbContext())
    {
        context.Database.Migrate();
    }
}

app.UseRouting();
app.UseWebSockets();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

