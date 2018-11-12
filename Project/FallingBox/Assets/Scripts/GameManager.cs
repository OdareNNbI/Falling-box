using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    public static event Action OnGameStarted;
    public static event Action OnMenuOpened; 
    public static event Action OnGameLosed;
    public static event Action OnMainButtonDown;
    
    [SerializeField] private List<GameObject> prefabManagers;

    private List<IManager> existedManagers = new List<IManager>();

    private void Awake()
    {
        Initialize();

        if (OnMenuOpened != null)
        {
            OnMenuOpened();
        }
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        
        UpdateManager(deltaTime);
    }

    public override void UpdateManager(float deltaTime)
    {
        for (int i = 0, n = existedManagers.Count; i < n; i++)
        {
            existedManagers[i].UpdateManager(deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (OnMainButtonDown != null)
            {
                OnMainButtonDown();
            }
        }
    }
    
    public override void Initialize()
    {
        base.Initialize();

        for (int i = 0, n = prefabManagers.Count; i < n; i++)
        {
            GameObject manager = Instantiate(prefabManagers[i], Vector3.zero, Quaternion.identity, transform);

            IManager iManager = manager.GetComponent<IManager>();

            if (iManager != null)
            {
                existedManagers.Add(iManager);
            }
        }
        
        existedManagers.ForEach((iManager) =>
        {
            iManager.Initialize();
        });
    }

    public void StartGame()
    {
        if (OnGameStarted != null)
        {
            OnGameStarted();
        }
    }

    public void LoseGame()
    {
        if (OnGameLosed != null)
        {
            OnGameLosed();
        }
    }
}