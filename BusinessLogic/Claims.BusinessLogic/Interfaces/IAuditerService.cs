namespace Claims.BusinessLogic.Interfaces;

public interface IAuditerService
{
    void AuditClaim(string id, string httpRequestType);
    void AuditCover(string id, string httpRequestType);
}
