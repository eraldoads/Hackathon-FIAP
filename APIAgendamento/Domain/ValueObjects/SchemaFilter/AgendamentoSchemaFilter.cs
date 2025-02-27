﻿using Domain.Entities;
using Domain.EntitiesDTO;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domain.ValueObjects
{
    public class AgendamentoSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // verifica se o contexto é da classe Pedido
            if (context.Type == typeof(Agendamento))
            {
                // cria um objeto OpenApiObject com os valores desejados
                var modeloPedido = new OpenApiObject
                {
                    ["Id"] = new OpenApiInteger(0),
                    ["IdCliente"] = new OpenApiInteger(0),
                    ["DataPedido"] = new OpenApiString(DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                    ["ValorTotal"] = new OpenApiInteger(0)
                };
                // atribui o exemplo ao esquema
                schema.Example = modeloPedido;
            }

            // verifica se o contexto é da classe PedidoDTO
            if (context.Type == typeof(AgendamentoDTO))
            {
                // cria um objeto OpenApiObject com os valores desejados
                var modeloPedidoDTO = new OpenApiObject
                {
                    ["idPedido"] = new OpenApiString("string"),
                    ["dataPedido"] = new OpenApiString(DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                    ["valorTotal"] = new OpenApiDouble(0.00)
                };
                // atribui o exemplo ao esquema
                schema.Example = modeloPedidoDTO;
            }

        }
    }
}
