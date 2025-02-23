using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite squishedSprite;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (other.transform.DotTest(transform, Vector2.down)) {
                Squish();
            }
        }
    }

    private void Squish() {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false; 
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = squishedSprite;
        Destroy(gameObject, 0.5f);
    }
}
