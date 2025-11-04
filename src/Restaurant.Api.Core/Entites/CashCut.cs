namespace Restaurant.Api.Core.Entities;

public class CashCut {
    public required Guid Id { get; set; }
    public required double Total {get; set; }
    public required double Cash {get; set; }
    public required double Card {get; set; }
    public required double Change {get; set; }
    public required DateTime Date {get; set; }
}
