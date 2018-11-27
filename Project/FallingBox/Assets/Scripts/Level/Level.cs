using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    public const int PLATFORM_BUFFER = 10;
    public const float Y_OFFSET_BOX_UPPER_PLATFORM = 2f;

    public static event Action<int> OnScoreChanged;
    
    [SerializeField] private Platform platformPrefab;
    [SerializeField] private Platform mainPlatformPrefab;
    [SerializeField] private List<Box> boxPrefabs;
    [SerializeField] private float minDistanceBetweenPlatforms;
    [SerializeField] private float maxDistanceBetweenPlatforms;

    static int lastPlatformNumber;
    
    private List<Platform> existedPlatforms = new List<Platform>();
    private float lastPlatformYPosition;
    private float currentDistanceDetweenPlatform;
    private Box currentBox;
    private Platform collisionPlatform;
    private int lastCollidedPlatformNumber = 0;
    private int score;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            if (OnScoreChanged != null)
            {
                OnScoreChanged(score);
            }
        }
    }


    private void OnEnable()
    {
        Box.OnCollide += Box_OnCollide;
        GameManager.OnMainButtonDown += GameManager_OnMainButtonDown;
    }

    private void OnDisable()
    {
        Box.OnCollide -= Box_OnCollide;
        GameManager.OnMainButtonDown -= GameManager_OnMainButtonDown;
    }


    public void CreateLevel()
    {
        lastPlatformYPosition = CameraManager.Instance.CameraUpYPosition;
        currentDistanceDetweenPlatform = Random.Range(minDistanceBetweenPlatforms, maxDistanceBetweenPlatforms);

        Box boxPrefab = boxPrefabs.Find((temp) =>
            {
                return temp.BoxType == (BoxType) PlayerPrefs.GetInt(Prefs.CURRENT_BOX, 0);
            });
        currentBox = Instantiate(boxPrefab, Vector3.zero + Vector3.up * (Y_OFFSET_BOX_UPPER_PLATFORM), Quaternion.identity, transform);
        
        Platform mainPlatform = Instantiate(mainPlatformPrefab,
            Vector3.zero, Quaternion.identity, transform);
        lastPlatformNumber = 0;
        mainPlatform.Initialize(lastPlatformNumber);
        lastPlatformNumber++;
        
        existedPlatforms.Add(mainPlatform);

        lastPlatformYPosition = mainPlatform.transform.position.y;
        currentDistanceDetweenPlatform = Random.Range(minDistanceBetweenPlatforms, maxDistanceBetweenPlatforms);
        
        Score = 0;
        
        CreatePlatform(PLATFORM_BUFFER);
    }

    public void DestroyLevel()
    {
        Destroy(currentBox.gameObject);
    }
    

    public void UpdateLevel(float deltaTime)
    {
        if (existedPlatforms.Count < PLATFORM_BUFFER)
        {
            CreatePlatform(PLATFORM_BUFFER - existedPlatforms.Count);
        }

        for (int i = 0, n = existedPlatforms.Count; i < n; i++)
        {
            existedPlatforms[i].PlatformUpdate(deltaTime);
        }
        
        int deletedPlatformsCount = 0;
        float cameraUpYPosition = CameraManager.Instance.CameraUpYPosition;
        for (int i = existedPlatforms.Count - 1; i >= 0; i--)
        {
            if (existedPlatforms[i].transform.position.y >= cameraUpYPosition)
            {
                deletedPlatformsCount++;
                
                Destroy(existedPlatforms[i].gameObject);
                existedPlatforms.RemoveAt(i);
            }
        }

        if (deletedPlatformsCount != 0)
        {
            CreatePlatform(deletedPlatformsCount);
        }

        if (currentBox.transform.position.y <= CameraManager.Instance.CameraDownYPosition)
        {
            GameManager.Instance.LoseGame(Score);
        }
    }


    void CreatePlatform(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 platformPosition = new Vector3(0f, lastPlatformYPosition - currentDistanceDetweenPlatform);

            lastPlatformYPosition = platformPosition.y;
            currentDistanceDetweenPlatform = Random.Range(minDistanceBetweenPlatforms, maxDistanceBetweenPlatforms);

            Platform platform = Instantiate(platformPrefab, platformPosition, Quaternion.identity, transform);
            platform.Initialize(lastPlatformNumber);
            lastPlatformNumber++;

            existedPlatforms.Add(platform);
        }
    }

    private void Box_OnCollide(Platform platform)
    {
        collisionPlatform = platform;
        
        Score += Mathf.Abs(lastCollidedPlatformNumber - platform.PlatformNumber);
        lastCollidedPlatformNumber = platform.PlatformNumber;
    }

    private void GameManager_OnMainButtonDown()
    {
        if (collisionPlatform != null)
        {
            currentBox.StartFalling();
            collisionPlatform.DisableCollision();
            collisionPlatform = null;
        }
    }
}
