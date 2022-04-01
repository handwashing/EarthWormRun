using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 오브젝트를 계속 왼쪽으로 움직이는 스크립트
public class ScrollingObject : MonoBehaviour
{
    public float speed = 4f;

    private void Update()
    {
        //게임 오브젝트를 일정 속도로 왼쪽으로 평행이동하는 처리
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
