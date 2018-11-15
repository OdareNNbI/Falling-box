using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : BaseManager<BackgroundManager>
{
    [SerializeField] private Background backgrounPrefab;

    private Background currentBackground;

    private void OnEnable()
    {
        GameManager.OnMenuOpened += GameManager_OnMenuOpened;
    }

    private void OnDisable()
    {
        GameManager.OnMenuOpened -= GameManager_OnMenuOpened;
    }

    public override void Initialize()
    {
        base.Initialize();

        currentBackground = Instantiate(backgrounPrefab, Vector3.zero, Quaternion.identity, transform);
        
        currentBackground.Initialize();
    }


    public override void UpdateManager(float deltaTime)
    {
        base.UpdateManager(deltaTime);

        if (currentBackground != null)
        {
            currentBackground.UpdateBackground(deltaTime);
        }
    }

    private void GameManager_OnMenuOpened()
    {
        Destroy(currentBackground.gameObject);
        currentBackground = Instantiate(backgrounPrefab, Vector3.zero, Quaternion.identity, transform);
        
        currentBackground.Initialize();
    }
}
