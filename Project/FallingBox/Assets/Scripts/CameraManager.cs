using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : BaseManager<CameraManager>
{
    [SerializeField] private Camera mainCamera;
    
    [Header("Camera follower")]
    [SerializeField] private float cameraYOffset;
    [SerializeField] private float speed;

    private bool isMoveToBoxAvailable;
    private float endYPosition;

    public float CameraUpYPosition
    {
        get
        {
            return mainCamera.ViewportToWorldPoint(Vector2.one).y;
        }
    }

    public float CameraLeftXPosition
    {
        get
        {
            return mainCamera.ViewportToWorldPoint(new Vector2(0, 0)).x;
        }
    }

    public float CameraRightXPosition
    {
        get
        {
            return mainCamera.ViewportToWorldPoint(new Vector2(1, 1)).x;
        }
    }

    private void OnEnable()
    {
        Box.OnCollide += Box_OnCollide;
    }

    private void OnDisable()
    {
        Box.OnCollide -= Box_OnCollide;
    }

    private void Update()
    {
        if (isMoveToBoxAvailable)
        {
            if (mainCamera.transform.position.y <= endYPosition)
            {
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, endYPosition, mainCamera.transform.position.z);
                isMoveToBoxAvailable = false;
            }
            else
            {
                mainCamera.transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }
    }

    private void Box_OnCollide(Platform platform)
    {
        isMoveToBoxAvailable = true;

        endYPosition = LevelManager.Instance.CurrentLevel.CurrentBox.transform.position.y + cameraYOffset;
    }
}
