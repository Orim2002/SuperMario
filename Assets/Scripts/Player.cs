using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallSpriteRenderer;
    public PlayerSpriteRenderer bigSpriteRenderer;

    private PlayerSpriteRenderer activeSpriteRenderer;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

    public bool big => bigSpriteRenderer.enabled;
    public bool small => smallSpriteRenderer.enabled;
    public bool dead => deathAnimation.enabled;

    public bool starpower { get; private set; }

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeSpriteRenderer = smallSpriteRenderer;
    }
    public void Hit()
    {
        if (!dead && !starpower)
        {
            if (big){
                Shrink();
            } else {
                Die();
            }
        }
    }

    public void Grow()
    {
        if (small)
        {
            bigSpriteRenderer.enabled = true;
            smallSpriteRenderer.enabled = false;
            activeSpriteRenderer = bigSpriteRenderer;
            capsuleCollider.size = new Vector2(1f, 2f);
            capsuleCollider.offset = new Vector2(0f, 0.5f);
            StartCoroutine(ScaleAnimation());
        }
    }

    private void Shrink()
    {
        if (big)
        {
            bigSpriteRenderer.enabled = false;
            smallSpriteRenderer.enabled = true;
            activeSpriteRenderer = smallSpriteRenderer;
            capsuleCollider.size = new Vector2(1f, 1f);
            capsuleCollider.offset = new Vector2(0f, 0f);
            StartCoroutine(ScaleAnimation());
        }
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (Time.frameCount % 4 == 0)
            {
                smallSpriteRenderer.enabled = !smallSpriteRenderer.enabled;
                bigSpriteRenderer.enabled = !smallSpriteRenderer.enabled;
            }
            yield return null;
        }
        smallSpriteRenderer.enabled = false;
        bigSpriteRenderer.enabled = false;
        activeSpriteRenderer.enabled = true;
    }

    private void Die()
    {
        bigSpriteRenderer.enabled = false;
        smallSpriteRenderer.enabled = false;

        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }

    public void Starpower()
    {
        starpower = true;
    }

    private IEnumerator StarpowerAnimation()
    {
        float elapsed = 0f;
        float duration = 10f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (Time.frameCount % 4 == 0)
            {
                activeSpriteRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            yield return null;
        }
        activeSpriteRenderer.spriteRenderer.color = Color.white;
        starpower = false;
    }
}
