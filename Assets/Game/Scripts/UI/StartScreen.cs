using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreen : UIBase
{
    [SerializeField] private TextMeshProUGUI tapToPlayText;
    private static StartScreen instance;
    public static StartScreen Instance => instance ?? (instance = FindObjectOfType<StartScreen>());
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
    private void Update()
    {
        var textScale = tapToPlayText.transform.localScale;
        textScale.x = Mathf.LerpUnclamped(1f, 1.3f, Mathf.Sin(Time.time));
        textScale.y = textScale.x;
        tapToPlayText.transform.localScale = textScale;
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
