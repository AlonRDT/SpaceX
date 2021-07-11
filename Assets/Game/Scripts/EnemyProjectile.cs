using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float m_Speed;

    private void Awake()
    {
        m_Speed = 5f;
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, -m_Speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
