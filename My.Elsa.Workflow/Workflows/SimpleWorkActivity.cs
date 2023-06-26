using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Services;
using Elsa.Services.Models;

namespace My.Elsa.Workflow.Workflows;

internal class SimpleWorkActivity : Activity
{
    private readonly ILogger<SimpleWorkActivity> _logger;

    [ActivityInput(Hint = "Message to be logged by logger")]
    public string LogMessage { get; set; } = ""; // don't know how to use it outside of the designer yet

    public SimpleWorkActivity(ILogger<SimpleWorkActivity> logger)
    {
        _logger = logger;
    }

    protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context) // OnExecute() or OnExecute(ActivityExecutionContext context) or OnExecuteAsync(ActivityExecutionContext context)
    {
        _logger.LogInformation("!!!! SimpleWorkActivity :: Start :: {logMessage}", LogMessage);

        await Task.Delay(1000);

        _logger.LogInformation("!!!! SimpleWorkActivity :: Done :: {logMessage}", LogMessage);

        return Done(); // return Done(); == return Outcome("Done"); == Outcome(OutcomeNames.Done)
    }
}