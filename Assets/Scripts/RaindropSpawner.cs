using UnityEngine;

public class RaindropSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;

    [SerializeField] private Vector2 xBounds;
    [SerializeField] private Vector2 yStartOffset;
    [SerializeField] private Vector2 yStartVelocity;
    [SerializeField] private Vector2 yStartAcceleration;
    [SerializeField] private Vector2 spawnDelayBounds;

    private float spawnDelay;
    private float lastSpawnAt;

    private bool levelStarted = false;
    private bool levelOver = false;

    // Start is called before the first frame update
    void Start()
    {
        GetNextRandom(spawnDelayBounds);

        LevelManager.Instance.OnLevelGameplayStarted += OnLevelStartedStartSpawning;
        LevelManager.Instance.OnLevelTimeOver += OnLevelTimeOverStopSpawning;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelStarted && !levelOver)
        {
            if (Time.time - lastSpawnAt > spawnDelay)
            {
                SpawnRaindrop();
                spawnDelay = GetNextRandom(spawnDelayBounds);
                lastSpawnAt = Time.time;
            }
        }
    }

    private void SpawnRaindrop()
    {
        GameObject raindrop = Instantiate(dropPrefab, new Vector2(transform.position.x + GetNextRandom(xBounds),
                                                                  transform.position.y + GetNextRandom(yStartOffset)),
                                                      Quaternion.identity);
        RaindropMove script = raindrop.GetComponent<RaindropMove>();
        script.InitSetVelocity(GetNextRandom(-yStartVelocity));
        script.InitSetAcceleration(GetNextRandom(-yStartAcceleration));
    }

    private float GetNextRandom(Vector2 fromRange)
    {
        return Random.Range(fromRange.x, fromRange.y);
    }

    private void OnLevelStartedStartSpawning()
    {
        levelStarted = true;
    }

    private void OnLevelTimeOverStopSpawning()
    {
        levelOver = true;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnLevelGameplayStarted -= OnLevelStartedStartSpawning;
        LevelManager.Instance.OnLevelTimeOver -= OnLevelTimeOverStopSpawning;
    }
}
