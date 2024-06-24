using System.Collections.Generic;
using UnityEngine;

public class GameSystemsRegistry : MonoBehaviour
{
    private List<GameSystem> _gameSystems;

    public void Initialize()
    {
        _gameSystems = new List<GameSystem>();
    }

    public void AddSystem(GameSystem gameSystem)
    {
        foreach (GameSystem system in _gameSystems)
        {
            if (system.GetType() == gameSystem.GetType())
            {
                throw new System.Exception($"System with type {gameSystem.GetType()} already exist");
            }
        }

        _gameSystems.Add(gameSystem);
    }

    public T GetSystem<T>() where T : GameSystem
    {
        foreach (GameSystem system in _gameSystems)
        {
            if (system is T)
            {
                return (T)system;
            }
        }

        throw new System.Exception($"System with type {typeof(T)} not found");
    }

    public void UpdateAllSystems(float delta)
    {
        foreach (var system in _gameSystems)
        {
            system.GameUpdate(delta);
        }
    }

    public void StopAllSystems()
    {
        foreach (var system in _gameSystems)
        {
            system.StopSystem();
        }
    }
}
