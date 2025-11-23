using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Repository
{
    public interface ICredentialRepository
    {
        Task<Credential?> GetCredentialById(int id);
        Task<List<Credential>> GetCredentialsBy(int id);
        Task<int> InsertCredential(Credential credential);
        Task<bool> UpdateCredential(Credential updatedCredential);
        Task<bool> DeleteCredential(int id);
    }
}
