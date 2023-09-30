using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{

private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer component not found on this GameObject.");
        }
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            // Set the sorting order based on the Y position
            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -10);
        }
    }
}
