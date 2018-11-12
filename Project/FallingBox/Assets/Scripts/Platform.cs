using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class Platform : MonoBehaviour
{
	[SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private SpriteRenderer platformSprite;
    [SerializeField] [Range(0f, 1f)] private float leftMoveChance;

    private int moveCoefficient;
    private float speed;

    public float SpriteUpYPosition
    {
        get { return transform.position.y + platformSprite.bounds.extents.y * 2; }
    }
    
    float SpriteHalfXSize
    {
        get
        {
            return platformSprite.bounds.extents.x;
        }
    }

    private void Start()
    {
        moveCoefficient = 1;
        if (UnityEngine.Random.Range(0f, 1f) < leftMoveChance)
        {
            moveCoefficient = -1;
        }

        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        float cameraLeftXPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        float cameraRightXPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x;
        float cameraCenterXPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1)).x;

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

        transform.Translate(Vector3.right * speed * moveCoefficient * Time.fixedDeltaTime);

    }

    public void DisableCollision()
    {
        boxCollider2D.enabled = false;
    }
}
