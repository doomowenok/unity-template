namespace Gameplay.Services.SaveLoad
{
    public interface ISavingService
    {
        bool TryLoad<TData>(string key, out TData save);
        void Save<TData>( string key, TData data);
    }
}