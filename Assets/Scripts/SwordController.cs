using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : Weapon
{
    Animator m_Animator = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Type = Type.Sword;

        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }


	public override void Attack()
	{
        Debug.Log("Sword Attack");
        if (m_Animator)
		{
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !m_Animator.IsInTransition(0))
            { 
                m_Animator.SetTrigger("attackRight");
            } 
		}
	}

	public  void PerformRayCast() 
    {
        if (m_HitPoint != null)
        {
            var collider = m_HitPoint.GetComponent<SphereCollider>();


            if (collider)
            {
                var hits = Physics.SphereCastAll(m_HitPoint.transform.position, 0.5f, m_HitPoint.transform.forward, 0.5f);

                //for ev 
                foreach (var rayHit in hits)
                {
                    if (rayHit.transform.tag == "Player" || rayHit.transform.tag == "AI")
                    {
                        var hitObject = rayHit.transform.gameObject;

                        //if it is not you
                        if (hitObject != gameObject)
                        {
                            var fighterScript = hitObject.GetComponent<FighterScript>();

                            
                            fighterScript.SetHealth(fighterScript.GetHealth() - (m_Damage));

                            return;
                        }


                    }
                }
            }

            
        }
    }


	private void FixedUpdate()
	{
	
	}


}
