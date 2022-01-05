using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlap : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Untagged")
        {
            //Debug.Log("hello");
            //target.gameObject.SetActive(false);
        }
    }
}