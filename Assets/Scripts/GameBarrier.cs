using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.gameObject.SetActive(false);
            GameManager.Instance.ResetLevel(3f);
        }
        else {
            Destroy(other.gameObject);
        }
    }
}
