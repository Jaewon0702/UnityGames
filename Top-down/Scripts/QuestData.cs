using System.Collections;
using System.Collections.Generic;

public class QuestData
{
    public string questName;
    public int[] npcId;

    public QuestData(string name, int[] npc){ //이 스크립트 내 변수를 다른 스크립트에서 쓰기 위한 구조체
        questName = name;
        npcId = npc;
    }
}
