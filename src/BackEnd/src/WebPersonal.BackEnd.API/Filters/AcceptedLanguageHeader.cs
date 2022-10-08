using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebPersonal.BackEnd.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AcceptedLanguageHeader : Attribute, ICustomAttribute, IOperationFilter
    {
        public static string HeaderName = "accept-language";
        
        public bool IsMandatory { get; }

        public AcceptedLanguageHeader(bool isMandatory = false)
        {
            IsMandatory = isMandatory;
        }
        
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            CustomAttribute acceptedLanguageHeader = context.RequireAttribute<AcceptedLanguageHeader>();

            if (!acceptedLanguageHeader.ContainsAttribute)
                return;
            
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = HeaderName,
                In = ParameterLocation.Header,
                Required = acceptedLanguageHeader.Mandatory,
                Schema = new OpenApiSchema() { Type = "string" }
            });
        }
    }
}