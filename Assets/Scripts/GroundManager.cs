using UnityEngine;

public enum TerrainType
{
    Grass = 1,
    Sand = 2,
    Snow = 3,
    Redsand = 4,
}

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance;

    [SerializeField] private GameObject groundRootObject;
    [SerializeField] public TerrainType thisLevelTerrain;
    [SerializeField] private Sprite terrainSprite; // potato implementation: manually pass sprite for ground tiles.
                                                   // FUTURE: should be coupled TerrainType<->Sprite - test SerializedDictionary asset.

    // another potato singleton
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    private void Start()
    {
        // visually edit the ground tiles according to desired type ("type")
        foreach (Transform t in groundRootObject.transform)
        {
            t.GetComponent<SpriteRenderer>().sprite = terrainSprite;
        }
    }
}
