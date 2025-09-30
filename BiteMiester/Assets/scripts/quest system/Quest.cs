using Unity.Android.Types;
using UnityEngine;

public class Quest
{
    // static info
    public QuestInfo info;

    // state info
    public QuestState state;
    private int currQuestStepIndex;

    public Quest(QuestInfo info)
    {
        this.info = info;
        this.state = QuestState.REQUIREMENTS_NOT_MET; // init
        this.currQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currQuestStepIndex < info.questStepPrefabs.Length); // to avoid going over numba of steps
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;

        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab but stepIndex was out of range indicating that " +
                "there's no current step: QuestId=" + info.id + ", stepIndex=" + currQuestStepIndex);
        }

        return questStepPrefab;
    }
}
