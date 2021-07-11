using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyLogic : MonoBehaviour
{
    [SerializeField] private int m_HealthPoints;
    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_FireSound;
    [SerializeField] private GameObject m_ProjectilePrefab;
    [SerializeField] private bool m_IsFiring;
    private float m_AccumlatedFireDelay;
    private float m_FireDelay;

    private void Update()
    {
        m_AccumlatedFireDelay += Time.deltaTime;
        move();
        if(m_IsFiring && m_AccumlatedFireDelay > m_FireDelay)
        {
            Fire();
        }
    }

    protected void Awake()
    {
        m_AudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        handleFireTimers();
    }

    private void handleFireTimers()
    {
        m_AccumlatedFireDelay = 0;
        m_FireDelay = UnityEngine.Random.Range(2, 5f);
    }

    protected abstract void move();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_HealthPoints--;

            if (m_HealthPoints == 0)
            {
                GameObject.Find("SpawnManager").GetComponent<SpawnManager>().NotifyOnEnemyDeath();
                Destroy(gameObject);
            }
        }
    }

    protected void Fire()
    {
        handleFireTimers();
        Instantiate(m_ProjectilePrefab, transform.position + new Vector3(0, -1, 0), transform.rotation);
        m_AudioSource.PlayOneShot(m_FireSound);
    }
}
