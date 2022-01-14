using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance ?? (instance = FindObjectOfType<GameManager>());
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
    public GameState currentGameState;
    [SerializeField] public int waveCount = 15;
    public int currentWave = 2;
    public float backgroundLoopSpeed = 0.1f;

    private void Start()
    {
        currentGameState = GameState.Menu;
    }

    public void GameOver()
    {
        currentGameState = GameState.Menu;
        UIManager.Instance.DisableInGameScreen();
        UIManager.Instance.ActivateGameOverScreen();
        Observer.handleShooting?.Invoke();
    }

    public void StartGame()
    {
        currentGameState = GameState.Gameplay;
        UIManager.Instance.DisableStartScreen();
        UIManager.Instance.ActivateInGameScreen();
        Observer.handleShooting?.Invoke();
        EnemySpawner.Instance.SpawnEnemy();

    }
    public void NextWave()
    {
        InGameScreen.Instance.UpdateWave(currentWave);
        if(EnemySpawner.Instance.enemies.Count == 0)
        {
            Debug.Log("Next Wave");
            currentWave = currentWave != waveCount ? currentWave + 1 : currentWave;
            if(currentWave % 3 == 0)
            {
                currentGameState = GameState.BetweenScenes;
                Observer.handleShooting?.Invoke();
                StartCoroutine(NextSceneRoutine());
            }
            else
            {
                EnemySpawner.Instance.SpawnEnemy();
            } 
        }
        else
        {
            return;
        }
        
    }

    private IEnumerator NextSceneRoutine()
    {
        float duration = 2f;
        float totalTime = 0f;
        while (totalTime <= duration)
        {
            backgroundLoopSpeed = totalTime < duration / 2 ? backgroundLoopSpeed + 0.05f : backgroundLoopSpeed - 0.05f;
            totalTime += Time.deltaTime/duration;
            yield return null;
        }
        backgroundLoopSpeed = 0.1f;
        currentGameState = GameState.Gameplay;
        Observer.handleShooting?.Invoke();
        EnemySpawner.Instance.SpawnEnemy();
    }

}
