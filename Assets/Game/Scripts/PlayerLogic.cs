using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    private Rigidbody2D m_RigidBody;
    private float m_FireDelay;
    private float m_AccumalatedFireDelay;
    private bool m_IsDead;
    [SerializeField] private float m_Accelaration;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_RotationSpeed;
    [SerializeField] private float m_YLocationBounds;
    [SerializeField] private float m_XLocationBounds;
    [SerializeField] private FloatingJoystick m_JoystickMove;
    [SerializeField] private FixedJoystick m_JoystickRotate;
    [SerializeField] private GameObject m_ProjectilePrefab;
    [SerializeField] private GameObject m_ProjectileSpawnLocation;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_FireSound;
    [SerializeField] private AudioClip m_ExplosionSound;
    [SerializeField] private SpriteRenderer m_ShipSprite;
    [SerializeField] private ParticleSystem m_ExplosionParticleSystem;

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Accelaration = 5f;
        m_MaxSpeed = 10f;
        m_RotationSpeed = 0.6f;
        m_XLocationBounds = 11.6f;
        m_YLocationBounds = 3.4f;
        m_FireDelay = 0.5f;
        m_AccumalatedFireDelay = 0.5f;
        m_IsDead = false;
    }

    private void Update()
    {
        m_AccumalatedFireDelay += Time.deltaTime;
        HandleRotation();
        HandleSpeed();
        HandleLocation();
    }

    private void HandleLocation()
    {
        float xLoc = transform.localPosition.x;
        float yLoc = Mathf.Clamp(transform.localPosition.y, -m_YLocationBounds, m_YLocationBounds);
        if(xLoc > m_XLocationBounds)
        {
            xLoc = -m_XLocationBounds;
        }
        else if(xLoc < -m_XLocationBounds)
        {
            xLoc = m_XLocationBounds;
        }

        transform.localPosition = new Vector2(xLoc, yLoc);
    }

    private void HandleSpeed()
    {
        float xSpeed = Mathf.Clamp(m_JoystickMove.Horizontal * Time.deltaTime * m_Accelaration + m_RigidBody.velocity.x, -m_MaxSpeed, m_MaxSpeed);
        float ySpeed = Mathf.Clamp(m_JoystickMove.Vertical * Time.deltaTime * m_Accelaration + m_RigidBody.velocity.y, -m_MaxSpeed, m_MaxSpeed);

        m_RigidBody.velocity = new Vector2(xSpeed, ySpeed);
    }

    private void HandleRotation()
    {
        transform.Rotate(new Vector3(0, 0, -m_JoystickRotate.Horizontal * m_RotationSpeed));
    }

    public void Fire()
    {
        if(m_AccumalatedFireDelay > m_FireDelay)
        {
            m_AccumalatedFireDelay = 0;
            Instantiate(m_ProjectilePrefab, m_ProjectileSpawnLocation.transform.position, transform.rotation);
            m_AudioSource.PlayOneShot(m_FireSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && m_IsDead == false)
        {
            m_AudioSource.PlayOneShot(m_ExplosionSound);
            m_ExplosionParticleSystem.Play();
            m_ShipSprite.enabled = false;
            m_IsDead = true;
            gameObject.tag = "Untagged";
            StartCoroutine(RestartLevelTimer());
        }
    }

    IEnumerator RestartLevelTimer()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
