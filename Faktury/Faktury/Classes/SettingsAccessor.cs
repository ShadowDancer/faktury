namespace Faktury.Classes
{
    public class SettingsAccessor
    {
        private EditorSettings _settings;

        public void SetSettings(EditorSettings value) => _settings = value;
        public EditorSettings GetSettings() => _settings;
    }
}