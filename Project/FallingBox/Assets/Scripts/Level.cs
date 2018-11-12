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
    [SerializeField] private float minDistanceBetweenPlatforms;
    [SerializeField] private float maxDistanceBetweenPlatforms;
    
    private List<Platform> existedPlatforms = new List<Platform>();
    private float lastPlatformYPosition;
    private float currentDistanceDetweenPlatform;
    private Box currentBox;
    private Platform collisionPlatform;


    public void CreateLevel()
    {
        lastPlatformYPosition = Camera.main.ViewportToWorldPoint(Vector2.up).y;
        currentDistanceDetweenPlatform = Random.Range(minDistanceBetweenPlatforms, maxDistanceBetweenPlatforms);
        
        currentBox = Instantiate(boxPrefab, Vector2.up * (lastPlatformYPosition - currentDistanceDetweenPlatform + Y_OFFSET_BOX_UPPER_PLATFORM), Quaternion.identity, transform);
        
        currentBox.OnCollide += CurrentBox_OnCollide;
        
        Platform mainPlatform = Instantiate(mainPlatformPrefab,
            Vector2.up * (lastPlatformYPosition - currentDistanceDetweenPlatform), Quaternion.identity, transform);
        
        existedPlatforms.Add(mainPlatform);

        lastPlatformYPosition = mainPlatform.transform.position.y;
        currentDistanceDetweenPlatform = Random.Range(minDistanceBetweenPlatforms, maxDistanceBetweenPlatforms);
        
        CreatePlatform(PLATFORM_BUFFER);
    }


    public void UpdateLevel(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (collisionPlatform != null)
            {
                currentBox.StartFalling();
                collisionPlatform.DisableCollision();
                collisionPlatform = null;
            }
        }
        
        
        if (existedPlatforms.Count < PLATFORM_BUFFER)
        {
            CreatePlatform(PLATFORM_BUFFER - existedPlatforms.Count);
        }
        
        int deletedPlatformsCount = 0;
        float cameraUpYPosition = Camera.main.ViewportToWorldPoint(Vector2.one).y;
        for (int i = existedPlatforms.Count - 1; i >= 0; i--)
        {
            if (existedPlatforms[i].transform.position.y >= cameraUpYPosition)
            {
                deletedPlatformsCount++;
                
                Destroy(existedPlatforms[i].gameObject);
                existedPlatforms.RemoveAt(i);
            }
            else if (currentBox.transform.position.y <= existedPlatforms[i].SpriteUpYPosition)
            {
                existedPlatforms[i].DisableCollision();
            }
        }

        if (deletedPlatformsCount != 0)
        {
            CreatePlatform(deletedPlatformsCount);
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

    private void CurrentBox_OnCollide(Platform platform)
    {
        collisionPlatform = platform;
    }
}
