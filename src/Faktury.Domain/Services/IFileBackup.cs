using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Faktury.Domain.Services
{
    public interface IFileBackup
    {
        Task SaveBackup();

        Task<bool> LoadBackupFromFile(string path);
    }
}
