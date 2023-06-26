using Elsa;
using Elsa.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Elsa.Workflow.Workflows;

internal class SimpleStateWorkflow : IWorkflow
{
    public void Build(IWorkflowBuilder builder)
    {
        builder
            .Add<SimpleWorkActivity>()
            .Then<SimpleBlockingActivity>()
            .Then<SimpleWorkActivity>();
    }
}