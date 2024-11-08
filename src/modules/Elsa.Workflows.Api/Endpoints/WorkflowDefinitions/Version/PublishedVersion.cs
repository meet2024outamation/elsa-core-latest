using Elsa.Abstractions;
using Elsa.Common.Entities;
using Elsa.Common.Models;
using Elsa.Workflows.Management;
using Elsa.Workflows.Management.Contracts;
using Elsa.Workflows.Management.Filters;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Elsa.Workflows.Api.Endpoints.WorkflowDefinitions.Version;
[PublicAPI]
internal class PublishedVersion(IWorkflowDefinitionStore store): ElsaEndpointWithoutRequest
{
    public override void Configure()
    {
        Get("workflow-definitions/versions");
        ConfigurePermissions("read:workflow-definitions");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
/*        var definitionId = Route<string>("definitionId")!;
*/
        var filter = new WorkflowDefinitionFilter
        {
/*            DefinitionId = definitionId,
*/            VersionOptions = VersionOptions.Published
        };

        var orderBy = new WorkflowDefinitionOrder<int>(x => x.Version, OrderDirection.Descending);
        var definitions = await store.FindManyAsync(filter, orderBy, cancellationToken);
        
        if (!definitions.Any())
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }
        var dict = new Dictionary<string, int> { };
        foreach (var item in definitions)
        {
            dict.Add(item.Name, item.Version);
        }
        await SendAsync(dict, StatusCodes.Status200OK, cancellationToken);
    }
}
