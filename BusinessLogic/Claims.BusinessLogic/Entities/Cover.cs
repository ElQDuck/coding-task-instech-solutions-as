using MongoDB.Bson.Serialization.Attributes;

namespace Claims.BusinessLogic.Entities;

/// <summary>
/// Represents a cover entity.
/// </summary>
public class Cover
{
    /// <summary>
    /// Gets or sets the ID of the cover.
    /// </summary>
    [BsonId]
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the start date of the cover.
    /// </summary>
    [BsonElement("startDate")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the cover.
    /// </summary>
    [BsonElement("endDate")]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the type of the cover.
    /// </summary>
    /// TODO: Why is this a claimType in the DB entry?
    [BsonElement("claimType")]
    public CoverType Type { get; set; }

    /// <summary>
    /// Gets or sets the premium for the cover.
    /// </summary>
    [BsonElement("premium")]
    public decimal Premium { get; set; }
}

/// <summary>
/// Represents the different types of covers.
/// </summary>
public enum CoverType
{
    /// <summary>Yacht cover.</summary>
    Yacht = 0,
    /// <summary>Passenger ship cover.</summary>
    PassengerShip = 1,
    /// <summary>Container ship cover.</summary>
    ContainerShip = 2,
    /// <summary>Bulk carrier cover.</summary>
    BulkCarrier = 3,
    /// <summary>Tanker cover.</summary>
    Tanker = 4
}
