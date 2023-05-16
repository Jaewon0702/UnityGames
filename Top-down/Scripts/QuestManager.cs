using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();    
        GenerateData();
    }


    void GenerateData()
    {
        questList.Add(10, new QuestData("Talking to Villagers", new int[]{1000, 2000}));
    }

    public int GetQuestTalkIndex(int id){
        return questId + questActionIndex;
    }

    public void CheckQuest(int id){

        if(id == questList[questId].npcId[questActionIndex]){
            questActionIndex++;
        }
    }


}
