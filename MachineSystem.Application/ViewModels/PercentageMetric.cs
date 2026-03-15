namespace MachineSystem.Application.ViewModels;

public class PercentageMetric(int part, int whole)
{
    public int Whole { get; init; } = whole;
    public int Part { get; init; } = part;
    public decimal Value { get => Whole == 0 ? 0 : decimal.Round((decimal)Part / Whole * 100 , 2); }
    public string FormattedValue { get => Value.ToString() + "%"; } 
}
