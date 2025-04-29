using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetColliderScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlockedItem")
        {

            Debug.Log("It detects");
            Rigidbody2D rb = collision.attachedRigidbody;
            if (rb != null)
            {
                Vector2 pushDirection = (Vector2)(collision.transform.position - transform.position).normalized;
                rb.MovePosition(rb.position + pushDirection * 3f);
            }
            else
            {
                // No Rigidbody2D, fallback to direct position move
                collision.transform.position += (Vector3)(collision.transform.position - transform.position).normalized * 3f;
            }
        }
    }
}
