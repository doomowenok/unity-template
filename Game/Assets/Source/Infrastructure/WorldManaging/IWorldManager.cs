namespace Infrastructure.WorldManaging
{
    public interface IWorldManager
    {
        IWorldManager CreateWorld(WorldType type, bool updateByUnity);
        IWorldManager AddSystemsGroup<TSystemsGroup>() where TSystemsGroup : class, ISystemsGroup;
        void Build();
    }
}