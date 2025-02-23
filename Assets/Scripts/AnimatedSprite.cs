using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float framesPerSecond = 1f / 6f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framesPerSecond, framesPerSecond);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        currentFrame++;
        if (currentFrame >= sprites.Length)
        {
            currentFrame = 0;
        }
        if (currentFrame >= 0 && currentFrame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[currentFrame];
        }
    }
}
