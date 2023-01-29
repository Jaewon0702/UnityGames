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
    }

    // Update is called once per frame

}
