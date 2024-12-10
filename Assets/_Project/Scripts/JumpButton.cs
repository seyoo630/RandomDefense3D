using UnityEngine;
using UnityEngine.UI;

public class JumpHandler : MonoBehaviour
{
    public Button jumpButton;
    public float jumpForce = 5f;
    public GameObject player;

    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    private PlayerGroundCheck groundCheck; // GroundCheck 스크립트를 참조합니다.

    void Start()
    {
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>();
            playerAnimator = player.GetComponent<Animator>();
            groundCheck = player.GetComponent<PlayerGroundCheck>(); // PlayerGroundCheck 스크립트를 가져옵니다.
        }

        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(OnJumpButtonPressed);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
    }

    private void OnJumpButtonPressed()
    {
        TryJump();
    }

    private void TryJump()
    {
        if (groundCheck != null && groundCheck.CanJump())
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            groundCheck.ConsumeJump();

            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("Jump");
            }
        }
    }
}
