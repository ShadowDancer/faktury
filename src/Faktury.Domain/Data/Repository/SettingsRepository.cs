using Faktury.Domain.Domain;

namespace Faktury.Domain.Data.Repository
{
    public class SettingsRepository
    {
        private EditorSettings _settings;

        public void SetSettings(EditorSettings value) => _settings = value;
        public EditorSettings GetSettings() => _settings;
    }
}