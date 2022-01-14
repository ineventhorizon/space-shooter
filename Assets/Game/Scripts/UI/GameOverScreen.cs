using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : UIBase
{
    private static GameOverScreen instance;
    public static GameOverScreen Instance => instance ?? (instance = FindObjectOfType<GameOverScreen>());
    // Start is called before the first frame update
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

    public void RestartGame()
    {
        SpaceShooter.ScreenManager.Instance.RestartActiveScene();
    }
}
