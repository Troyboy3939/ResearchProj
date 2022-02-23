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
            var movement = new Vector3(horizontal,0.0f,vertical);

            //normalise it 
            movement.Normalize();

            
            movement *= m_Speed * Time.deltaTime;

           

            var cc = GetComponent<CharacterController>();

            cc.Move(movement);



            var camRot = Camera.main.transform.rotation;

            var x = camRot.x;

            var forward = Vector3.forward;




     


            transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.0f, 0.0f, 39)));

            var euler = transform.rotation;

            euler.x = 0.0f;
            euler.z = 0.0f;

            transform.rotation = euler;

            //transform.Rotate(axis: Vector3.up, angle: Mathf.Acos(Vector3.Dot(toMouse.normalized, transform.forward)));

		}
    }
}
