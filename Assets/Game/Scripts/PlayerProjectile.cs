using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float m_Speed = 10f;
    private Rigidbody2D m_RigidBody;


    private void Awake()
    {
        m_Speed = 10f;
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_RigidBody.AddForce(transform.up * m_Speed, ForceMode2D.Impulse);
        StartCoroutine(SelfDestruct());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}