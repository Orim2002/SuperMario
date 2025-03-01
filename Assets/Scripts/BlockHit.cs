using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public Sprite emptySprite;
    public int maxHits = -1;

    private bool animating;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (!animating && other.gameObject.CompareTag("Player")) 
        {
            if (other.transform.DotTest(transform, Vector2.up)) 
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        maxHits--;
        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptySprite;
        }
        StartCoroutine(Animate());
    }

    private IEnumerator Animate ()
    {
        animating = true;
        Vector3 restingPostion = transform.localPosition;
        Vector3 animatedPosition = restingPostion + Vector3.up * 0.5f;
        yield return Move(restingPostion, animatedPosition);
        yield return Move(animatedPosition, restingPostion);
        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = to;
    }
}
