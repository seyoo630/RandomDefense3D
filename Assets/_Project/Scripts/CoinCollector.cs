using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public GameObject bronzeCoinPrefab;
    public GameObject silverCoinPrefab;
    public GameObject goldCoinPrefab;
    public GameOverManager gameOverManager; // GameOverManager ����

    private SpriteRenderer bronzeCoinRenderer;
    private SpriteRenderer silverCoinRenderer;
    private SpriteRenderer goldCoinRenderer;

    private bool bronzeCoinCollected = false;
    private bool silverCoinCollected = false;
    private bool goldCoinCollected = false;

    void Start()
    {
        bronzeCoinRenderer = bronzeCoinPrefab.GetComponent<SpriteRenderer>();
        silverCoinRenderer = silverCoinPrefab.GetComponent<SpriteRenderer>();
        goldCoinRenderer = goldCoinPrefab.GetComponent<SpriteRenderer>();

        // �ʱ� ������ �������� ����
        bronzeCoinRenderer.color = Color.black;
        silverCoinRenderer.color = Color.black;
        goldCoinRenderer.color = Color.black;

        // GameOverManager�� ã��
        if (gameOverManager == null)
        {
            gameOverManager = FindObjectOfType<GameOverManager>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BronzeCoin"))
        {
            CollectCoin(bronzeCoinRenderer);
            bronzeCoinCollected = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("SilverCoin"))
        {
            CollectCoin(silverCoinRenderer);
            silverCoinCollected = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("GoldCoin"))
        {
            CollectCoin(goldCoinRenderer);
            goldCoinCollected = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            if (bronzeCoinCollected && silverCoinCollected && goldCoinCollected)
            {
                FinishGame();
            }
        }
    }

    void CollectCoin(SpriteRenderer coinRenderer)
    {
        coinRenderer.color = Color.white; // ������ ������ ������� �����Ͽ� ���� ���� ǥ��
    }

    void FinishGame()
    {
        Debug.Log("Game Finished! All coins collected.");
        if (gameOverManager != null)
        {
            gameOverManager.GameOver(); // ���� ���� ȭ���� ǥ��
        }
        else
        {
            Debug.LogError("CoinCollector: gameOverManager is not assigned.");
        }
    }
}
