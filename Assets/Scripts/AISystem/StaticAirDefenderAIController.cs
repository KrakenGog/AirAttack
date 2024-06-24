using UnityEngine;

public class StaticAirDefenderAIController : GroundAIController
{
    private EntitiesRegistry _entitiesRegistry;

    public override void Initialize(GameSystemsRegistry gameSystemRegistry)
    {
        base.Initialize(gameSystemRegistry);

        _entitiesRegistry = gameSystemRegistry.GetSystem<EntitiesRegistry>();
    }

    protected override bool ShouldMoveForward(Vector3 waypoint)
    {
        return false;
    }

    protected override void UpdateAI()
    {
        if (State == AIState.Dead) return;

        var antiAir = (StaticAntiAirDefender)Vehicle;


        antiAir.Attack(_entitiesRegistry.Player.Rigidbody);
    }
}
