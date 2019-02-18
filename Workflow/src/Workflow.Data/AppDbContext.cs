using Microsoft.EntityFrameworkCore;
using Workflow.Core;
using Workflow.Data.Entities;
using Workflow.Data.Entities.Management;

namespace Workflow.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserWorkflow> UserWorkflows { get; set; }
        public DbSet<UserWorkflowStep> UserWorkflowSteps { get; set; }

        //ForManagement setup: TODO different domain?
        public DbSet<Entities.Management.Workflow> Workflows { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<WorkflowStep> WorkflowSteps { get; set; }




        public DbSet<Company> Companies { get; set; }
         public DbSet<State> States {get;set;}

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkflowStep>()
                .HasKey(sws => new { sws.WorkflowId, sws.StepId });

            modelBuilder.Entity<WorkflowStep>()
                .HasOne(sws => sws.Workflow)
                .WithMany(w => w.WorkflowSteps)
                .HasForeignKey(sws => sws.WorkflowId);

            modelBuilder.Entity<WorkflowStep>()
                .HasOne(sws => sws.Step)
                .WithMany(s => s.WorkflowSteps)
                .HasForeignKey(sws => sws.WorkflowId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
