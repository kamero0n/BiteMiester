using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance()
    {
        if (_instance == null)
        {
            Debug.LogError("game manager is null! what the!");
        }

        return _instance;
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        // for existance of game manager through all levels if i add more
        DontDestroyOnLoad(this);
    }
}
