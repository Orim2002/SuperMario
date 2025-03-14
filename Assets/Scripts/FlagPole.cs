using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;
    public int nextLevel = 1;
    public int nextStage = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(collision.transform));
        }
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        yield return null;
        yield return MoveTo(player, poleBottom.position);
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, castle.position);
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.LoadLevel(nextLevel, nextStage);
    }

    private IEnumerator MoveTo(Transform obj, Vector3 position)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        while (Vector3.Distance(obj.position, position) > 0.125f)
        {
            obj.position = Vector3.MoveTowards(obj.position, position, speed * Time.deltaTime);
            yield return null;
        }
        obj.position = position;
    }
}
