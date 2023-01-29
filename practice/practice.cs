using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 vec = new Vector3(0, 0, 1);
        transform.Translate(vec); //Translate: 벡터 값을 현재 위치에 더하는 함수
        //벡터의 크기만큼 이동
        //Debug.Log("Hello my name is yebbi yebbi yo");
    }

    void Update()
    {
         Vector3 vec = new Vector3(
            Input.GetAxis("Horizontal"), 
            Input.GetAxis("Vertical"),
            0);
        transform.Translate(vec); //Translate: 벡터 값을 현재 위치에 더하는 함수
        //벡터의 크기만큼 이동
        
        //1. MoveTowards

       /* transform.position =
            Vector3.MoveTowards(transform.position, target, 2f); //(현재 위치, 목표 위치, 속도) 

        //2. SmoothDamp: 부드러운 감속 이동, 마지막 메개변수에 반비례하여 속도 증가

        Vector3 velo = Vector3.zero;

        transform.position = 
            Vector3.SmoothDamp(transform.position, target, ref velo, 1.0f);

        //3. Lerp
        transform.position = 
            Vector3.Lerp(transform.position, target, 0.05f);

        //4. SLerp(구면 선형 보간)
        transform.position = 
        Vector3.Slerp(transform.position, target, 0.05f);
    }

    // Update is called once per frame

}
