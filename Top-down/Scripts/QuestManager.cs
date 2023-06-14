using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; //Order of Quest Talk
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();    
        GenerateData();
    }


    void GenerateData()
    {
        questList.Add(10, new QuestData("Talking to Villagers", new int[]{1000, 2000})); //new int[]안에는 quest와 연관된 Npc의 Id가 입력되어 있음.
        questList.Add(20, new QuestData("Finding Ludo's Coin", new int[]{5000, 2000}));
        questList.Add(30, new QuestData("Quest All Clear!", new int[]{0}));

    }

    public int GetQuestTalkIndex(int id){
        return questId + questActionIndex;
    }

    public string CheckQuest(int id){
    
        //Next Quest Target
        if(id == questList[questId].npcId[questActionIndex]){ //QuestData 함수의 npcId 배열(new int[]{1000, 2000}))의 Index 
        //Ex) questActionIndex == 0이면 0번째인 1000반환
            //순서에 맞게 대화했을 때만 QuestActionIndex++;
            questActionIndex++;
        }

        //Control Quest Object
         ControlObject();

        if(questActionIndex == questList[questId].npcId.Length){
            NextQuest();
        }
        //Quest Name
        return questList[questId].questName;
    }
public string CheckQuest(){
    //Quest Name
    return questList[questId].questName;
}

    void NextQuest(){
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject(){
        switch(questId){
            case 10:
                if(questActionIndex == 2){
                    questObject[0].SetActive(true);
                }
            break;
            case 20:
                if(questActionIndex == 1){
                    questObject[0].SetActive(false);
                }
            break;
        }
    }


}


