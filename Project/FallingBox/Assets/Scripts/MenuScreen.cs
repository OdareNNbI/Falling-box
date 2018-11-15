using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : BaseScreen
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button shopButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartButton_Click);
        shopButton.onClick.AddListener(ShopButton_Click);
    }

    void OnDisable()
    {
        startButton.onClick.RemoveListener(StartButton_Click);
        shopButton.onClick.RemoveListener(ShopButton_Click);
    }

    void StartButton_Click()
    {
        GameManager.Instance.StartGame();
    }

    void ShopButton_Click()
    {
        GuiManager.Instance.ShowScreenByType(ScreenType.Shop);
    }
}
