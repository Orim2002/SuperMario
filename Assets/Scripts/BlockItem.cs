using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockItem : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rb.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.enabled = true;
        float elapsed = 0f;
        float duration = 0.5f;
        Vector3 startPositon = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(startPositon, endPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPosition;
        rb.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;


    }


}
