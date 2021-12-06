using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Tooltip("How fast you want the player to be able to move")]
    [SerializeField] float m_Speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //if this is the player then
        if(tag == "Player")
		{
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            //create a vector
            Vector3 movement = new Vector3(horizontal,0.0f,vertical);

            //normalise it 
            movement.Normalize();

            
            movement *= m_Speed * Time.deltaTime;

            transform.position += movement;


            if (Input.GetAxisRaw("Strafe") < 0.9f)
            {
                if (movement.sqrMagnitude > 0.0f)
                {
                    transform.forward = movement.normalized;
                }
            }


		}
    }
}
