using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    public void DisablePanel()
    {
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        StartCoroutine(DisablePanelRoutine());
        //canvasGroup.alpha = 0f;
    }

    public void EnablePanel()
    {
        canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    private IEnumerator DisablePanelRoutine()
    {
        while (true)
        {
            if (canvasGroup.alpha == 0f) break;
            canvasGroup.alpha -= 0.1f;
            yield return null;
        }
    }
}
