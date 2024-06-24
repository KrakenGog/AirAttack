using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance
    {
        get => _instance;

        set
        {
            if (!_instance)
            {
                _instance = value;
            }
            else
            {
                Destroy(value);
            }
        }

    }

    private static T _instance;

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        Instance = (T)this;
    }
}

