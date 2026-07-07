using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    // Search for existing instance in the scene
                    _instance = (T)FindAnyObjectByType(typeof(T));

                    if (_instance == null)
                    {
                        // Create new GameObject if none exists
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        // Keep it alive across scenes
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }
    }
}