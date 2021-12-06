using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : Weapon
{
    Animator m_Animator;



    bool a = true;


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();

        m_Type = Type.Pike;
    }

    public override void Attack()
    {
        Debug.Log("Spear Attack");


        if (m_Animator)
        {
            if (!m_Animator.GetNextAnimatorStateInfo(0).IsName("attackFront") && !m_Animator.IsInTransition(0))
            {
                m_Animator.SetTrigger("attackFront");
            }
			else
			{

			}
        }
    }


    // Update is called once per frame
    void Update()
    {  
        if(a)
		{
            m_Animator.SetTrigger("attackRight");
            a = false;
		}
    }


    public void Raycast()
	{
        if (m_HitPoint != null)
        {
            


            var hits = Physics.SphereCastAll(m_HitPoint.transform.position, 0.5f, m_HitPoint.transform.forward,1.0f);

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
