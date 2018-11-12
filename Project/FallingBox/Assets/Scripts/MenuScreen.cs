using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : BaseScreen
{
    [SerializeField] private Button startButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartButton_Click);
    }

    void OnDisable()
    {
        startButton.onClick.RemoveListener(StartButton_Click);
    }

    void StartButton_Click()
    {
        GameManager.Instance.StartGame();
    }
}
