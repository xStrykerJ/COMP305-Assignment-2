using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CPlatformManager.Instance.StartCoroutine("SpawnPlatform",
                new Vector2(transform.position.x, transform.position.y));
            Invoke("CollapsePlatform", 1.5f);
            Destroy(gameObject, 3f);
        }
    }

    void CollapsePlatform()
    {
        rb.isKinematic = false;
    }
}
