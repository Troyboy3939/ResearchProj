using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("This should always be the fighter script that is attached to the fighter")]
    [SerializeField] FighterScript m_FighterScript = null;

    [SerializeField] Weapon m_Weapon = null;


    // Start is called before the first frame update
    void Start()
    {
        
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
}
