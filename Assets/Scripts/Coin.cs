using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerCore>(out var player))
        {
            var game = FindObjectOfType<GamePlay>();
            game.AddScore();
            Destroy(gameObject);
        }
    }
}
