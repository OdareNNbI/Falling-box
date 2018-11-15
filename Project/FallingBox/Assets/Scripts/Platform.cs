using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using Random = System.Random;


public class Platform : MonoBehaviour
{
    private const float STAR_OFFSET = 0.7f;
    
	[SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private SpriteRenderer platformSprite;
    [SerializeField] [Range(0f, 1f)] private float leftMoveChance;
    [SerializeField] [Range(0f, 1f)] private float starChance;
    [SerializeField] private GameObject starPrefab;

    private int moveCoefficient;
    private float speed;
    private int platformNumber;

    public int PlatformNumber
    {
        get { return platformNumber; }
    }
    
    float SpriteHalfXSize
    {
        get
        {
            return platformSprite.bounds.extents.x;
        }
    }

    public void Initialize(int number)
    {
        platformNumber = number;
        moveCoefficient = 1;
        if (UnityEngine.Random.Range(0f, 1f) < leftMoveChance)
        {
            moveCoefficient = -1;
        }

        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);

        if (UnityEngine.Random.Range(0f, 1f) < starChance)
        {
            GameObject star = Instantiate(starPrefab, transform);
            
            star.transform.localPosition = new Vector3(0f, STAR_OFFSET);
        }
    }

    public void PlatformUpdate(float deltaTime)
    {
        float cameraLeftXPosition = CameraManager.Instance.CameraLeftXPosition;
        float cameraRightXPosition = CameraManager.Instance.CameraRightXPosition;

        if (moveCoefficient == 1)
        {
            if (transform.position.x + SpriteHalfXSize >= cameraRightXPosition)
            {
                moveCoefficient = -1;
            }
        }
        else if (moveCoefficient == -1)
        {
            if (transform.position.x - SpriteHalfXSize <= cameraLeftXPosition)
            {
                moveCoefficient = 1;
            }
        }

        transform.Translate(Vector3.right * speed * moveCoefficient * deltaTime);

    }

    public void DisableCollision()
    {
        boxCollider2D.enabled = false;
    }
}
