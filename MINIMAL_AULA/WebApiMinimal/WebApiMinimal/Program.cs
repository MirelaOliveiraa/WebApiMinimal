using Microsoft.EntityFrameworkCore;
using WebApiMinimal.Contexto;
using WebApiMinimal.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<Contexto>(options => options.UseSqlServer(
    "Server=172.16.1.119;Database=ZOOLOGICO;Uid=mirela;Pwd=123456;"
    ));


builder.Services.AddSwaggerGen();
builder.Services.AddCors();


var app = builder.Build();

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());


app.UseSwagger();

app.MapPost("AdicionaCarro", async (Carro carro, Contexto contexto) =>
{
    contexto.Carro.Add(carro);
    await contexto.SaveChangesAsync();
}
);

app.MapDelete("ExcluiCarro/{id}", async (int id, Contexto contexto) =>
{
    var carroExcluir = await contexto.Carro.FirstOrDefaultAsync(c => c.Id == id);

    if (carroExcluir != null)
    {

        contexto.Carro.Remove(carroExcluir);
        await contexto.SaveChangesAsync();
    }
}
);

app.MapGet("ListarCarros", async (Contexto contexto) =>
{

    return await contexto.Carro.ToArrayAsync();

}
);

app.MapPost("obterCarro/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Carro.FirstOrDefaultAsync(c => c.Id == id);

}
);

app.MapPut("AttCarro/{id}", async (int id, Contexto contexto, Carro carro) =>
{
    var carroUpdate = await contexto.Carro.FindAsync(id);

    if (carroUpdate != null)
    {
        carroUpdate.Nome = carro.Nome;
        carroUpdate.Comentario = carro.Comentario;

        //contexto.Update(carroUpdate);
        await contexto.SaveChangesAsync();
    }
}
);

app.UseSwaggerUI();
app.Run();
