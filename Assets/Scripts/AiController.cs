using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    public GameObject m_Opponent = null;

    [Tooltip("This should be the weapon that this AI will be using to fight")]
     Weapon m_Weapon = null;


    [Tooltip("This should be the spear that this AI will be using to fight")]
    [SerializeField] GameObject m_Spear = null;

    [Tooltip("This should be the sword that this AI will be using to fight")]
    [SerializeField] GameObject m_Sword = null;

    [Tooltip("This should be the bow that this AI will be using to fight")]
    [SerializeField] GameObject m_Bow = null;



    Weapon.Type m_State;


    [SerializeField] float m_Distance = 50;


    FighterScript m_FighterScript = null;

    NavMeshAgent m_NavAgent = null;
    // Start is called before the first frame update
    void Start()
    {
  
        m_NavAgent = GetComponent<NavMeshAgent>();


       

        m_NavAgent.updateRotation = false;


        m_FighterScript = gameObject.GetComponent<FighterScript>();
    }


    public void SetWeapon(Weapon.Type type)
	{
        switch(type)
		{
            case Weapon.Type.Sword:
				{
                    m_Sword.SetActive(true);
                    m_Weapon = m_Sword.GetComponent<SwordController>();

				}
                break;
            case Weapon.Type.Pike:
                {
                    m_Spear.SetActive(true);
                    m_Weapon = m_Spear.GetComponent<SpearController>();
                }
                break;
            case Weapon.Type.Bow:
                {
                    m_Bow.SetActive(true);
                    m_Weapon = m_Bow.GetComponent<BowController>();
                }
                break;

        }

        m_State = type;
	}


    // Update is called once per frame
    void FixedUpdate()
    {


        transform.forward = m_Opponent.transform.position - transform.position;

        switch (m_State)
		{
            case Weapon.Type.Sword:
                Sword();
                break;
            case Weapon.Type.Pike:
                Pike();
                break;
            case Weapon.Type.Bow:
                Bow();
                break;
        }
    }



    void Sword()
	{
        var navAgent = GetComponent<NavMeshAgent>();

        if(navAgent && m_Opponent)
		{
            var oppToThis = transform.position - m_Opponent.transform.position;
            if(oppToThis.sqrMagnitude < 20)
			{
                m_Weapon.Attack();
               
			}
			else 
            {
                navAgent.SetDestination(m_Opponent.transform.position + (m_Opponent.transform.forward));
            }
		}
	}


    void Pike()
	{
        

        if (m_NavAgent && m_Opponent)
        {

            


            var toOpponent = m_Opponent.transform.position - transform.position;

            var disSq = toOpponent.sqrMagnitude;

            if (disSq < 66.0f)
            {
                var opp = m_Opponent.GetComponent<AiController>();

                if(disSq < 40.0f && opp.GetWeaponType() == Weapon.Type.Sword)
				{
                   

                    m_NavAgent.isStopped = false;
                    m_NavAgent.SetDestination(transform.position - (toOpponent.normalized));
                }
                else if(disSq < 35.0f)
				{
                    m_NavAgent.isStopped = true;
                }

               


                if(m_Weapon)
				{
                    m_Weapon.Attack();
				}
				else
				{
                    Debug.Log("Weapon is null");
				}

                
            }
            else
            {
                m_NavAgent.isStopped = false;
                m_NavAgent.SetDestination(m_Opponent.transform.position + (m_Opponent.transform.forward / 2));
            }
        }
    }


    void Bow()
	{

        if (m_NavAgent && m_Opponent)
        {




            var toOpponent = m_Opponent.transform.position - transform.position;

            var disSq = toOpponent.sqrMagnitude;

            if (disSq < 200.0f)
            {


                if (disSq < 150.0f)
                {


                    m_NavAgent.isStopped = false;
                    m_NavAgent.SetDestination(transform.position - (toOpponent.normalized));
                }
                else
                {
                    m_NavAgent.isStopped = true;
                }

            }
            else
            {
                m_NavAgent.isStopped = false;
                m_NavAgent.SetDestination(m_Opponent.transform.position + (m_Opponent.transform.forward / 2));
            }



            if (m_Weapon)
            {
                m_Weapon.Attack();
            }
            else
            {
                Debug.Log("Weapon is null");
            }



        }
    }




    Weapon.Type GetWeaponType()
	{
        return m_State;
	}
}
