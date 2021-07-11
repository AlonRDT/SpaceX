using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic_Small : EnemyLogic
{
    private float m_Speed;
    protected override void move()
    {
        transform.Translate(new Vector3(m_Speed * Time.deltaTime, 0, 0));

        if(transform.position.x > 11.6f)
        {
            transform.position = new Vector3(-11.6f, transform.position.y, transform.position.z);
        }
    }

    // Start is called before the first frame update
    protected new void Awake()
    {
        base.Awake();
        m_Speed = 3;
    }
}
