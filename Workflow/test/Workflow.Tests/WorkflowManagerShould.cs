using Workflow.Api;
using Workflow.Core.Data;
using Workflow.Core.WorkflowSteps;
using Workflow.Tests.Mocks;
using Xunit;
using Xunit.Abstractions;

namespace Workflow.Tests
{
    public class WorkflowManagerShould
    {
        private readonly WorkflowManager _systemUnderTest;
        private readonly ITestOutputHelper output;

        public WorkflowManagerShould(ITestOutputHelper output)
        {
            _systemUnderTest = new WorkflowManager(new MockWorkflowDataService());
            this.output = output;
        }        

        #region SetData
        [Fact]
        public void SetPersonalData()
        {
            _systemUnderTest.LoadUserWorkflowByRequestId("jawad");

            _systemUnderTest.SetStepData(Steps.Personal, new Personal("jawad", "hasan", "jawadhasan80@gmail.com"));

            Assert.Equal("jawad", _systemUnderTest.BaseWorkflow.WorkflowData.FirstName);
            Assert.Equal("hasan", _systemUnderTest.BaseWorkflow.WorkflowData.LastName);
            Assert.Equal("jawadhasan80@gmail.com", _systemUnderTest.BaseWorkflow.WorkflowData.Email);
        }

        [Fact]
        public void SetWorkData()
        {
            _systemUnderTest.LoadUserWorkflowByRequestId("jawad");
            _systemUnderTest.SetStepData(Steps.Work, new Work("code"));

            Assert.Equal("code", _systemUnderTest.BaseWorkflow.WorkflowData.Work);
        }

        [Fact]
        public void SetAddressData()
        {
            _systemUnderTest.LoadUserWorkflowByRequestId("jawad");

            _systemUnderTest.SetStepData(Steps.Address, new Address("123. street", "Braunschweig", "ABC", "38114"));

            Assert.Equal("123. street", _systemUnderTest.BaseWorkflow.WorkflowData.Street);
        }

        [Fact]
        public void SetResultData()
        {
            _systemUnderTest.LoadUserWorkflowByRequestId("jawad");

            _systemUnderTest.SetStepData(Steps.Result, true);

            Assert.False(_systemUnderTest.BaseWorkflow.IsValid());
        }
        #endregion


        [Fact]
        public void OnBoardingWorkflow_Valid_If_AllStepsAreValid()
        {
            _systemUnderTest.LoadUserWorkflowByRequestId("jawad");
            _systemUnderTest.SetStepData(Steps.Personal, new Personal("jawad", "hasan", "jawadhasan80@gmail.com"));
            _systemUnderTest.SetStepData(Steps.Work, new Work("code"));
            _systemUnderTest.SetStepData(Steps.Address, new Address("123. street", "Braunschweig", "ABC", "38114"));
            _systemUnderTest.SetStepData(Steps.Result, true);

            Assert.True(_systemUnderTest.BaseWorkflow.IsValid());
            output.WriteLine(_systemUnderTest.BaseWorkflow.ToString());
        }


        [Fact]
        public void OnBoardingWorkflow_InValid_If_AnyStepIsInValid()
        {
            _systemUnderTest.LoadUserWorkflowByRequestId("jawad");
            _systemUnderTest.SetStepData(Steps.Personal, new Personal("jawad", "hasan", "jawadhasan80@gmail.com"));
            _systemUnderTest.SetStepData(Steps.Work, new Work("code"));
             //missing Address
            _systemUnderTest.SetStepData(Steps.Result, true);

            Assert.False(_systemUnderTest.BaseWorkflow.IsValid());
            output.WriteLine(_systemUnderTest.BaseWorkflow.ToString());
        }


        [Fact]
        public void Return_First_Invalid_STEP_Name()
        {
            _systemUnderTest.LoadUserWorkflowByRequestId("jawad");
            _systemUnderTest.SetStepData(Steps.Personal, new Personal("jawad", "hasan", "jawadhasan80@gmail.com"));
            _systemUnderTest.SetStepData(Steps.Work, new Work("code"));
            //missing Address
            _systemUnderTest.SetStepData(Steps.Result, true);

            Assert.False(_systemUnderTest.BaseWorkflow.IsValid());
            Assert.Equal("Address", _systemUnderTest.GetFirstInvalidStepName);
        }
    }
}
