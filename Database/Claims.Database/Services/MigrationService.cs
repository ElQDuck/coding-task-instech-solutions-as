using Claims.BusinessLogic.Interfaces;
using Claims.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Claims.Database.Repositories
{
    public class MigrationService : IMigrationService
    {
        private readonly AuditContext _auditContext;

        public MigrationService(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        public void ApplyMigrations()
        {
            _auditContext.Database.Migrate();
        }
    }
}
