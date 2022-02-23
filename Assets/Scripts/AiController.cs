using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    public GameObject m_Opponent = null;

    [Tooltip("This should be the weapon that this AI will be using to fight")]
     Weapon m_Weapon = null;


    [Tooltip("This should be the spear that this AI will be using to fight")]
    [SerializeField] GameObject m_Spear = null;

    [Tooltip("This should be the sword that this AI will be using to fight")]
    [SerializeField] GameObject m_Sword = null;

    [Tooltip("This should be the shield that this AI will be using to fight")]
    [SerializeField] GameObject m_Shield = null;

    [Tooltip("This should be the bow that this AI will be using to fight")]
    [SerializeField] GameObject m_Bow = null;

    Vector3 m_Destination = new Vector3();

    float m_Speed = 0.0f;

    Weapon.Type m_State;


    [SerializeField] float m_Distance = 50;

    bool m_GettingKnockedBack = false;
    FighterScript m_FighterScript = null;

    NavMeshAgent m_NavAgent = null;

    float m_SwordSkill = 0.0f;
    float m_BowSkill = 0.0f;
    float m_SpearSkill = 0.0f;
    Weapon.Type m_PreferedWeapon = 0;

    public Color m_Colour = Color.red;


    bool m_SetUpSpeed = false;

    void Awake()
    {

        //I realised that skills should definitely be a fighterscript thing and not an AI thing
        //Cannot be bothered adjusting this code, however these values don't actually change so this'll work just fine
        //Skills need to be a fighter thing for the player

        m_SwordSkill = Random.Range(0.05f, 1.5f);
        m_BowSkill = Random.Range(0.05f, 1.5f);
        m_SpearSkill = Random.Range(0.05f, 1.5f);

        m_PreferedWeapon = (Weapon.Type)Random.Range(0, (int)(Weapon.Type.Bow + 1));

        m_FighterScript = gameObject.GetComponent<FighterScript>();


        m_FighterScript.SetBowSkill(m_BowSkill);
        m_FighterScript.SetSwordSkill(m_SwordSkill);
        m_FighterScript.SetBowSkill(m_SpearSkill);
        m_FighterScript.SetPrefered(m_PreferedWeapon);

        m_NavAgent = GetComponent<NavMeshAgent>();
        Random.InitState((int)System.DateTime.Now.Ticks);


       
       

        m_NavAgent.updateRotation = false;



       


    }

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

    public void PickWeapon(int technique)
	{
        

   


        switch (technique)
		{
            case 0:
                Highest();
                break;
            case 1:
                RandomHighest();
                break;
            case 2:
                WheelRandom();
                break;
            case 3:
                FSM();
                break;
            case 4:
                TotalRandom();
                break;
        }
	}

    void Highest()
	{




        var spear = 0.0f;
        var sword = 0.0f;
        var bow = 0.0f;


        var map = PlayerPrefs.GetInt("Map");


        //DISCLAIMER
        // even though technically I haven't made world states these act exactly the same 
        // and theres no real point in going full on with this.
        // With the map on a game, there would literally be a world called "OpenMap" that is set to true
        // that would do eventually do the exact same thing below here.
        // In addition there would literally be a world state "PreferBow" set to true or something,
        // as for the stats there would be a world state "HighStat", "LowStat" and that would
        // act the same. 


        switch (map)
        {
            case 0:
                {
                    bow = 50.0f;
                    spear = 25.0f;
                }
                break;
            case 1:
                {
                    spear = 50.0f;
                    sword = 25.0f;
                }
                break;
            case 2:
                {
                    sword = 50.0f;
                    spear = 25.0f;
                }
                break;
        }

        switch (m_PreferedWeapon)
		{
            case Weapon.Type.Bow:
                bow += 50.0f;
                break;
            case Weapon.Type.Sword:
                sword += 50.0f;
                break;
            case Weapon.Type.Pike:
                spear += 50.0f;
                break;
        }

        
        if (m_SwordSkill >= 1.0f)
        {
            sword += 50.0f;
        }
        if (m_SwordSkill >= 0.7f)
        {
            sword += 25.0f;
        }
        else if (m_SwordSkill >= 0.3f)
        {
            sword += 15.0f;
        }



        if (m_SpearSkill >= 1.0f)
        {
            spear += 50.0f;
        }
        if (m_SpearSkill >= 0.7f)
        {
            spear += 25.0f;
        }
        else if (m_SpearSkill >= 0.3f)
        {
            spear += 15.0f;
        }

        if (m_BowSkill >= 1.0f)
        {
            bow += 50.0f;
        }
        if (m_BowSkill >= 0.7f)
        {
            bow += 25.0f;
        }
        else if (m_BowSkill >= 0.3f)
        {
            bow += 15.0f;
        }




        //pick the highest overall
        if (bow > spear && bow > sword)
		{
            SetWeapon(Weapon.Type.Bow);
		}
        else if(spear > bow && spear > sword)
		{
            SetWeapon(Weapon.Type.Pike);

		}
        else if (sword > bow && sword > spear)
        {
            SetWeapon(Weapon.Type.Sword);
        }


        //if there are ties at first place just pick one
        if(bow > spear && bow == sword)
		{
            SetWeapon(Weapon.Type.Bow);
            
		}

        if (spear > sword && bow == spear)
        {
            SetWeapon(Weapon.Type.Pike);

        }

        if (spear > bow && sword == spear)
        {
            SetWeapon(Weapon.Type.Sword);

        }



    }

	void RandomHighest()
    {



        var spear = Random.Range(0.0f, 50.0f);
        var sword = Random.Range(0.0f, 50.0f);
        var bow = Random.Range(0.0f, 50.0f);


        var map = PlayerPrefs.GetInt("Map");


        //DISCLAIMER
        // even though technically I haven't made world states these act exactly the same 
        // and theres no real point in going full on with this.
        // With the map on a game, there would literally be a world called "OpenMap" that is set to true
        // that would do eventually do the exact same thing below here.
        // In addition there would literally be a world state "PreferBow" set to true or something,
        // as for the stats there would be a world state "HighStat", "LowStat" and that would
        // act the same. 


        switch (map)
        {
            case 0:
                {
                    bow = 50.0f;
                    spear = 25.0f;
                }
                break;
            case 1:
                {
                    spear = 50.0f;
                    sword = 25.0f;
                }
                break;
            case 2:
                {
                    sword = 50.0f;
                    spear = 25.0f;
                }
                break;
        }


        switch (m_PreferedWeapon)
        {
            case Weapon.Type.Bow:
                bow += 50.0f;
                break;
            case Weapon.Type.Sword:
                sword += 50.0f;
                break;
            case Weapon.Type.Pike:
                spear += 50.0f;
                break;
        }


        if (m_SwordSkill >= 1.0f)
        {
            sword += 50.0f;
        }
        if (m_SwordSkill >= 0.7f)
        {
            sword += 25.0f;
        }
        else if (m_SwordSkill >= 0.3f)
        {
            sword += 15.0f;
        }



        if (m_SpearSkill >= 1.0f)
        {
            spear += 50.0f;
        }
        if (m_SpearSkill >= 0.7f)
        {
            spear += 25.0f;
        }
        else if (m_SpearSkill >= 0.3f)
        {
            spear += 15.0f;
        }

        if (m_BowSkill >= 1.0f)
        {
            bow += 50.0f;
        }
        if (m_BowSkill >= 0.7f)
        {
            bow += 25.0f;
        }
        else if (m_BowSkill >= 0.3f)
        {
            bow += 15.0f;
        }




        //pick the highest overall
        if (bow > spear && bow > sword)
        {
            SetWeapon(Weapon.Type.Bow);
        }
        else if (spear > bow && spear > sword)
        {
            SetWeapon(Weapon.Type.Pike);

        }
        else if (sword > bow && sword > spear)
        {
            SetWeapon(Weapon.Type.Sword);
        }


        //if there are ties at first place just pick one
        if (bow > spear && bow == sword)
        {
            SetWeapon(Weapon.Type.Bow);

        }

        if (spear > sword && bow == spear)
        {
            SetWeapon(Weapon.Type.Pike);

        }

        if (spear > bow && sword == spear)
        {
            SetWeapon(Weapon.Type.Sword);

        }
    }

    void WheelRandom()
	{
    

        var spear = 0.0f;
        var sword = 0.0f;
        var bow = 0.0f;


        var map = PlayerPrefs.GetInt("Map");


        //DISCLAIMER
        // even though technically I haven't made world states these act exactly the same 
        // and theres no real point in going full on with this.
        // With the map on a game, there would literally be a world called "OpenMap" that is set to true
        // that would do eventually do the exact same thing below here.
        // In addition there would literally be a world state "PreferBow" set to true or something,
        // as for the stats there would be a world state "HighStat", "LowStat" and that would
        // act the same. 

        switch (map)
        {
            case 0:
                {
                    bow = 50.0f;
                    spear = 25.0f;
                }
                break;
            case 1:
                {
                    spear = 50.0f;
                    sword = 25.0f; 
                }
                break;
            case 2:
                {
                    sword = 50.0f;
                    spear = 25.0f;
                }
                break;
        }


        switch (m_PreferedWeapon)
        {
            case Weapon.Type.Bow:
                bow += 50.0f;
                break;
            case Weapon.Type.Sword:
                sword += 50.0f;
                break;
            case Weapon.Type.Pike:
                spear += 50.0f;
                break;
        }



        if (m_SwordSkill >= 1.0f)
        {
            sword += 50.0f;
        }
        if (m_SwordSkill >= 0.7f)
        {
            sword += 25.0f;
        }
        else if (m_SwordSkill >= 0.3f)
        {
            sword += 15.0f;
        }

        

        if (m_SpearSkill >= 1.0f)
        {
            spear += 50.0f;
        }
        if (m_SpearSkill >= 0.7f)
        {
            spear += 25.0f;
        }
        else if (m_SpearSkill >= 0.3f)
        {
            spear += 15.0f;
        }

        if (m_BowSkill >= 1.0f)
        {
            bow += 50.0f;
        }
        if (m_BowSkill >= 0.7f)
        {
            bow += 25.0f;
        }
        else if (m_BowSkill >= 0.3f)
        {
            bow += 15.0f;
        }


  



        var total = bow + spear + sword;


        var choice = Random.Range(0.0f, total);


        choice -= bow;

        if(choice <= 0.0f)
		{
            SetWeapon(Weapon.Type.Bow);
            return;
		}
        choice -= spear;

        if(choice <= 0.0f)
		{
            SetWeapon(Weapon.Type.Pike);
            return;
		}

        choice -= sword;

        if (choice <= 0.0f)
        {
            SetWeapon(Weapon.Type.Sword);
            return;
        }


    }

    void FSM()
	{

        var map = PlayerPrefs.GetInt("Map");

        switch (map)
        {
            case 0:
                {
                    SetWeapon(Weapon.Type.Bow);
                }
                break;
            case 1:
                {
                    SetWeapon(Weapon.Type.Pike);
                }
                break;
            case 2:
                {
                    SetWeapon(Weapon.Type.Sword);
                }
                break;
        }
    }

    void TotalRandom()
	{

        var weapon = Random.Range(0, 3);


        switch (weapon)
        {
            case 0:
                {
                    SetWeapon(Weapon.Type.Bow);
                }
                break;
            case 1:
                {
                    SetWeapon(Weapon.Type.Sword);
                }
                break;
            default:
            case 2:
                {
                    SetWeapon(Weapon.Type.Pike);
                }
                break;
        }

    }

    public void SetWeapon(Weapon.Type type)
	{
        switch(type)
		{
            case Weapon.Type.Sword:
				{
                    m_Sword.SetActive(true);
                    m_Shield.SetActive(true);
                    m_Weapon = m_Sword.GetComponent<SwordController>();


                   


                }
                break;
            case Weapon.Type.Pike:
                {
                    m_Spear.SetActive(true);
                    m_Weapon = m_Spear.GetComponent<SpearController>();


                    

                }
                break;
            case Weapon.Type.Bow:
                {
                    m_Bow.SetActive(true);
                    m_Weapon = m_Bow.GetComponent<BowController>();

                   
                }
                break;

        }

        m_State = type;
	}


    void SetUpSpeed()
	{
        if (m_NavAgent)
        {
            m_SetUpSpeed = true;
            switch (m_Weapon.GetWeaponType())
            {
                case Weapon.Type.Sword:
                    {

                        m_NavAgent.speed = 3.5f;
                        m_Speed = 3.5f;

                        if (PlayerPrefs.GetInt("Map") == 1)
                        {
                            m_Speed += 0.5f;
                            m_NavAgent.speed = m_Speed;
                        }

                        if (PlayerPrefs.GetInt("Map") == 0)
                        {
                            m_Speed -= 1.0f;
                            m_NavAgent.speed = m_Speed;
                        }

                        //if this is prefered weapon and the map isn't open
                        if (m_PreferedWeapon == Weapon.Type.Sword && PlayerPrefs.GetInt("Map") != 0)
                        {
                            m_NavAgent.speed = 4.5f;
                            m_Speed = 4.5f;
                        }

                        
                    }
                    break;
                case Weapon.Type.Pike:
                    {


                        m_NavAgent.speed = 2.33334f;
                        m_Speed = 2.33334f;


                        if (PlayerPrefs.GetInt("Map") == 2)
                        {
                            m_Speed -= 1.0f;
                            m_NavAgent.speed = m_Speed;
                        }

                        if (PlayerPrefs.GetInt("Map") == 1)
                        {
                            m_Speed += 1.0f;
                            m_NavAgent.speed = m_Speed;
                        }


                        //if this is prefered weapon and the map isn't horseshoe
                        if (m_PreferedWeapon == Weapon.Type.Pike && PlayerPrefs.GetInt("Map") != 2)
                        {
                            m_NavAgent.speed = 3.33334f;
                            m_Speed = 3.33334f;
                        }



                    }
                    break;
                case Weapon.Type.Bow:
                    {
                        m_NavAgent.speed = 3.0f;
                        m_Speed = 3.0f;

                        if (PlayerPrefs.GetInt("Map") == 1)
						{
                            m_Speed -= 1.0f;
                            m_NavAgent.speed = m_Speed;
                        }

                        if(PlayerPrefs.GetInt("Map") == 0)
						{
                            m_Speed += 1.0f;
                            m_NavAgent.speed = m_Speed;
						}
                        //if this is prefered weapon and the map isn't narrow
                        if (m_PreferedWeapon == Weapon.Type.Bow)
                        {
                            m_Speed += 1.0f;
                            m_NavAgent.speed = m_Speed;
                        }

                    }
                    break;

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!m_SetUpSpeed)
		{
            SetUpSpeed();
		}

        transform.forward = m_Opponent.transform.position - transform.position;

        switch (m_State)
		{
            case Weapon.Type.Sword:
                Sword();
                break;
            case Weapon.Type.Pike:
                Pike();
                break;
            case Weapon.Type.Bow:
                Bow();
                break;
        }
    }


	private void OnDrawGizmos()
	{
		if(m_NavAgent)
		{

            
            Gizmos.color = m_Colour;
            Gizmos.DrawSphere(m_NavAgent.destination + new Vector3(0.0f,2.0f), 0.5f);
		}
	}


   public Weapon.Type GetWeaponType()
	{

       
        return m_State;
	}



	void Sword()
	{
        var navAgent = GetComponent<NavMeshAgent>();

        if(navAgent && m_Opponent)
		{
            
            var oppToThis = transform.position - m_Opponent.transform.position;


            if(oppToThis.sqrMagnitude < 20)
			{
                m_Weapon.Attack();
               
			}
        
            Pursue();
        
		}
	}


    void Pike()
	{
        

        if (m_NavAgent && m_Opponent)
        {

            

            var toOpponent = m_Opponent.transform.position - transform.position;

            var disSq = toOpponent.sqrMagnitude;

            if (disSq < 60.0f)
            {

                var weap = Weapon.Type.Sword;

                if(m_Opponent.tag == "AI")
				{

                    var opp = m_Opponent.GetComponent<AiController>();

                    weap = opp.GetWeaponType();

                }
				else
				{
                    var opp = m_Opponent.GetComponent<PlayerController>();

                    weap = opp.GetWeaponType();
                }

                if(disSq < 20.0f && weap == Weapon.Type.Sword)
                { 
                    Flee(toOpponent);
                }
                else if(disSq < 35.0f)
				{
                    //m_NavAgent.isStopped = true;
                }

               


                if(m_Weapon)
				{
                    m_Weapon.Attack();
				}
				else
				{
                    Debug.Log("Weapon is null");
				}

                
            }
            else
            {
                Pursue();
            }
        }
    }


    void Bow()
	{

        if (m_NavAgent && m_Opponent)
        {

           
            var toOpponent = m_Opponent.transform.position - transform.position;

            var disSq = toOpponent.sqrMagnitude;

            if (disSq < 200.0f)
            {


                if (disSq < 150.0f)
                {

                    var hits = Physics.RaycastAll(transform.position, (m_Opponent.transform.position - transform.position).normalized * 10.0f);

                    var pursue = true;

                    foreach(var hit in hits)
					{
                        var hitTag = hit.transform.tag;

                        if (hitTag == "Weapon" || hitTag == "AI" || hitTag == "Player")
						{
                            pursue = false;
                            break;
						}
					}



                    if(pursue)
					{
                        Pursue();
					}
					else
					{
                        Flee(toOpponent);

					}
                }
                else
                {
                    m_NavAgent.isStopped = true;
                }

            }
            else
            {
                Pursue();
            }

            

            if (m_Weapon)
            {
                m_Weapon.Attack();
            }
            else
            {
                Debug.Log("Weapon is null");
            }



        }
    }


    void Pursue()
    {
        m_NavAgent.isStopped = false;

        var nav = m_Opponent.GetComponent<NavMeshAgent>();


        if (nav && nav.velocity.sqrMagnitude > 0.0f)
        {
            m_Destination = m_Opponent.transform.position + (nav.velocity.normalized * nav.speed);
        }
        else
        {
            m_Destination = m_Opponent.transform.position + (transform.forward);
        }

        if (!m_GettingKnockedBack)
        {
            m_NavAgent.SetDestination(m_Destination);
        }
    }


    void Flee(Vector3 toOpponent)
	{
        m_NavAgent.isStopped = false;


        var safeDirection = toOpponent * -1.0f;

        if (!m_GettingKnockedBack)
        {


            Debug.DrawRay(transform.position, safeDirection.normalized * 10.0f, Color.green);
            var hits = Physics.RaycastAll(new Ray(transform.position, safeDirection.normalized), 10.0f);


            var performRaycast = false;

            foreach (var hit in hits)
            {
                var tag = hit.transform.tag;

                if (tag == "Wall")
                {
                    if (hit.distance < 60)
                    {
                        performRaycast = true;
                        break;
                    }
                }
            }


            if (performRaycast)
            {
                var numRotations = 16;

                for (int degrees = 112; degrees < (360 + 112); degrees += (360 + 112) / numRotations)
                {

                    var dir = Quaternion.AngleAxis(degrees, Vector3.up) * transform.forward;



                    var hits2 = Physics.RaycastAll(new Ray(transform.position, dir.normalized), 10.0f);

                    Debug.DrawRay(transform.position, dir * 10.0f);



                    var safe = true;

                    foreach (var hit in hits2)
                    {
                        var hitTag = hit.collider.tag;


                        //if it hits something with these tags
                        if (hitTag == "Player" || hitTag == "AI" || hitTag == "Shield" || hitTag == "Wall" || hitTag == "Weapon")
                        {
                            var hitGo = hit.collider.gameObject;

                            //and if its not you or any of you weapons
                            if (hitGo != gameObject && hitGo != m_Shield && hitGo != m_Sword && hitGo != m_Spear && hitGo != m_Bow)
                            {
                                if (hitGo == m_Opponent)
                                {
                                    safe = false;
                                    break;
                                }
                                else if (hit.distance < 3)
                                {
                                    safe = false;
                                    break;

                                }
                            }
                        }
                    }

                    if (safe)
                    {
                        safeDirection = dir;

                        break;
                    }

                }


            }


            //safeDirection /= numSafe;
            m_Destination = (transform.position + safeDirection.normalized * 5.0f);
            m_NavAgent.SetDestination(m_Destination);
        }
    }

   public IEnumerator Knockback(Vector3 attackerPos)
	{
        var toThis = transform.position - attackerPos;

        toThis.Normalize();

        m_NavAgent.destination = transform.position + (toThis * 20.0f);

        m_NavAgent.acceleration = 200;

        m_GettingKnockedBack = true;

        yield return new WaitForSeconds(0.25f);

        m_GettingKnockedBack = false;
        m_NavAgent.acceleration = 8;
        m_NavAgent.speed = m_Speed;

        m_NavAgent.destination = m_Destination;
	}

}

/*
 * 
 *  

public IEnumerator Knockback(Vector3 attackerPos)
	{
        var toThis = transform.position - attackerPos;

        toThis.Normalize();

        var rig = gameObject.AddComponent<Rigidbody>();

        rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        rig.AddForce(toThis.normalized * 10.0f, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);

        Destroy(rig);
	}



 * 
 */