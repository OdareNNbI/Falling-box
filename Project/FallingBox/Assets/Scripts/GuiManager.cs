using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : BaseManager<GuiManager>
{
    [SerializeField] private List<BaseScreen> screensPrefab;

    private BaseScreen currentActiveScreen;
    private List<BaseScreen> existedScreens = new List<BaseScreen>();

    private void OnEnable()
    {
        GameManager.OnGameStarted += GameManager_OnGameStarted;
        GameManager.OnMenuOpened += GameManager_OnMenuOpened;
        GameManager.OnGameLosed += GameManager_OnGameLosed;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= GameManager_OnMenuOpened;
        GameManager.OnMenuOpened -= GameManager_OnMenuOpened;
        GameManager.OnGameLosed -= GameManager_OnGameLosed;
    }

    public override void Initialize()
    {
        base.Initialize();

        for (int i = 0, n = screensPrefab.Count; i < n; i++)
        {
            existedScreens.Add(Instantiate(screensPrefab[i], transform));
            
            existedScreens[i].HideScreen();
        }
    }

    void ShowScreenByType(ScreenType screenType)
    {
        BaseScreen screen = existedScreens.Find((baseScreen) =>
        {
            return baseScreen.Type == screenType;
        });

        if (screen != null)
        {
            if (currentActiveScreen != null)
            {
                currentActiveScreen.HideScreen();
            }

            screen.ShowScreen();
            currentActiveScreen = screen;
        }
    }

    private void GameManager_OnGameStarted()
    {
        ShowScreenByType(ScreenType.Game);
    }

    private void GameManager_OnMenuOpened()
    {
        ShowScreenByType(ScreenType.Menu);
    }

    private void GameManager_OnGameLosed()
    {
        ShowScreenByType(ScreenType.Menu);
    }
}
