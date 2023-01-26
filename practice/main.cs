using System;
using System.Collections.Generic;

public class NewBehaviorScript{

  void Start()
  {
    // 1. 변수
    int level = 100;
    float strength = 15.5f;
    string playerName = "이검사";
    bool isFullLevel = false;

    //2. 그룹형 변수
    string[] monsters = {"슬라임", "사막뱀", "악마"};
    int[] monsterLevel = new int[3];
    monsterLevel[0] = 1;

    List<string> items = new List<string>();
    items.Add("생명물약30");
    items.Add("마나물약 30");

    items.RemoveAt(0);

    //3. 연산자
    int exp = 1500;
    exp = 1500 + 320;
    exp = exp - 10;
    level = exp / 300;
    strength = level * 3.1f;
    int nextExp = 300 - (exp % 300);

    int fullLevel = 99;
    isFullLevel = level == fullLevel;
    bool isEndTutorial = level > 10;

    int health = 30;
    int mana = 25;
    //bool isBadCondition = health <= 50 && mana <= 20; // &&: 두 값이 모두 true면 true
    bool isBadCondition = health <= 50 || mana <= 20; // ||: 두 값 중 하나라도 true면 true

    string condition = isBadCondition ? "나쁨" : "좋음";

    //4.키워드
    //int float string bool new List

    //5.조건문
    if(condition == "나쁨"){
      
    }
    else{
      
    }
    switch (monsters[1]){
    case "슬라임":
    case "사막뱀":
      break;
    case "악마":
      break;
    case "골렘":
      break;
    default: //모든 case 통과한 후 실행
      break;
      
    }

    //6. 반복문
    while(health > 0){
      health--;
      Console.WriteLine("Hello World!");
      
    }
    for(int count = 0; count < 10; count++){
      
    }
    for(int index = 0; index < monsters.Length; index++){
      
    }
    foreach(string monster in monsters){ //monsters 안에 변수들을 직접 끄집어냄.
      
    }
    Heal(health);
  }
  //7.함수 (메소드)
  void Heal(int health){
    health += 10;
  }
  }
