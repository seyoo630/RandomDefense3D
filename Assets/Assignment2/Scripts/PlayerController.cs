using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform playerTransform;
    Animator playerAnim;
    SpriteRenderer playerSprite;
    public float moveSpeed = 3f;
    private bool facingRight = true;  // 초기 상태를 오른쪽으로 설정

    // 플레이어 이동 범위 설정
    public float minX = -10f;
    public float maxX = 10f;

    // 움직임 멈춤 확인을 위한 타이머
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

        // 플레이어의 X 좌표를 제한
        float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
        playerTransform.position = new Vector3(clampedX, playerTransform.position.y, playerTransform.position.z);

        // 방향 전환과 애니메이션 상태를 먼저 설정
        if (horizontalInput != 0)
        {
            // Walk 애니메이션 상태를 먼저 활성화
            playerAnim.SetBool("Walk", true);

            // 방향 전환 체크
            if (horizontalInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (horizontalInput < 0 && facingRight)
            {
                Flip();
            }

            // 입력이 있을 때 타이머 초기화
            idleTimerCounter = 0f;
        }
        else
        {
            // 입력이 없을 때 타이머를 증가시킴
            idleTimerCounter += Time.deltaTime;

            // 타이머가 설정된 시간을 초과하면 Walk 애니메이션 비활성화
            if (idleTimerCounter >= idleTimer)
            {
                playerAnim.SetBool("Walk", false);
            }
        }
    }

    // 캐릭터 방향을 전환하는 메서드
    void Flip()
    {
        facingRight = !facingRight;
        playerSprite.flipX = !playerSprite.flipX;
    }
}
