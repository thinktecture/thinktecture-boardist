using System;

namespace Thinktecture.Boardist.WebApi.Database.Models
{
  public class Person
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}
