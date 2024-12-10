using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform playerTransform;
    Animator playerAnim;
    SpriteRenderer playerSprite;
    public float moveSpeed = 3f;
    private bool facingRight = true;  // �ʱ� ���¸� ���������� ����

    // �÷��̾� �̵� ���� ����
    public float minX = -10f;
    public float maxX = 10f;

    // ������ ���� Ȯ���� ���� Ÿ�̸�
    private float idleTimer = 0.05f;
    private float idleTimerCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = this.GetComponent<Transform>();
        playerAnim = this.GetComponent<Animator>();
        playerSprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, 0);
        playerTransform.position += moveDirection * moveSpeed * Time.deltaTime;

        // �÷��̾��� X ��ǥ�� ����
        float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
        playerTransform.position = new Vector3(clampedX, playerTransform.position.y, playerTransform.position.z);

        // ���� ��ȯ�� �ִϸ��̼� ���¸� ���� ����
        if (horizontalInput != 0)
        {
            // Walk �ִϸ��̼� ���¸� ���� Ȱ��ȭ
            playerAnim.SetBool("Walk", true);

            // ���� ��ȯ üũ
            if (horizontalInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (horizontalInput < 0 && facingRight)
            {
                Flip();
            }

            // �Է��� ���� �� Ÿ�̸� �ʱ�ȭ
            idleTimerCounter = 0f;
        }
        else
        {
            // �Է��� ���� �� Ÿ�̸Ӹ� ������Ŵ
            idleTimerCounter += Time.deltaTime;

            // Ÿ�̸Ӱ� ������ �ð��� �ʰ��ϸ� Walk �ִϸ��̼� ��Ȱ��ȭ
            if (idleTimerCounter >= idleTimer)
            {
                playerAnim.SetBool("Walk", false);
            }
        }
    }

    // ĳ���� ������ ��ȯ�ϴ� �޼���
    void Flip()
    {
        facingRight = !facingRight;
        playerSprite.flipX = !playerSprite.flipX;
    }
}
