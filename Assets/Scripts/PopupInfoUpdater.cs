using System;
using UnityEngine;
using TMPro;

public class PopupInfoUpdater : MonoBehaviour
{
    [SerializeField] private GameObject popupObject;

    [Header("Text fields")]
    [SerializeField] private TextMeshProUGUI textTerrain;
    [SerializeField] private TextMeshProUGUI textTargetWater;
    [SerializeField] private TextMeshProUGUI textGroundWater;
    [SerializeField] private TextMeshProUGUI textBucketWater;

    // Start is called before the first frame update
    void Start()
    {
        popupObject.SetActive(false);   // good extra in order to not have to care about the popup active state in editor

        LevelManager.Instance.OnLevelTimeOver += OnLevelOverFillPopup;
    }

    private void OnLevelOverFillPopup()
    {
        popupObject.SetActive(true);    // in a better (slower-paced) world, this would be animated

        // get ground/bucket water as percentages
        int groundWaterPercent = Mathf.CeilToInt((float)LevelManager.Instance.numRaindropsGround / (LevelManager.Instance.numRaindropsGround + LevelManager.Instance.numRaindropsBucket) * 100f);
        int bucketWaterPercent = 100 - groundWaterPercent;

        // update text infos (lazy - initial/static text is assumed to be set in inspector, here only append level stats)
        textTerrain.text = textTerrain.text + Enum.GetName(typeof(TerrainType), GroundManager.Instance.thisLevelTerrain);  // DECOUPLE THIS?
        textTargetWater.text = textTargetWater.text + LevelManager.Instance.targetWaterPercent + "%";
        textGroundWater.text = textGroundWater.text + groundWaterPercent + "%";
        textBucketWater.text = textBucketWater.text + bucketWaterPercent + "%)";    // bonus hack for the text I decided should be in parentheses too

        // color ground percent text as a minimal win/lose condition
        if (groundWaterPercent > LevelManager.Instance.targetWaterPercent - LevelManager.Instance.targetPercentMargin &&
            groundWaterPercent < LevelManager.Instance.targetWaterPercent + LevelManager.Instance.targetPercentMargin)
        {
            textGroundWater.color = LevelManager.Instance.targetSuccessColor;
        }
        else
        {
            textGroundWater.color = LevelManager.Instance.targetFailColor;
        }
    }


    void OnDestroy()
    {
        LevelManager.Instance.OnLevelTimeOver -= OnLevelOverFillPopup;
    }
}
