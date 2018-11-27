using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : BaseScreen
{
    public static event Action OnClaimButtonClicked;
    
    [SerializeField] private TextMeshProUGUI scoreNumberText;
    [SerializeField] private TextMeshProUGUI maxScoreText;
    [SerializeField] private Button okButton;

    private void OnEnable()
    {
        okButton.onClick.AddListener(OkButton_OnClick);
    }

    private void OnDisable()
    {    
        okButton.onClick.RemoveListener(OkButton_OnClick);
    }

    public override void ShowScreen()
    {
        base.ShowScreen();

        scoreNumberText.text = LevelManager.Instance.CurrentLevel.Score.ToString();
        maxScoreText.text = GameManager.Instance.MaxScore.ToString();
    }

    void OkButton_OnClick()
    {
        if (OnClaimButtonClicked != null)
        {
            OnClaimButtonClicked();
        }
    }
}
