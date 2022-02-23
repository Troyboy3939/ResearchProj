using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : Weapon
{
    Animator m_Animator = null;

    FighterScript m_Controller = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Type = Type.Sword;

        m_Animator = GetComponent<Animator>();

        m_Controller = GetComponent<FighterScript>();


        if(m_Controller)
		{
            var skill = m_Controller.GetSwordSkill();


            m_Damage = Mathf.Lerp(m_Damage, m_Damage * 1.5f, skill);
		}
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
                var hits = Physics.SphereCastAll(m_HitPoint.transform.position, 0.5f, m_HitPoint.transform.forward, 1.0f);

                //for ev 
                foreach (var rayHit in hits)
                {
                    if (rayHit.transform.tag == "Shield")
                    {
                        var hitObject = rayHit.transform.gameObject;

                        //if it is not you
                        if (hitObject != gameObject)
                        {
                            var shieldController = hitObject.GetComponent<ShieldController>();


                            shieldController.SetHealth(shieldController.GetHealth() - (100.0f / 3.0f));

                            return;
                        }
                    }
                    else if (rayHit.transform.tag == "Player" || rayHit.transform.tag == "AI")
                    {
                        var hitObject = rayHit.transform.gameObject;

                        //if it is not you
                        if (hitObject != gameObject)
                        {
                            var shieldController = hitObject.GetComponentInChildren<ShieldController>();

                            if(shieldController)
							{
                                if(shieldController.GetHealth() > 0.0f)
								{
                                    shieldController.SetHealth(shieldController.GetHealth() - (100.0f / 3.0f));

                                    return;
                                }
							}


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
