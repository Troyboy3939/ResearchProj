using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : Weapon
{
    [Tooltip("The point at which raycast should be done on this sword, to check if it hit an enemy")]
    [SerializeField] GameObject m_HitPoint = null;

    //Whether the an enemy has been hit
    GameObject m_HitEnemy = null;

    //Whether the sword has been swung
    bool m_SwordSwung = false;

    //Whether the sword is about to be swung
    bool m_SwingingSword = false;

    //Length of the raycast when swinging sword
    float m_SwingDistance = 1.0f;

    //[Tooltip("Collider of where the sword will be swung")]
    //[SerializeField] BoxCollider m_HitArea = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Type = Type.Sword;
    }

    // Update is called once per frame
    void Update()
    {
        //if the sword has been swung and there is an enemy
        if (m_SwordSwung && m_HitEnemy)
        {


            m_SwordSwung = false;
        }




        m_HitEnemy = null;
    }

    public  void PerformRayCast() 
    {

    }


	private void FixedUpdate()
	{
		if(m_SwingingSword && m_HitPoint != null)
		{
            m_SwordSwung = true;
            RaycastHit hit = new RaycastHit();

            

            Physics.Raycast(m_HitPoint.transform.position,m_HitPoint.transform.forward * m_SwingDistance,out hit);

            if(hit.transform.tag == "AI")
			{
                m_HitEnemy = hit.transform.gameObject;
                return;
			}

            m_HitEnemy = null;
		}
	}

	public void SetHitEnemy(GameObject enemy)
	{
        m_HitEnemy = enemy;
	}


}
