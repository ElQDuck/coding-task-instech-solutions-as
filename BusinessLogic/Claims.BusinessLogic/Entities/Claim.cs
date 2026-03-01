using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Claims.BusinessLogic.Entities
{
    /// <summary>
    /// Represents a claim entity.
    /// </summary>
    public class Claim
    {
        /// <summary>
        /// Gets or sets the ID of the claim.
        /// </summary>
        /// TODO: Probably make this invisible to user.
        [BsonId]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the cover associated with this claim.
        /// </summary>
        /// TODO: Make this field required.
        [BsonElement("coverId")]
        public string CoverId { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the claim.
        /// </summary>
        [BsonElement("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the name of the claim.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the claim.
        /// </summary>
        [BsonElement("claimType")]
        public ClaimType Type { get; set; }

        /// <summary>
        /// Gets or sets the damage cost of the claim.
        /// </summary>
        [BsonElement("damageCost")]
        public decimal DamageCost { get; set; }
    }

    /// <summary>
    /// Represents the different types of claims.
    /// </summary>
    /// TODO: ClaimType nowhere used. Could be deleted but i decided to keep it to be able to ask why it exists.
    public enum ClaimType
    {
        /// <summary>Collision claim.</summary>
        Collision = 0,
        /// <summary>Grounding claim.</summary>
        Grounding = 1,
        /// <summary>Bad weather claim.</summary>
        BadWeather = 2,
        /// <summary>Fire claim.</summary>
        Fire = 3
    }
}
