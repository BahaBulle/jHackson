namespace jHackson.Core.Localization
{
    public interface ILocalizationResourceProvider
    {
        object GetValue(string key);

        void Load();

        void Unload();
    }
}