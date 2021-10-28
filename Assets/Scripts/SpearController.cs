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

	}
      
}
