using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;

namespace My.Elsa.Workflow.Workflows;

internal class SimpleBlockingActivity : Activity
{
    private readonly ILogger<SimpleBlockingActivity> _logger;

    public SimpleBlockingActivity(ILogger<SimpleBlockingActivity> logger)
    {
        _logger = logger;
    }

    protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
    {
        // code here will be run on entry of the activity

        _logger.LogInformation("!!!! [{workflowId}] SimpleBlockingActivity :: Suspended", context.WorkflowInstance.Id);

        return Suspend(); // this will suspend the workflow until resumed`
    }

    protected override async ValueTask<IActivityExecutionResult> OnResumeAsync(ActivityExecutionContext context)
    {
        // code here will be run on resume of the activity

        _logger.LogInformation("!!!! [{workflowId}] SimpleBlockingActivity :: Resumed Start", context.WorkflowInstance.Id);

        await Task.Delay(2500);

        _logger.LogInformation("!!!! [{workflowId}] SimpleBlockingActivity :: Resumed Done", context.WorkflowInstance.Id);

        return Done();
    }
}

internal class SimpleUnblockBookmark : IBookmark
{
}

internal class SimpleUnblockBookmarkProvider : BookmarkProvider<SimpleUnblockBookmark, SimpleBlockingActivity>
{
    public override IEnumerable<BookmarkResult> GetBookmarks(BookmarkProviderContext<SimpleBlockingActivity> context)
    {
        return new[] { Result(new SimpleUnblockBookmark()) };
    }
}