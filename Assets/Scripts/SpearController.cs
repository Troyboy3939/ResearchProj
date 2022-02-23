using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : Weapon
{
    Animator m_Animator;



    bool a = true;

    FighterScript m_Controller;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();

        m_Type = Type.Pike;


        m_Controller = GetComponent<FighterScript>();

        if (m_Controller)
        {
            var skill = m_Controller.GetSpearSkill();


            m_Damage = Mathf.Lerp(m_Damage, m_Damage * 1.5f, skill);
        }


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
            Debug.DrawRay(m_HitPoint.transform.position, m_HitPoint.transform.forward,Color.red ,3.0f);


            var hits = Physics.SphereCastAll(m_HitPoint.transform.position, 0.75f, m_HitPoint.transform.forward,3.0f);

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


                        shieldController.SetHealth(shieldController.GetHealth() - (103.0f / 3.0f));


                        var Ai = hitObject.GetComponentInParent<AiController>();

                        if (Ai)
                        {
                            StartCoroutine(Ai.Knockback(transform.position));
                        }
                        return;
                    }
                }
                else if (rayHit.transform.tag == "Player" || rayHit.transform.tag == "AI")
                {

                    var hitObject = rayHit.transform.gameObject;

                    //if it is not you
                    if (hitObject != gameObject)
                    {
                        var Ai = hitObject.GetComponentInParent<AiController>();
                        var shieldController = hitObject.GetComponentInChildren<ShieldController>();

                        if (shieldController)
                        {
                            if (shieldController.GetHealth() > 0.0f)
                            {
                                shieldController.SetHealth(shieldController.GetHealth() - (100.0f / 3.0f));
                                if (Ai)
                                {
                                    StartCoroutine(Ai.Knockback(transform.position));
                                }
                                return;
                            }
                        }


                        var fighterScript = hitObject.GetComponent<FighterScript>();


                        fighterScript.SetHealth(fighterScript.GetHealth() - (m_Damage));



                        if (Ai)
                        {
                            StartCoroutine(Ai.Knockback(transform.position));
                        }
                        return;
                    }
                }
              }


        }
    }

    
      
}


