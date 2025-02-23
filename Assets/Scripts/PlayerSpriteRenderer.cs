using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    public Sprite idleSprite;
    public Sprite jumpSprite;
    public Sprite slideSprite;
    public AnimatedSprite runSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void LateUpdate()
    {
        runSprite.enabled = playerMovement.running;
        if (playerMovement.jumping) {
            spriteRenderer.sprite = jumpSprite;
        } else if (playerMovement.sliding) {
            spriteRenderer.sprite = slideSprite;
        } else if (!playerMovement.running) {
            spriteRenderer.sprite = idleSprite;
        }
    }
}
