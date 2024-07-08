using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private static int numRaindropsGround;
    private static int numRaindropsBucket;

    [SerializeField] private SpriteRenderer bucketWaterSprite;
    [SerializeField] private Sprite[] bucketWaterStates;

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
