using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player Controller�� �÷��̾� ĳ���ͷμ� Player ���ӿ�����Ʈ�� ������..
public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 700f;

    private int jumpCount = 0;//���� ���� Ƚ��
    private bool isGrounded = false; // �ٴڿ� ��Ҵ���...
    private bool isDead = false;

    private Rigidbody2D playerRigidbody; // ����� ������ٵ� ���۳�Ʈ
    private Animator animator; // ����� �ִϸ����� ���۳�Ʈ
    private AudioSource playerAudio; //����� ����� �ҽ� ���۳�Ʈ

   
    private void Start()
    {//���� ������Ʈ�κ��� ����� ���۳�Ʈ���� ������ ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        
    }

    
    private void Update()
    {if (isDead)
        {
            //����� ó���� ���̻� �������� �ʰ� ����
            return;
        }
    //���콺 ���� ��ư�� �������� && �ִ� ���� Ƚ��(2)�� �������� �ʾҴٸ�
    if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            //���� Ƚ�� ����
            jumpCount++;
            //���� ������ �ӵ��� ���������� ����(0.0)���� ����
            playerRigidbody.velocity = Vector2.zero;
            //������ٵ� �������� ���ֱ�
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            //����� �ҽ� ���
            playerAudio.Play();
        }
    else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            //���콺 ���� ��ư���� ���� ���� ���� && �ӵ��� y���� ������(���� ��� ��)
            //���� �ӵ��� �������� ����
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        //�ִϸ������� Grounded �Ķ���͸� isGrounded ������ ����
        animator.SetBool("Grounded", isGrounded);
        
    }

    private void Die()
    {
        //�ִϸ������� Die Ʈ���� �Ķ���͸� ��
        animator.SetTrigger("Die");
        //����� �ҽ��� �Ҵ�� ����� Ŭ���� deathClip���� ����
        playerAudio.clip = deathClip;
        //��� ȿ���� ���
        playerAudio.Play();

        //�ӵ��� ����(0,0)�� ����
        playerRigidbody.velocity = Vector2.zero;
        //��� ���¸� true�� ����
        isDead = true;c
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Dead" && !isDead)
        {
            //�浹�� ������ �±װ� Dead�̸� ���� ������� �ʾҴٸ� Die() ����
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ٴڿ� ��Ҵ��� �����ϴ� ó��
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //�ٴڿ��� ������� �����ϴ� ó��
    }
}
