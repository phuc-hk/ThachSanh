using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpriteOrder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpriteRenderer itemSpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer playerSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();

            if (collision.transform.position.y < transform.position.y)
            {
                itemSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder - 1;
            }
            else
            {
                itemSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder + 1;
            }
        }
    }
}
