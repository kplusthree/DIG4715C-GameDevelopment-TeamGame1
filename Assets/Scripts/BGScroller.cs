using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    private Vector2 startPosition;
    private bool switchedBackgrounds;

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    void Start()
    {
        startPosition = new Vector2(0, -56);
        transform.position = startPosition;
        switchedBackgrounds = false;
    }

    void Update()
    {
        transform.position = transform.position + new Vector3(0, scrollSpeed, 0);

        GameController instance = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
        if (instance.secondRound == true && switchedBackgrounds == false)
        {
            switchedBackgrounds = true;
            startPosition = new Vector2(0, -46);
            transform.position = startPosition;
            spriteRenderer.sprite = newSprite;
        }
    }
}
