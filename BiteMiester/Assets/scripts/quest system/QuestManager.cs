using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();

        Quest quest = GetQuestById("CardDogQuest");
        Debug.Log(quest.info.displayName);
        Debug.Log(quest.info.levelRequirement);
        Debug.Log(quest.state);
        Debug.Log(quest.CurrentStepExists());
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        // loads all quest info scriptables under the Assets/Resources/Quests folder
        QuestInfo[] allQuests = Resources.LoadAll<QuestInfo>("Quests");

        // create quest map
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfo questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
            }

            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }

        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("ID not found in the quest map: " + id);
        }

        return quest;
    }
}
