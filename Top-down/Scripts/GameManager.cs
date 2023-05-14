using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public TalkManager talkMamager;
    public int talkIndex;
    

    public void Action(GameObject scanObj)
    { 
        scanObject = scanObj;
        ObjData ObjData = scanObject.GetComponent<ObjData>();
        Talk(ObjData.id, ObjData.isNpc);       
        
        
       talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc){
        string talkData = talkMamager.GetTalk(id, talkIndex);
        if(talkData == null){ // When talk is done
            isAction = false;
            talkIndex = 0;
            return; // void 함수에서 강제 종료 역할.

    }

        if(isNpc){
            talkText.text = talkData;
        }
        else{
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }   
}

