using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Workflow.Data;
using Workflow.WebUi.Models;

namespace Workflow.WebUi.Controllers
{
    [Route("api/[controller]")]
    public class WorkflowDefinitionController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<WorkflowDefinitionController> _logger;

        public WorkflowDefinitionController(AppDbContext db, ILogger<WorkflowDefinitionController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var definitons = new List<WorkflowDefinition>();
            try
            {
               var worksflows = await _db.Workflows
                   .Include(w => w.WorkflowSteps)
                   .ThenInclude(ws=> ws.Step)
                   .ToListAsync();

                foreach (var workflow in worksflows)
                {
                    var workflowDefition = new WorkflowDefinition(workflow.Id, workflow.Name);
                    foreach (var workflowStep in workflow.WorkflowSteps)
                    {
                        var step = new Step(workflowStep.StepId, workflowStep.Step.Name, workflowStep.Step.Title);
                        workflowDefition.AddStep(step);
                    }
                    definitons.Add(workflowDefition);
                }
                return Ok(definitons);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}