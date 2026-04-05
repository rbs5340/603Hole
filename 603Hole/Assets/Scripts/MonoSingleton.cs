using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public bool dontDestroyOnLoad = false;
    private static T _instance;

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Destroy(this);
            return;
        }
        if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (_instance != null && _instance == this) { _instance = null; }
    }
}
