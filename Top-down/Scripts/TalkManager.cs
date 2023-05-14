using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
   Dictionary<int, string[]> talkData;

   void Awake(){

    talkData = new Dictionary<int, string[]>(); 
    GenerateData();
   }

   void GenerateData(){
    talkData.Add(1000, new string[]{"Hi.", "It's your first time here!" }); //Npc Luna
    talkData.Add(2000, new string[]{"Hey!", "What a beautiful lake.", "There is something secret hidden in this lake."}); //Npc Ludo
    talkData.Add(100, new string[]{"It's an ordinary wooden box."}); //Box
    talkData.Add(200, new string[]{"It's a desk with traces of someone's use."}); //Desk

   }

   public string GetTalk(int id, int talkIndex){
      
      if(talkIndex == talkData[id].Length){
         return null;
      }
      else{
         return talkData[id][talkIndex];
      }
   }

}
