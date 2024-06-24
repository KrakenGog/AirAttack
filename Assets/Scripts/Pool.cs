using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T>
{
    private Func<T> _getInstance;
    private Action<T> _activate;
    private Action<T> _disactivate;

    private Queue<T> _pool;

    public Pool(Func<T> getInstance, Action<T> activate, Action<T> deactivate, int startCount)
    {
        _getInstance = getInstance;
        _activate = activate;
        _disactivate = deactivate;

        _pool = new Queue<T>();

        for (int i = 0; i < startCount; i++)
        {
            T instance = _getInstance();
            _disactivate(instance);

            _pool.Enqueue(instance);
        }
    }


    public T Get()
    {
        T item = _pool.Count > 0 ? _pool.Dequeue() : _getInstance();

        _activate(item);

        return item;
    }

    public void Push(T item)
    {
        _disactivate(item);

        _pool.Enqueue(item);
    }

}
