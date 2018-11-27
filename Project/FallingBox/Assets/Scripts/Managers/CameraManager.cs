using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : BaseManager<CameraManager>
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 defaultCameraPosition;
    
    [Header("Camera follower")]
    [SerializeField] private float speed;

    private bool isMoveToBoxAvailable;
    private float currentTime;

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
        GameManager.OnGameLosed += GameManager_OnGameLosed;
        GameManager.OnGameStarted += GameManager_OnGameStarted;
        GameManager.OnMenuOpened += GameManager_OnMenuOpened;
    }

    private void OnDisable()
    {
        GameManager.OnMenuOpened -= GameManager_OnGameLosed;
        GameManager.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.OnMenuOpened -= GameManager_OnMenuOpened;
    }

    public override void Initialize()
    {
        mainCamera.transform.position = defaultCameraPosition;
    }

    public override void UpdateManager(float deltaTime)
    {
        if (isMoveToBoxAvailable)
        {
            mainCamera.transform.Translate(Vector3.down * speed * currentTime * deltaTime);
            currentTime += deltaTime;
        }
    }
    
    
    void GameManager_OnGameLosed()
    {
        isMoveToBoxAvailable = false;
    }

    void GameManager_OnGameStarted()
    {
        isMoveToBoxAvailable = true;
        currentTime = 0f;
    }

    void GameManager_OnMenuOpened()
    {
        mainCamera.transform.position = defaultCameraPosition;
    }
}
