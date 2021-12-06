using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterScript : MonoBehaviour
{
    [Tooltip("How much health each fighter starts off with")]
    [SerializeField] float m_Health = 100.0f;



    public void SetHealth(float health)
	{
        m_Health = health;

        if(m_Health <= 0.0f)
		{
            gameObject.SetActive(false);
		}

        Debug.Log(m_Health);
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
