using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{

    [SerializeField] float m_Health = 100.0f;


    public float GetHealth()
	{
        return m_Health;
	}


    public void SetHealth(float health)
	{
        m_Health = health;
	}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Health <= 0.0f)
		{
            gameObject.SetActive(false);
		}
    }
}
