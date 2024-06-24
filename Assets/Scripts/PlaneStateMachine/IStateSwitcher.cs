using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateSwitcher 
{
    T SwithState<T>() where T : State;

    T SwithState<T>(T state) where T : State;
}
