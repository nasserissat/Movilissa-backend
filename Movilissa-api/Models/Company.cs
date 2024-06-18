namespace Movilissa_api.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public string Instagram { get; set; }
    public string Facebook { get; set; }
    public string Website { get; set; }
    public double? Score { get; set; }
    public ICollection<Branch> Branches { get; set; }
    public ICollection<Bus> Buses { get; set; }
    public ICollection<Schedule> Schedules { get; set; }
}