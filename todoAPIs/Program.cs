
using Microsoft.AspNetCore.Mvc;

var builder= WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ItemRepository>();

var app = builder.Build();


app.MapGet("/items", ([FromServices] ItemRepository items) =>
{
    return items.GetAll();
});

// app.MapGet("/items/{id}", ([FromServices] ItemRepository items, int id) =>
// {
//     var result = items.GetById(id);
//     return result != null ? Results.Ok(result) :  Results.NotFound();
// });

app.MapGet("/items/{id}",([FromServices] ItemRepository items, int id)=>{
    var result= items.GetItem(id);

    return result!= null? Results.Ok(result): Results.NotFound();
});

app.MapPost("/items", ([FromServices] ItemRepository items, Item item) =>
{
    items.Add(item);
    return Results.Created($"/items/{item.Id}",item);
});

app.MapPut("/items/{id}", ([FromServices] ItemRepository items, int id, Item item) =>
{
    if (items.GetItem(id) == null)
    {
        return Results.NotFound();
    }

    items.Update(item);
    return Results.Ok(item);
});


app.MapDelete("/items/{id}", ([FromServices] ItemRepository items, int id) =>
{
    if (items.GetItem(id) == null)
    {
        return Results.NotFound();
    }

    items.Delete(id);
    return Results.NoContent();
});

app.Run();

record Item (int Id, string title, bool isCompleted);

class ItemRepository
{
    private readonly 
     Dictionary<int, Item> _items = new Dictionary<int, Item>();

     public ItemRepository()
     {
         var item1 = new Item(1, "Go to the gym", false);
        var item2 = new Item(2, "Buy bread", false);
        var item3 = new Item(3, "Watch TV ", false);
        _items.Add(item1.Id, item1);
        _items.Add(item2.Id, item2);
        _items.Add(item3.Id, item3);
     }

     public IEnumerable<Item> GetAll() => _items.Values;
     public Item GetItem(int id) => _items[id];
     public void Add(Item item) {
         _items.Add(item.Id, item);
    }
    public void Update(Item item) => _items[item.Id] = item;

    public void Delete(int id) => _items.Remove(id);
}






// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateTime.Now.AddDays(index),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");


// app.MapGet("/items", ([FromServices] ItemRepository items) =>
// {
//     return items.GetAll();
// });

// app.Run();

// record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }

// record Item (int Id, string title, bool isCompleted);

// class ItemRepository
// {
//     private readonly 
//      Dictionary<int, Item> _items = new Dictionary<int, Item>();

//      public ItemRepository()
//      {
//          var item1 = new Item(1, "Go to the gym", false);
//         var item2 = new Item(2, "Buy bread", false);
//         var item3 = new Item(3, "Watch TV ", false);
//         _items.Add(item1.Id, item1);
//         _items.Add(item2.Id, item2);
//         _items.Add(item3.Id, item3);
//      }

//      public IEnumerable<Item> GetAll() => _items.Values;
//      public Item GetItem(int id) => _items[id];
//      public void Add(Item item) {
//          _items.Add(item.Id, item);
//     }
//     public void Update(Item item) => _items[item.Id] = item;

//     public void Delete(int id) => _items.Remove(id);
// }