using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseManager<LevelManager>
{
    [SerializeField] private Level level;

    private Level currentLevel;

    private void Start()
    {
        CreateLevel();
    }

    void CreateLevel()
    {
        currentLevel = Instantiate(level, Vector3.zero, Quaternion.identity, transform);
        if (currentLevel != null)
        {
            currentLevel.CreateLevel();
        }
    }

    private void Update()
    {
        currentLevel.UpdateLevel(Time.deltaTime);
    }
}
