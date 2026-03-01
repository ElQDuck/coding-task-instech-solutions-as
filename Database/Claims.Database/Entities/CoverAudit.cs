namespace Claims.Database.Entities
{
    /// <summary>
    /// Represents a cover audit record.
    /// </summary>
    public class CoverAudit
    {
        /// <summary>Gets or sets the ID of the audit record.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the ID of the audited cover.</summary>
        public string? CoverId { get; set; }

        /// <summary>Gets or sets the creation date of the audit record.</summary>
        public DateTime Created { get; set; }

        /// <summary>Gets or sets the type of the HTTP request that triggered the audit.</summary>
        public string? HttpRequestType { get; set; }
    }
}
