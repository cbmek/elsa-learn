using My.Elsa.Workflow.Workflows;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ELSA
builder.Services
    .AddElsa(opt =>
    {
        opt
            .AddActivity<SimpleWorkActivity>()
            .AddActivity<SimpleBlockingActivity>()
            //.AddActivitiesFrom<SimpleStateWorkflow>() // Register all activies from assmbly
            .AddWorkflow<SimpleStateWorkflow>();
    });
builder.Services
    .AddBookmarkProvider<SimpleUnblockBookmarkProvider>();

var app = builder.Build();

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