using LegalTrace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.DAL.Controllers.CredentialControllers
{
    public interface ICredentialController
    {
        Task<Credential?> GetCredentialById(int id);
        Task<List<Credential>> GetCredentialsBy(int id);
        Task<int> InsertCredential(Credential credential);
        Task<bool> UpdateCredential(Credential updatedCredential);
        Task<bool> DeleteCredential(int id);
    }
}
