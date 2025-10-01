using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance { get; private set; }

    public QuestEvents questEvents;

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

        // init all things!
        questEvents = new QuestEvents();
    }
}
