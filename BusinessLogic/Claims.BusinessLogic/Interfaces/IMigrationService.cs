namespace Claims.BusinessLogic.Interfaces
{
    /// <summary>
    /// Defines the service for applying database migrations.
    /// </summary>
    public interface IMigrationService
    {
        /// <summary>
        /// Applies any pending database migrations.
        /// </summary>
        void ApplyMigrations();
    }
}
