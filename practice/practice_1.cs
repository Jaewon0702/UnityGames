using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practice_1 : MonoBehaviour
{
    void Update(){
        if (Input.anyKeyDown)
            Debug.Log("플레이어가 아무 키를 눌렀습니다.");

        
        if(Input.GetButton("Horizontal")){
            Debug.Log("횡 이동 중...." + Input.GetAxisRaw("Horizontal")); //수평, 수직 버튼 입력을 받으면 float
        }

        if(Input.GetButton("Vertical")){
            Debug.Log("종 이동 중...." + Input.GetAxisRaw("Vertical")); //수평, 수직 버튼 입력을 받으면 float
        }
    
    
        /*if (Input.anyKeyDown)
            Debug.Log("플레이어가 아무 키를 눌렀습니다.");
        if (Input.anyKey)
            Debug.Log("플레이어가 아무 키를 누르고 있습니다.");*/
        if(Input.GetKeyDown(KeyCode.Return)) //Return은 Enter 키를 의미함. GetKeyDown은 키를 눌렀을 때
            Debug.Log("아이템을 구입하였습니다.");

        if(Input.GetKey(KeyCode.LeftArrow)) //GetKey는 키를 누르고 있을 때
            Debug.Log("왼쪽으로 이동 중");

        if(Input.GetKeyUp(KeyCode.RightArrow))  //GetKeyUp은 키를 눌렀다가 땠을 때
            Debug.Log("오른쪽 이동을 멈추었습니다.");

        if(Input.GetMouseButtonDown(0))
            Debug.Log("미사일 발사!");
        
        if(Input.GetMouseButton(0))
            Debug.Log("미사일 모으는 중....");

        if(Input.GetMouseButtonUp(0))
            Debug.Log("슈퍼 미사일 발사!!");

        if(Input.GetButtonDown("Fire1")) //Button을 사용하면 여러 키를 Fire1 버튼에 넣고 사용할 수 있다.
            Debug.Log("점프!");

        if(Input.GetButton("Fire1"))     //Button은 새로 추가하거나 변경이 가능하다.
            Debug.Log("점프 모으는 중...");

         if(Input.GetButtonUp("Fire1"))
            Debug.Log("슈퍼 점프!!");

        
        //여기까지 복습함
    
    }
    
    
    
    
    
       /*
     //게임 오브젝트 생성할 때, 최초 실행
    void Awake() 
    { 
        Debug.Log("플레이어 데이터가 준비되었습니다.");
    }


    //게임 오브젝트가 활성화 되었을 때
    void OnEnable(){
        Debug.Log("플레이어가 로그인했습니니다.");
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("사냥 장비를 챙겼습니다.");
    }
    void FixedUpdate() { //물리 연산 업데이트, 고정된 실행 주기로 CPU 많이 사용
        Debug.Log("이동~");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("몬스터 사냥!!");
    }
    void LateUpdate() //모든 업데이트 끝난 후
    {
        Debug.Log("경험치 획득.");
    }
    // 게임 오브젝트가 비활성화 되었을 때
    void OnDisable() {
        Debug.Log("플레이어가 로그아웃했습니다.");
    }
    void OnDestroy()//게임 오브젝트가 삭제될 때
    { 
        Debug.Log("플레이어 데이터를 해제하였습니다.");

    }*/
}
