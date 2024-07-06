using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        GetNextRandom(spawnDelayBounds);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnAt > spawnDelay)
        {
            SpawnRaindrop();
            spawnDelay = GetNextRandom(spawnDelayBounds);
            lastSpawnAt = Time.time;
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
}
