using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public int maxJumpCount = 2;
    private int currentJumpCount;

    void Start()
    {
        currentJumpCount = maxJumpCount;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            currentJumpCount = maxJumpCount;
        }
    }

    public bool CanJump()
    {
        return currentJumpCount > 0;
    }

    public void ConsumeJump()
    {
        currentJumpCount--;
    }
}
