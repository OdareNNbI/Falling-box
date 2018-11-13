using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public const int PLATFORM_BUFFER = 5;
    public const float Y_OFFSET_BOX_UPPER_PLATFORM = 2f;
    
    [SerializeField] private Platform platformPrefab;
    [SerializeField] private Platform mainPlatformPrefab;
    [SerializeField] private Box boxPrefab;
    [SerializeField] private Spikes spikesPrefab;
    [SerializeField] private float minDistanceBetweenPlatforms;
    [SerializeField] private float maxDistanceBetweenPlatforms;
    [SerializeField] private float minTimeForSpikes;
    [SerializeField] private float maxTimeForPerfectTap;
    
    private List<Platform> existedPlatforms = new List<Platform>();
    private float lastPlatformYPosition;
    private float currentDistanceDetweenPlatform;
    private Box currentBox;
    private Spikes spikes;
    private Platform collisionPlatform;
    private bool isTapAvailable;
    private float currentTime;
    private bool isPerfectTap;

    public Box CurrentBox
    {
        get { return currentBox; }
    }


    private void OnEnable()
    {
        Box.OnCollide += Box_OnCollide;
        GameManager.OnMainButtonDown += GameManager_OnMainButtonDown;
        CameraManager.OnCameraMovedToTarget += CameraManager_OnCameraMovedToTarget;
    }

    private void OnDisable()
    {
        Box.OnCollide -= Box_OnCollide;
        GameManager.OnMainButtonDown -= GameManager_OnMainButtonDown;
        CameraManager.OnCameraMovedToTarget -= CameraManager_OnCameraMovedToTarget;
    }


    public void CreateLevel()
    {
        lastPlatformYPosition = CameraManager.Instance.CameraUpYPosition;
        currentDistanceDetweenPlatform = Random.Range(minDistanceBetweenPlatforms, maxDistanceBetweenPlatforms);
        
        currentBox = Instantiate(boxPrefab, Vector3.zero + Vector3.up * (Y_OFFSET_BOX_UPPER_PLATFORM), Quaternion.identity, transform);
        
        Platform mainPlatform = Instantiate(mainPlatformPrefab,
            Vector3.zero, Quaternion.identity, transform);
        
        existedPlatforms.Add(mainPlatform);

        lastPlatformYPosition = mainPlatform.transform.position.y;
        currentDistanceDetweenPlatform = Random.Range(minDistanceBetweenPlatforms, maxDistanceBetweenPlatforms);

        spikes = Instantiate(spikesPrefab, CameraManager.Instance.MainCamera.transform);
        spikes.CreateSpikes();
        
        isTapAvailable = false;
        currentTime = 0f;
        
        CreatePlatform(PLATFORM_BUFFER);
    }

    public void DestroyLevel()
    {
        Destroy(spikes.gameObject);
        Destroy(currentBox.gameObject);
    }
    

    public void UpdateLevel(float deltaTime)
    {
        if (isTapAvailable)
        {
            currentTime += deltaTime;

            if (currentTime >= minTimeForSpikes)
            {
                spikes.MoveSpikes(deltaTime, false);
            }

            if (isPerfectTap)
            {
                if (currentTime >= maxTimeForPerfectTap)
                {
                    isPerfectTap = false;
                }
                else
                {
                    spikes.MoveSpikes(deltaTime, true);
                }
            }
        }

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
            GameManager.Instance.LoseGame();
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

            existedPlatforms.Add(platform);
        }
    }

    private void Box_OnCollide(Platform platform)
    {
        collisionPlatform = platform;
    }

    private void GameManager_OnMainButtonDown()
    {
        if (collisionPlatform != null && isTapAvailable)
        {
            currentBox.StartFalling();
            collisionPlatform.DisableCollision();
            collisionPlatform = null;
            isTapAvailable = false;

            if (currentTime <= maxTimeForPerfectTap)
            {
                isPerfectTap = true;
            }
            else
            {
                isPerfectTap = false;
            }
        }
    }

    private void CameraManager_OnCameraMovedToTarget()
    {
        isTapAvailable = true;

        currentTime = 0f;
    }
}
