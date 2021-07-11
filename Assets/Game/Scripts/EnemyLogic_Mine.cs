using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic_Mine : EnemyLogic
{
    private float m_SpeedMove;
    private float m_SpeedRotate;
    private GameObject m_Player;
    private Rigidbody2D m_RigidBody;
    
    protected new void Awake()
    {
        base.Awake();
        m_SpeedMove = 0.1f;
        m_SpeedRotate = 30;
        m_Player = GameObject.Find("Player");
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    protected override void move()
    {
        transform.Rotate(new Vector3(0, 0, m_SpeedRotate * Time.deltaTime));
        Vector3 direction = m_Player.transform.position - transform.position;
        m_RigidBody.AddForce(direction * m_SpeedMove * Time.deltaTime, ForceMode2D.Impulse);
    }
}
