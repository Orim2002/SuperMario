using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    private bool shelled;
    private bool shellMoving;
    public float shellSpeed = 12f;

    private void OnCollisionEnter2D(Collision2D other) {
        if (!shelled && other.gameObject.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.starpower) {
                Hit();
            } else if (other.transform.DotTest(transform, Vector2.down)) {
                EnterShell();
            } else {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (shelled && other.gameObject.CompareTag("Player")) {
            if (!shellMoving) 
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            } else {
                Player player = other.GetComponent<Player>();
                if (player.starpower) {
                    Hit();
                } else {
                    player.Hit();
                }
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }

    private void EnterShell() {
        shelled = true;
        GetComponent<EntityMovement>().enabled = false; 
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction) {
        shellMoving = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<EntityMovement>().direction = direction.normalized;
        GetComponent<EntityMovement>().speed = shellSpeed;
        GetComponent<EntityMovement>().enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    void OnBecameInvisible()
    {
        if (shellMoving)
        {
            Destroy(gameObject);
        }
    }
}
