using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Keryhe.Telemetry.Core;

namespace Keryhe.Telemetry.Data.Access.Models;

public class InstrumentationScope
{
    public long Id { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = null!;
    
    [StringLength(255)]
    public string? Version { get; set; }
    
    [StringLength(2048)]
    public string? SchemaUrl { get; set; }
    
    [Required]
    [StringLength(64)]
    public string ScopeHash { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string? AttributesJson { get; set; }

    // Navigation properties
    public virtual ICollection<Span> Spans { get; set; } = new List<Span>();
    public virtual ICollection<LogRecord> LogRecords { get; set; } = new List<LogRecord>();
    public virtual ICollection<Metric> Metrics { get; set; } = new List<Metric>();
    
    // Helper properties for JSON deserialization
    [NotMapped]
    public Dictionary<string, object>? Attributes
    {
        get => string.IsNullOrEmpty(AttributesJson) ? null : JsonSerializer.Deserialize<Dictionary<string, object>>(AttributesJson);
        set => AttributesJson = value == null ? null : JsonSerializer.Serialize(value);
    }
}
