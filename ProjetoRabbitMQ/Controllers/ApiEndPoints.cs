using MassTransit;
using ProjetoRabbitMQ.Bus;
using ProjetoRabbitMQ.Relatorios;

namespace ProjetoRabbitMQ.Controllers
{
    public static class ApiEndPoints
    {
        //extension metodos rever 
        public static void AddApiEndpoints(this WebApplication app)
        {
            app.MapPost("solicitar-relatorio/{name}", async (string name, IPublishBus bus, CancellationToken ct = default) => 
            {
                var solicitacao = new SolicitacaoRelatorio()
                {
                    Id = Guid.NewGuid(),
                    Nome = name,
                    Status = "Pendente",
                    ProcessedTime = null
                };

                //Salando no banco
                Lista.Relatorios.Add(solicitacao);

                var eventRequest = new RelatorioSolicitadoEvent(solicitacao.Id, solicitacao.Nome);

                await bus.PublishAsync(eventRequest,ct);

                return Results.Ok(solicitacao);
            });

            app.MapGet("relatorios", () => Lista.Relatorios);
        }
    }
}
