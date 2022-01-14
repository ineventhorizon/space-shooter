using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Material backgroundMaterial;
    private float speed;
    private Vector2 offset;
    private void OnEnable()
    {
        backgroundMaterial.SetTextureOffset("_MainTex", Vector2.zero);
    }
    private void OnDisable()
    {
        backgroundMaterial.SetTextureOffset("_MainTex", Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundLoop();
    }

    private void BackgroundLoop()
    {
        speed = GameManager.Instance.backgroundLoopSpeed;
        //Debug.Log(GameManager.Instance.currentGameState);
        offset = backgroundMaterial.mainTextureOffset;
        offset.y += Time.deltaTime * speed;
        backgroundMaterial.SetTextureOffset("_MainTex", offset);
        
    }
}
