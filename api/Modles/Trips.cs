using System.ComponentModel.DataAnnotations;

namespace HITW.Function.Models;

public record Trip
{
    public string Label { get; set; }

    [StringLength(3)]
    public string Departure { get; set; }

    [StringLength(3)]
    public string Arrival { get; set; }

    public bool IsRoundTrip { get; set; }
}