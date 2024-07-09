using System;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private static int numRaindropsGround;
    private static int numRaindropsBucket;

    [SerializeField] private SpriteRenderer bucketWaterSprite;
    [SerializeField] private Sprite[] bucketWaterStates;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float maxRoundTime;
    private float timeLeft;

    public event Action OnLevelTimeOver;
    private bool timeAlreadyOver = false;

    // potato singleton
    private void Awake()
    {
        if (Instance != null) 
            Destroy(Instance.gameObject);

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        RaindropMove.OnRaindropDestroyed += RaindropDestroyedCount;

        timeLeft = maxRoundTime;
        timerText.text = maxRoundTime.ToString();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            // ONLY ONCE end round - stop spawns, do popup...
            if (!timeAlreadyOver)
            {
                OnLevelTimeOver?.Invoke();
                timeAlreadyOver = true;
            }
            timeLeft = 0f;
        }

        // update UI
        timerText.text = Mathf.CeilToInt(timeLeft).ToString();
    }

    private void RaindropDestroyedCount(bool bucketCollected, Vector3 atPosition)
    {
        if (bucketCollected)
        {
            numRaindropsBucket++;
            Debug.Log("Drops in bucket: " + numRaindropsBucket);
        }
        else
        {
            numRaindropsGround++;
            Debug.Log("Drops on ground: " + numRaindropsGround);
        }

        // check for number of raindrops in bucket and adjust bucket sprite:
        switch (numRaindropsBucket)
        {
            case 10:    // hardcoded...
                bucketWaterSprite.sprite = bucketWaterStates[0];
                break;
            case 20:
                bucketWaterSprite.sprite = bucketWaterStates[1];
                break;
            case 30:
                bucketWaterSprite.sprite = bucketWaterStates[2];
                break;
            case 40:
                bucketWaterSprite.sprite = bucketWaterStates[3];
                break;
            case 50:
                bucketWaterSprite.sprite = bucketWaterStates[4];
                break;
            default:    // any other number should trigger no change
                break;
        }
    }

    private void OnDestroy()
    {
        RaindropMove.OnRaindropDestroyed -= RaindropDestroyedCount;
    }
}
