using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision1 : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer mesh;
    Material mat;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
        
    }

    private void OnCollisionEnter(Collision collision) { //물리적 충돌이 시작될 때 호출되는 함수

        if(collision.gameObject.name == "Sphere")
            mat.color = new Color(0, 0, 0);
    }

    private void OnCollisionExit(Collision collision) { //물리적 충돌이 끝났을때 호출되는 함수

        if(collision.gameObject.name == "Sphere") //어떤 물체랑 부딪혔을 때 색깔이 바뀌는지 지정(바닥에 닿았을 때 색깔 바뀌는 거 방지)
            mat.color = new Color(1, 1, 1);
    }

    /*
     private void OnCollisionStay(Collision other) { //물리적 충돌 중일때 호출되는 함수
        
    }

    private void OnCollisionExit(Collision other) { //물리적 충돌이 끝났을 때 호출
        
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
