using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private static int numRaindropsGround;
    private static int numRaindropsBucket;

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
    }

    private void OnDestroy()
    {
        RaindropMove.OnRaindropDestroyed -= RaindropDestroyedCount;
    }
}
