using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Workflow.Core;
using Workflow.Core.Workflows;
using Workflow.Core.WorkflowSteps;
using Workflow.Data;
using Workflow.Data.Entities;
using Workflow.Data.Entities.Management;

namespace Workflow.WebUi.Helpers
{
    public class DbSeeder
    {
        readonly ILogger _Logger;

        public DbSeeder(ILoggerFactory loggerFactory)
        {
            _Logger = loggerFactory.CreateLogger("DbSeederLogger");
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            //Based on EF team's example at https://github.com/aspnet/MusicStore/blob/dev/samples/MusicStore/Models/SampleData.cs
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (await db.Database.EnsureCreatedAsync())
                {
                    if (!await db.Workflows.AnyAsync())
                    {
                        await InsertApplicationData(db);
                        //await InsertTestData(db);
                    }
                }
            }
        }


        public async Task InsertTestData(AppDbContext db)
        {
            var userWorkflow = new UserWorkflow(
                SupportedWorkflows.OnBoarding.ToString(),
                "jawadhasan80@gmail.com",
                "test1",
                10,
                new DateTime(2018, 12, 20)
            );

            var personalStep = new UserWorkflowStep(Steps.Personal.ToString());
            personalStep.MarkIsStepComplete(true);
            userWorkflow.AddWorkflowStep(personalStep);

            var workStep = new UserWorkflowStep(Steps.Work.ToString());
            workStep.MarkIsStepComplete(true);
            userWorkflow.AddWorkflowStep(workStep);

            var addressStep = new UserWorkflowStep(Steps.Address.ToString());
            addressStep.MarkIsStepComplete(true);
            userWorkflow.AddWorkflowStep(addressStep);

            var resultStep = new UserWorkflowStep(Steps.Result.ToString());
            resultStep.MarkIsStepComplete(true);
            userWorkflow.AddWorkflowStep(resultStep);


            userWorkflow.SetPersonal("Jawad", "Hasan", "j.shani@hds-systems.com");
            userWorkflow.SetWork("Code");
            userWorkflow.SetAddress("Willy-Brandt-Plataz", "Braunschweig", "38114", "SomeState");
            userWorkflow.UpdateStatus(WorkflowStatus.Completed);

            db.UserWorkflows.Add(userWorkflow);

            foreach (var step in userWorkflow.Steps)
            {
                if (step.TrackingState == TrackingState.Created)
                {
                    db.Entry(step).State = EntityState.Added;
                }
            }
            await db.SaveChangesAsync();

            //_workflowDataService.SaveWorkflowAsync(dbWorkflow);
        }

        public async Task InsertApplicationData(AppDbContext db)
        {
            var supportedWorkflows = ApplicationData.GetSupportedWorkflows();
            db.Workflows.AddRange(supportedWorkflows);

            var supportedSteps = ApplicationData.GetSupportedSteps();
            db.Steps.AddRange(supportedSteps);


            await db.SaveChangesAsync();
            
            var personalStep = db.Steps.FirstOrDefault(s => s.Name == "Personal");
            var workStep = db.Steps.FirstOrDefault(s => s.Name == "Work");
            var addressStep = db.Steps.FirstOrDefault(s => s.Name == "Address");
            var resultStep = db.Steps.FirstOrDefault(s => s.Name == "Result");

            //create mapping for Basic
            var basicWorkflow = db.Workflows.FirstOrDefault(w => w.Name == "Basic");
            var basicPersonal = new WorkflowStep { WorkflowId = basicWorkflow.Id, StepId = personalStep.Id };
            var basicResult = new WorkflowStep { WorkflowId = basicWorkflow.Id, StepId = resultStep.Id };


            //create mapping for Simple
            var simpleWorkflow = db.Workflows.FirstOrDefault(w => w.Name == "Simple");
            var simplePersonal = new WorkflowStep { WorkflowId = simpleWorkflow.Id, StepId = personalStep.Id };
            var simpleWork = new WorkflowStep { WorkflowId = simpleWorkflow.Id, StepId = workStep.Id };
            var simpleResult = new WorkflowStep { WorkflowId = simpleWorkflow.Id, StepId = resultStep.Id };


            //create mapping for onboarding
            var onboardingWorkflow = db.Workflows.FirstOrDefault(w => w.Name == "OnBoarding");
            var onboardingPersonal = new WorkflowStep {WorkflowId = onboardingWorkflow.Id, StepId = personalStep.Id};
            var onboardingWork = new WorkflowStep { WorkflowId = onboardingWorkflow.Id, StepId = workStep.Id };
            var onboardingAddress = new WorkflowStep { WorkflowId = onboardingWorkflow.Id, StepId = addressStep.Id };
            var onboardingResult = new WorkflowStep { WorkflowId = onboardingWorkflow.Id, StepId = resultStep.Id };
            

            
            db.WorkflowSteps.AddRange(
                basicPersonal, basicResult,
                simplePersonal, simpleWork, simpleResult,
                onboardingPersonal, onboardingWork, onboardingAddress, onboardingResult
                );

            try
            {
                int numAffected = await db.SaveChangesAsync();
                _Logger.LogInformation($"Saved {numAffected} Rows");
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DbSeeder)}: " + exp.Message);
                throw;
            }
        }
    }
}
