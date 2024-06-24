public class PlayerDied : GameObserver
{
    public override void Initialize(GameSystemsRegistry gameSystemsRegistry)
    {
        base.Initialize(gameSystemsRegistry);

        EntitiesRegistry entitiesRegistry = gameSystemsRegistry.GetSystem<EntitiesRegistry>();

        entitiesRegistry.Player.PlayerDied += CallEvent;
    }
}

