using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;

public class SpawnAI : MonoBehaviour
{
    [SerializeField] GameObject m_AIPrefab = null;
    [SerializeField] GameObject m_PlayerPrefab = null;
    [SerializeField] GameObject m_RedLocation = null;
    [SerializeField] GameObject m_BlueLocation = null;
    [SerializeField] Material m_BlueMaterial = null;
    [SerializeField] NavigationBaker m_NavBaker = null;

    GameObject m_Red;
    GameObject m_Blue;

    List<Match> m_Matches = new List<Match>();

    Match m_Current = null;

    [SerializeField] GameObject m_Narrow = null;
    [SerializeField] GameObject m_Horseshoe = null;

    [Tooltip("start position 1 for narrow map")]
    [SerializeField] GameObject m_N1 = null;

    [Tooltip("start position 2 for narrow map")]
    [SerializeField] GameObject m_N2 = null;

    [Tooltip("start position 1 for horseshoe map")]
    [SerializeField] GameObject m_H1 = null;

    [Tooltip("start position 2 for horseshoe map")]
    [SerializeField] GameObject m_H2 = null;


    Vector3 m_RedPos = new Vector3();
    Vector3 m_BluePos = new Vector3();

    int m_NoMatches = 0;


    float m_fTimer = 0.0f;


    public GameObject GetBlue()
	{
        return m_Blue;
	}

    public GameObject GetRed()
    {
        return m_Red;
    }

    public int GetMatchesSize()
	{
        return m_Matches.Count;
	}

    // Start is called before the first frame update
    void Start()
    {

        var number = PlayerPrefs.GetInt("NoTimes");

        switch(number)
		{
            case 0:
                m_NoMatches = 5;
                break;
            case 1:
                m_NoMatches = 10;
                break;
            case 2:
                m_NoMatches = 20;
                break;
            case 3:
                m_NoMatches = 30;
                break;
        }
    }


    public void Finish()
	{
        if(m_Current != null)
		{
            m_Current.Finish();
		}
	}

    // Update is called once per frame
    void Update()
    {
        if(m_Red && m_Blue)
		{
            if (!m_Red.activeSelf || !m_Blue.activeSelf)
            {
                if (m_Current != null)
                {
                    m_Current.Finish();
                }
            }
        }


       
            
        


        if (m_AIPrefab != null && m_RedLocation != null && m_BlueLocation != null)
        {
            if (m_Current != null && m_Current.GetFinished() || m_Current == null)
            {
   
                ResetMatch();
   
                if (m_Matches.Count < m_NoMatches)
                {
                    var selection = PlayerPrefs.GetInt("MapSelection");
                        
                    var map = 0;
   
                    if (selection == 3)
                    {
                        map = UnityEngine.Random.Range(0, 3);
                        PlayerPrefs.SetInt("Map", map);
                    }
                    else
                    {
                        map = PlayerPrefs.GetInt("Map");
                    }
   
                    SetUpMatch();

                    if (PlayerPrefs.GetInt("PlayerMatch") == 0)
                    {
                        m_Red = Instantiate(m_AIPrefab, m_RedPos, m_RedLocation.transform.rotation);
                        m_Blue = Instantiate(m_AIPrefab, m_BluePos, m_BlueLocation.transform.rotation);

                        m_Red.name = "Red";
                        m_Blue.name = "Blue";

                        m_Blue.GetComponent<MeshRenderer>().material = m_BlueMaterial;




                        var redTech = PlayerPrefs.GetInt("A1");
                        var blueTech = PlayerPrefs.GetInt("A2");

                        var redAI = m_Red.GetComponent<AiController>();
                        var blueAI = m_Blue.GetComponent<AiController>();
                        redAI.m_Colour = Color.blue;
                        redAI.PickWeapon(redTech);
                        blueAI.PickWeapon(blueTech);

                        redAI.m_Opponent = m_Blue;
                        blueAI.m_Opponent = m_Red;
                        m_Current = new Match(redAI, blueAI, redTech, blueTech, map);
                    }
					else if(PlayerPrefs.GetInt("PlayerMatch") == 1)
					{
                        m_Red = Instantiate(m_AIPrefab, m_RedPos, m_RedLocation.transform.rotation);
                        m_Blue = Instantiate(m_PlayerPrefab, m_BluePos, m_BlueLocation.transform.rotation);

                        m_Red.name = "Red";
                        m_Blue.name = "Player";

                        m_Blue.GetComponent<MeshRenderer>().material = m_BlueMaterial;




                        var redTech = PlayerPrefs.GetInt("A1");
                        var blueTech = 5;

                        var redAI = m_Red.GetComponent<AiController>();
                        var blueAI = m_Blue.GetComponent<PlayerController>();
                        //redAI.m_Colour = Color.blue;
                        redAI.PickWeapon(redTech);

                        redAI.m_Opponent = m_Blue;
                        m_Current = new Match(redAI, blueAI, redTech, blueTech, map);
                    }
                    
   
   
   
                    m_Matches.Add(m_Current);
   
                }
				else
				{
                    var file = Application.dataPath;
                    var pathArray = file.Split('/');
                    file = "";
                    for (int i = 0; i < pathArray.Length - 1; i++)
                    {
                        file += pathArray[i] + "/";
                    }
                    file += "data" + Time.time + ".txt";



                    var contents = "";

                    for(int i = 0; i < m_Matches.Count; i++)
					{
                        string round = "Match " + (i + 1) + ":";

                        round += Environment.NewLine;

                        round += "AI Match: " + m_Matches[i].GetAIMatch();
                        round += Environment.NewLine;
                        round += "Result: " + m_Matches[i].GetWinner();
                        round += Environment.NewLine;
                        round += "Map: " + m_Matches[i].GetMap();
                        round += Environment.NewLine;
                        round += "Blue Decision Making Technique: " + m_Matches[i].GetBlueTech();
                        round += Environment.NewLine;
                        round += "Blue Weapon: " + m_Matches[i].GetBlueWeapon();
                        round += Environment.NewLine;
                        round += "Red Weapon: " + m_Matches[i].GetRedWeapon();
                        round += Environment.NewLine;
                        round += "Red Decision Making Technique: " + m_Matches[i].GetRedTech();
                        round += Environment.NewLine;

                        round += "---------------------------------------------------------";
                        round += Environment.NewLine;

                        contents += round;
					}


                    System.IO.File.WriteAllText(file,contents);


                    UnityEngine.SceneManagement.SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                }
   
   
            }
        }
    }
    

	void ResetMatch()
	{
	   if(m_Current != null)
		{
            Debug.Log(m_Current.GetWinner());


            if (m_Narrow)
			{
                m_Narrow.SetActive(false);
			}

            if (m_Horseshoe)
            {
                m_Horseshoe.SetActive(false);
            }


            m_RedPos = new Vector3();
            m_BluePos = new Vector3();
        }
	}


    void SetUpMatch()
	{
        var map = PlayerPrefs.GetInt("Map");

        switch(map)
		{
            case 1:
                {
                    if (m_Narrow)
                    {
                        m_Narrow.SetActive(true);
                        m_RedPos = m_N1.transform.position;
                        m_BluePos = m_N2.transform.position;

                        if(m_NavBaker)
						{
                            m_NavBaker.Build();
						}

                       //  NavMeshBuilder.nav;
                       //  NavMeshBuilder.BuildNavMesh();
                        return;
                    }
                }
                break;
            case 2:
                {
                    if (m_Horseshoe)
                    {
                        m_Horseshoe.SetActive(true);
                        m_RedPos = m_H1.transform.position;
                        m_BluePos = m_H2.transform.position;

                        // NavMeshBuilder.ClearAllNavMeshes();
                        // NavMeshBuilder.BuildNavMesh();

                        if (m_NavBaker)
                        {
                            m_NavBaker.Build();
                        }
                        return;
                    }
                }
                break;
        }


        m_RedPos = m_RedLocation.transform.position;
        m_BluePos = m_BlueLocation.transform.position;
        if (m_NavBaker)
        {
            m_NavBaker.Build();
        }

    }

}
