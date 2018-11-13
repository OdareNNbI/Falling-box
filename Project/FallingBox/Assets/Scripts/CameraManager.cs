using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : BaseManager<CameraManager>
{
    public static event Action OnCameraMovedToTarget;
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 defaultCameraPosition;
    
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
            return mainCamera.ViewportToWorldPoint(Vector2.zero).x;
        }
    }

    public float CameraRightXPosition
    {
        get
        {
            return mainCamera.ViewportToWorldPoint(Vector2.one).x;
        }
    }

    public float CameraDownYPosition
    {
        get
        {
            return mainCamera.ViewportToWorldPoint(Vector2.zero).y;
        }
    }

    public Camera MainCamera
    {
        get { return mainCamera; }
    }

    private void OnEnable()
    {
        Box.OnCollide += Box_OnCollide;
        GameManager.OnGameLosed += GameManager_OnGameLosed;
    }

    private void OnDisable()
    {
        Box.OnCollide -= Box_OnCollide;
        GameManager.OnMenuOpened -= GameManager_OnGameLosed;
    }

    public override void Initialize()
    {
        mainCamera.transform.position = defaultCameraPosition;
    }

    public override void  UpdateManager(float deltaTime)
    {
        if (isMoveToBoxAvailable)
        {
            if (mainCamera.transform.position.y <= endYPosition)
            {
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, endYPosition, mainCamera.transform.position.z);
                isMoveToBoxAvailable = false;

                if (OnCameraMovedToTarget != null)
                {
                    OnCameraMovedToTarget();
                }
            }
            else
            {
                mainCamera.transform.Translate(Vector3.down * speed * deltaTime);
            }
        }
    }

    private void Box_OnCollide(Platform platform)
    {
        isMoveToBoxAvailable = true;

        endYPosition = LevelManager.Instance.CurrentLevel.CurrentBox.transform.position.y + cameraYOffset;
    }

    void GameManager_OnGameLosed()
    {
        mainCamera.transform.position = defaultCameraPosition;
        isMoveToBoxAvailable = false;
    }
}
