namespace Workflow.WebUi.Models
{
  public class UpdatePersonalStep
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
  }

  public class UpdateWorkStep
  {
    public string Work { get; set; }
  }

  public class UpdateAddressStep
  {
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
  }

  public class UpdateResultStep
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Work { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
  }
}
