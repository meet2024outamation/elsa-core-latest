using Elsa.Common.Models;
using Elsa.Mediator.Contracts;
using Elsa.Mediator.Models;

namespace Elsa.Workflows.Runtime.Commands;

/// <summary>
/// Dispatches a workflow definition.
/// </summary>
public class DispatchWorkflowDefinitionCommand(string definitionId, VersionOptions versionOptions) : ICommand<Unit>
{
    public string DefinitionId { get; init; } = definitionId;
    public VersionOptions VersionOptions { get; init; } = versionOptions;
    public string? ParentWorkflowInstanceId { get; init; }
    public IDictionary<string, object>? Input { get; set; }
    public IDictionary<string, object>? Properties { get; set; }
    public string? CorrelationId { get; set; }
    public string? InstanceId { get; set; }
    public string? TriggerActivityId { get; set; }
}