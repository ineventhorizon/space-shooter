    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager instance;
    public static UIManager Instance => instance ?? (instance = FindObjectOfType<UIManager>());
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion
    public void DisableStartScreen()
    {
        StartScreen.Instance.DisablePanel();
    }

    public void ActivateStartScreen()
    {
        StartScreen.Instance.EnablePanel();
    }

    public void DisableInGameScreen()
    {
        InGameScreen.Instance.DisablePanel();
    }

    public void ActivateInGameScreen()
    {
        InGameScreen.Instance.EnablePanel();
    }

    public void DisableGameOverScreen()
    {
        GameOverScreen.Instance.DisablePanel();
    }

    public void ActivateGameOverScreen()
    {
        GameOverScreen.Instance.EnablePanel();
    }
}
