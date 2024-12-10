using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform JoystickBackground;
    public RectTransform Handler;
    public float moveSpeed = 5f;
    public GameObject player;

    private Vector2 inputVector;
    private Vector3 moveVector;
    private Vector2 joystickCenter;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        inputVector = Vector2.zero;
        joystickCenter = JoystickBackground.anchoredPosition;
        Handler.anchoredPosition = joystickCenter;

        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
            playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickBackground, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / JoystickBackground.sizeDelta.x) * 2;
            position.y = (position.y / JoystickBackground.sizeDelta.y) * 2;

            inputVector = new Vector2(position.x, position.y);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            Handler.anchoredPosition = new Vector2(inputVector.x * (JoystickBackground.sizeDelta.x / 2), inputVector.y * (JoystickBackground.sizeDelta.y / 2)) + joystickCenter;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        Handler.anchoredPosition = joystickCenter;

        if (playerAnimator != null)
        {
            playerAnimator.SetBool("Run", false);
        }
    }

    void Update()
    {
        moveVector = (Vector3.right * inputVector.x) * moveSpeed * Time.deltaTime;
        player.transform.Translate(moveVector);

        float h = Input.GetAxis("Horizontal");
        player.transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);

        bool isMoving = inputVector.x != 0 || h != 0;
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("Run", isMoving);
        }

        if (playerSpriteRenderer != null)
        {
            if (inputVector.x < 0 || h < 0)
            {
                playerSpriteRenderer.flipX = true;
            }
            else if (inputVector.x > 0 || h > 0)
            {
                playerSpriteRenderer.flipX = false;
            }
        }
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }
}
