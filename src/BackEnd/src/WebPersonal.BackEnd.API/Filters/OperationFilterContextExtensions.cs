using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebPersonal.BackEnd.API.Filters
{
    public static class OperationFilterContextExtensions
    {
        public static CustomAttribute RequireAttribute<T>(this OperationFilterContext context)
        where T : ICustomAttribute
        {
            IEnumerable<IFilterMetadata> globalAttributes = context
                .ApiDescription
                .ActionDescriptor
                .FilterDescriptors
                .Select(p => p.Filter);

            object[] controllerAttributes = context
                .MethodInfo?
                .DeclaringType?
                .GetCustomAttributes(true) ?? Array.Empty<object>();

            object[] methodAttributes = context
                .MethodInfo?
                .GetCustomAttributes(true)?? Array.Empty<object>();

            List<T> containsHeaderAttributes = globalAttributes
                .Union(controllerAttributes)
                .Union(methodAttributes)
                .OfType<T>()
                .ToList();
            
            return containsHeaderAttributes.Count == 0 
                ? new CustomAttribute(false, false) 
                : new CustomAttribute(true, containsHeaderAttributes.First().IsMandatory);
        }
    }
}