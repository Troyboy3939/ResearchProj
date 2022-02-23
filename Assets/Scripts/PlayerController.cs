using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("This should always be the fighter script that is attached to the fighter")]
    [SerializeField] FighterScript m_FighterScript = null;

     Weapon m_Weapon = null;
    [SerializeField] Weapon m_Bow = null;
    [SerializeField] Weapon m_Sword = null;
    [SerializeField] Weapon m_Spear = null;
    [SerializeField] GameObject m_Shield = null;





    // Start is called before the first frame update
    void Awake()
    {
        var weap = PlayerPrefs.GetInt("Player");

        if(weap == 3)
		{
            weap = Random.Range(0,3);
		}



        switch ((Weapon.Type)weap)
		{
            case Weapon.Type.Bow:
				{
                    m_Weapon = m_Bow;
				}
                break;
            case Weapon.Type.Sword:
                {
                    m_Weapon = m_Sword;
                    m_Shield.SetActive(true);
                }
                break;
            case Weapon.Type.Pike:
                {
                    m_Weapon = m_Spear;
                }
                break;
        }

        m_Weapon.gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        var attack = Input.GetKeyUp(KeyCode.Space);

        if(attack)
		{
            if (m_Weapon)
			{

                Debug.Log("GiMmE Ya Loighta!");
                m_Weapon.Attack();
			}

		}

       
    }



    public Weapon.Type GetWeaponType()
	{
        return m_Weapon.GetWeaponType();
	}
}
