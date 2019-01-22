using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workflow.Core;
using Workflow.Core.Data;
using Workflow.Core.Workflows;
using BaseWorkflow = Workflow.Core.Workflows.BaseWorkflow;

namespace Workflow.Tests.Mocks
{
    public class MockWorkflowDataService : IWorkflowDataService
    {
        public Dictionary<string, BaseWorkflow> Data { get; private set; }

        public MockWorkflowDataService()
        {
            var requestId_1 = Guid.NewGuid().ToString("N");
            var requestId_2 = Guid.NewGuid().ToString("N");
            var requestId_3 = Guid.NewGuid().ToString("N");

            Data = new Dictionary<string, BaseWorkflow>
            {
                {"jon", BasicWorkflow.Create("jawadhasan80@gmail.com", requestId_1, 10)},
                {"jane", SimpleWorkflow.Create("jawadhasan80@gmail.com", requestId_2, 10)},
                {"jawad", OnboardingWorkflow.Create("jawadhasan80@gmail.com", requestId_3, 10)}
            };
        }

        public Task<BaseWorkflow> GetWorkflowByRequestId(string requestId)
        {
            var data = Data.FirstOrDefault(w => w.Key == requestId).Value;
            return Task.FromResult(data);
        }

        public Task SaveWorkflowAsync(BaseWorkflow workflow)
        {
            return Task.FromResult(0);
        }

        public Task UpdatePersonalInfo(string id, Personal personalInfo)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWorkInfo(string id, Work workInfo)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAddressInfo(string id, Address addressInfo)
        {
            throw new NotImplementedException();
        }

        public Task UpdateResult(string id, FormData result)
        {
            throw new NotImplementedException();
        }
    }
}
