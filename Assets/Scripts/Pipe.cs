using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public KeyCode enterKeyCode = KeyCode.DownArrow;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(enterKeyCode))
            {
                StartCoroutine(Enter(other.transform));
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        Vector3 enteredPostion = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;
        yield return Move(player, enteredPostion, enteredScale);
        yield return new WaitForSeconds(1f);
        bool underground = connection.position.y < transform.position.y;
        Camera.main.GetComponent<SideScrolling>().SetUnderground(underground);
        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator Move(Transform player, Vector3 targetPosition, Vector3 targetScale)
    {
        float elapsed = 0f;
        float duration = 1f;
        Vector3 initialPosition = player.position;
        Vector3 initialScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            player.position = Vector3.Lerp(initialPosition, targetPosition, t);
            player.localScale = Vector3.Lerp(initialScale, targetScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.position = targetPosition;
        player.localScale = targetScale;
    }
}
