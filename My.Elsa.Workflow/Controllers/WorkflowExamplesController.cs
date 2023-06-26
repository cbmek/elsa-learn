using Elsa.Models;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.AspNetCore.Mvc;
using My.Elsa.Workflow.Workflows;

namespace My.Elsa.Workflow.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class WorkflowExamplesController : ControllerBase
{
    private readonly IBuildsAndStartsWorkflow _buildsAndStartsWorkflow;
    private readonly IWorkflowLaunchpad _workflowLaunchpad;

    public WorkflowExamplesController(IBuildsAndStartsWorkflow buildsAndStartsWorkflow, IWorkflowLaunchpad workflowLaunchpad)
    {
        _buildsAndStartsWorkflow = buildsAndStartsWorkflow;
        _workflowLaunchpad = workflowLaunchpad;
    }

    [HttpGet(Name = "CreateMyEntity")]
    public async Task<IActionResult> CreateMyEntity()
    {
        var result = await _buildsAndStartsWorkflow.BuildAndStartWorkflowAsync<SimpleStateWorkflow>();

        return Ok(result);
    }

    [HttpGet(Name = "ContinueWorkflow")]
    public async Task<IActionResult> ContinueWorkflow()
    {
        var context = new WorkflowsQuery(nameof(SimpleBlockingActivity), new SimpleUnblockBookmark());
        var result = await _workflowLaunchpad.CollectAndDispatchWorkflowsAsync(context);

        return Ok(result);
    }
}