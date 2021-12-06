using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : Weapon
{
    [Tooltip("This should always be the arrow prefab")]
    [SerializeField] GameObject m_Arrow;

    [Tooltip("How long they should wait before they can shoot again")]
    [SerializeField] float m_ReloadTime = 2.0f;

    float m_Timer = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Timer >= 0.0f)
		{
            m_Timer += Time.deltaTime;

            if(m_Timer >= m_ReloadTime)
			{
                m_Timer = -1.0f;
			}
		}
    }


	public override void Attack()
	{
        if (m_Timer == -1.0f)
        {

            if (m_HitPoint)
            {
                //in this case I am just using the hit point as the point to start the arrow,
                //otherwise this variable would literally be entirely useless
                var arrow = Instantiate(m_Arrow, m_HitPoint.transform);
                arrow.transform.position = m_HitPoint.transform.position;




                if (arrow)
                {
                    var controller = arrow.GetComponent<ArrowController>();
                    if (controller)
                    {
                        controller.m_Creator = gameObject;
                    }
                    arrow.transform.parent = null;

                    m_Timer = 0.0f;
                }
            }
        }
	}
}
