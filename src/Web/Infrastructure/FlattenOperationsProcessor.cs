using NSwag.Generation.Processors.Contexts;
using NSwag.Generation.Processors;

namespace SampleProject.Web.Infrastructure;

class FlattenOperationsProcessor : IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        context.OperationDescription.Operation.OperationId = $"{context.MethodInfo.Name}";
        return true;
    }
}
