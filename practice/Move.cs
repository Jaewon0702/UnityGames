using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        
        
    }

    // Update is called once per frame
    void FixedUpdate() //Rigid 관련 코드는 FixedUpdate안에 작성!
    {   //#1. 속력 바꾸기
        //rigid.velocity = new Vector3(2, 4, 3); 

        //#2. 힘을 가하기
        
        if(Input.GetButtonDown("Jump")){
        rigid.AddForce(Vector3.up * 100, ForceMode.Impulse); //주로 캐릭터가 점프할 때 사용!!
        Debug.Log(rigid.velocity);
        }
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); //키보드로 공을 왔다 갔다

        rigid.AddForce(vec, ForceMode.Impulse);
        

        //#3 회전력
        //rigid.AddTorque(Vector3.up); //AddTorgque(Vec) Vec 방향을 축으로 회전력이 생김.
        
    }

    private void OnTriggerStay(Collider other) { //OnTrigger는 물리적인 충돌이 아닌 둘이 겹쳐있는 가를 보기 때문에 Collider 이용!
        if(other.name == "Cube")
            rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
    }

    
}
