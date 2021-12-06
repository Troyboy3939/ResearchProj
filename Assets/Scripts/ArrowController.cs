using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [Tooltip("How much damage this arrow does")]
    [SerializeField] float m_Damage = 25.0f;

    [SerializeField] float m_MaxSpeed = 20.0f;

    public GameObject m_Creator = null;

    // Start is called before the first frame update
    void Start()
    {
        var rigid = GetComponent<Rigidbody>();

        rigid.velocity = transform.forward * m_MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
        var colTag = collision.transform.tag;


        if (colTag == "AI" || colTag == "Player")
		{
            var colGO = collision.gameObject;
            if (m_Creator && m_Creator != colGO)
			{
                var fighterScript = colGO.GetComponent<FighterScript>();

                if(fighterScript)
				{
                    fighterScript.SetHealth(fighterScript.GetHealth() - m_Damage);
                    gameObject.SetActive(false);
				}
			}

		}
        else if(colTag == "Floor")
		{
           gameObject.SetActive(false);
		}
	}


}
