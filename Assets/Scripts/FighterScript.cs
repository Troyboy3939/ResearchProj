using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterScript : MonoBehaviour
{
    [Tooltip("How much health each fighter starts off with")]
    [SerializeField] float m_Health = 100.0f;

    [SerializeField] GameObject m_Shield = null;



    float m_SwordSkill = 0.0f;
    float m_BowSkill = 0.0f;
    float m_SpearSkill = 0.0f;
    Weapon.Type m_PreferedWeapon = 0;

    public float GetBowSkill()
    {
        return m_BowSkill;
    }

    public float GetSwordSkill()
    {
        return m_SwordSkill;
    }

    public float GetSpearSkill()
    {
        return m_SpearSkill;
    }


    public void SetBowSkill(float skill)
    {
        m_BowSkill = skill;
    }

    public void SetSwordSkill(float skill)
    {
        m_SwordSkill = skill;
    }

    public void SetSpearSkill(float skill)
    {
        m_SpearSkill = skill;
    }


    public Weapon.Type GetPreferred()
	{
        return m_PreferedWeapon;
	}


    public void SetPrefered(Weapon.Type type)
	{
        m_PreferedWeapon = type;
	}

    public void SetHealth(float health)
	{

        if (m_Shield)
        {
            if (!m_Shield.activeSelf)
            {

                m_Health = health;


                if (m_Health <= 0.0f)
                {
                    gameObject.SetActive(false);
                }

               // Debug.Log(m_Health);
            }
        }
	}

    public float GetHealth()
	{
        return m_Health;
	}



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
