using EventsBookingBackend.Application.Models.Common;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EventsBookingBackend.Api.ControllerOptions.Filters;

public class SwaggerProducesResponseTypesFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var errorResponseType = new OpenApiResponse
        {
            Description = "Internal Server Exceptions",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(ErrorModel), context.SchemaRepository)
                }
            }
        };

        var validationErrorResponseType = new OpenApiResponse
        {
            Description = "Bad Request",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(ValidationErrorModel),
                        context.SchemaRepository)
                }
            }
        };

        // Add 500 status code response
        operation.Responses.TryAdd(StatusCodes.Status500InternalServerError.ToString(), errorResponseType);

        // Add 400 status code response
        operation.Responses.TryAdd(StatusCodes.Status400BadRequest.ToString(), validationErrorResponseType);
    }
}