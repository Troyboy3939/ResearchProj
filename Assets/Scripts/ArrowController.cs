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
    void Awake()
    {
        var rigid = GetComponent<Rigidbody>();

        rigid.velocity = (transform.forward + new Vector3(0.0f,0.1f)).normalized* m_MaxSpeed;


       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
        var colTag = collision.transform.tag;

        if (colTag == "Shield")
        {
            var hitObject = collision.gameObject;

            //if it is not you
            if (hitObject != gameObject)
            {
                var shieldController = hitObject.GetComponent<ShieldController>();

                var dam = 1.0f;

                if(m_Creator.tag == "AI")
				{
                    var Ai = m_Creator.GetComponent<AiController>();

                    if(Ai)
					{
                        dam = Ai.GetBowSkill();
					}
				}
                shieldController.SetHealth(shieldController.GetHealth() - (100.0f * dam / 3.0f));
                gameObject.SetActive(false);

                return;
            }
        }
        else if (colTag == "AI" || colTag == "Player")
		{
            var colGO = collision.gameObject;
            if (m_Creator && m_Creator != colGO)
			{
                var fighterScript = colGO.GetComponent<FighterScript>();


                var dam = 1.0f;

                if (m_Creator)
                {
                    var Ai = m_Creator.GetComponentInParent<AiController>();

                    if (Ai)
                    {
                        dam = Ai.GetBowSkill();
                    }
                }

                if (fighterScript)
				{
                    fighterScript.SetHealth(fighterScript.GetHealth() - m_Damage * dam);
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
