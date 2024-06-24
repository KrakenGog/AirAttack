using System.Collections.Generic;
using UnityEngine;

public class DestroyGroundVehicles : GameObserver
{
    [SerializeField] private List<GroundVehicle> _vehicles;

    public override string Description => "Destroy targets";

    public override void Initialize(GameSystemsRegistry gameSystemsRegistry)
    {
        base.Initialize(gameSystemsRegistry);

        _aim = _vehicles.Count;

        foreach (var vehicle in _vehicles)
        {
            var veh = vehicle;
            vehicle.Health.OnDeath += (_) =>
            {
                _vehicles.Remove(veh);
                _progress++;

                CallChangedEvent();

                if (_progress == _aim)
                    CallEvent();
            };
        }
    }
}
