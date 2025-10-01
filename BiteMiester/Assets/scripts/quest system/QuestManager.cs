using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();

        // check if we are actually loading the quest correctly!
        /*Quest quest = GetQuestById("CardDogQuest");
        Debug.Log(quest.info.displayName);
        Debug.Log(quest.info.levelRequirement);
        Debug.Log(quest.state);
        Debug.Log(quest.CurrentStepExists());*/
    }

    private void OnEnable()
    {
        GameManager._instance.questEvents.onStartQuest += StartQuest;
        GameManager._instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameManager._instance.questEvents.onFinishedQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameManager._instance.questEvents.onStartQuest -= StartQuest;
        GameManager._instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameManager._instance.questEvents.onFinishedQuest -= FinishQuest;
    }

    private void Start()
    {
        // broadcast the inital state of all quests on startup
        foreach (Quest quest in questMap.Values)
        {
            GameManager._instance.questEvents.QuestStateChange(quest);
        }
    }

    private void StartQuest(string id)
    {
        // TODO - start quest
        Debug.Log("Start Quest: " + id);
    }

    private void AdvanceQuest(string id)
    {
        // TODO - advance quest
        Debug.Log("Advance Quest: " + id);
    }

    private void FinishQuest(string id)
    {
        //TODO - finish quest
        Debug.Log("Finish Quest: " + id);
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
