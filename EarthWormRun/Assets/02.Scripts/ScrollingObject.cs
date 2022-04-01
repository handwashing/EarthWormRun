using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ������Ʈ�� ��� �������� �����̴� ��ũ��Ʈ
public class ScrollingObject : MonoBehaviour
{
    public float speed = 4f;

    private void Update()
    {
        //���� ������Ʈ�� ���� �ӵ��� �������� �����̵��ϴ� ó��
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
