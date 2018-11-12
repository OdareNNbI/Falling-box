using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseManager<LevelManager>
{
    [SerializeField] private Level level;

    private Level currentLevel;

    public Level CurrentLevel
    {
        get { return currentLevel; }
    }

    private void OnEnable()
    {
        GameManager.OnGameStarted += GameManager_OnGameStarted;
        GameManager.OnGameLosed += GameManager_OnGameLosed;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.OnGameLosed -= GameManager_OnGameLosed;
    }

    void CreateLevel()
    {
        currentLevel = Instantiate(level, Vector3.zero, Quaternion.identity, transform);
        if (currentLevel != null)
        {
            currentLevel.CreateLevel();
        }
    }

    void DestroyLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);

            currentLevel = null;
        }
    }

    public override void UpdateManager(float deltaTime)
    {
        if (currentLevel != null)
        {
            currentLevel.UpdateLevel(deltaTime);
        }
    }

    void GameManager_OnGameStarted()
    {
        CreateLevel();
    }

    void GameManager_OnGameLosed()
    {
        DestroyLevel();
    }
}
