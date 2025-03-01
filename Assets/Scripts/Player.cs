using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallSpriteRenderer;
    public PlayerSpriteRenderer bigSpriteRenderer;

    private DeathAnimation deathAnimation;

    public bool big => bigSpriteRenderer.enabled;
    public bool small => smallSpriteRenderer.enabled;
    public bool dead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }
    public void Hit()
    {
        if (big) {
            Shrink();
        } else {
            Die();
        }
    }

    private void Shrink()
    {

    }

    private void Die()
    {
        bigSpriteRenderer.enabled = false;
        smallSpriteRenderer.enabled = false;

        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }
}
