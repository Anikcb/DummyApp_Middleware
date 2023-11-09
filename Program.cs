var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Using "Use" and "Run"

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("MiddleWare1 Started!!");
//     await context.Response.WriteAsync("First Delegate!!");
//     await next.Invoke();
//     Console.WriteLine("MiddkeWare1 Ended!!");
// });

// app.Run(async context =>
// {
//     Console.WriteLine("MiddleWare2 Started!!");
//     await context.Response.WriteAsync("\nSecond Delegate!!");
// });


// Using "Map"

// app.Map("/middleware-map1", HandleMapTest1);
// app.Map("/middleware-map2", HandleMapTest2);

// app.Run(async context =>
// {
//     await context.Response.WriteAsync("Non-Map Delegate!!");
// });

// static void HandleMapTest1(IApplicationBuilder app)
// {
//     app.Run(async context =>
//     {
//         Console.WriteLine("Map1 Called");
//         await context.Response.WriteAsync("Map Test 1");
//     });
// }

// static void HandleMapTest2(IApplicationBuilder app)
// {
//     app.Run(async context =>
//     {
//         await context.Response.WriteAsync("Map Test 2");
//     });
// }


// Using "MapWhen"

app.MapWhen(Context => Context.Request.Query.ContainsKey("branch"), HandleBranch);

app.Run(async context =>
{
    await context.Response.WriteAsync("Non-Map Delegate!!");
});

static void HandleBranch(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        var branchVer = context.Request.Query["branch"];
        await context.Response.WriteAsync($"Branch used = {branchVer}");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
