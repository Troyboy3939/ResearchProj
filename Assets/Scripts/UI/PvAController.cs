using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PvAController : MonoBehaviour
{

    [SerializeField] TMP_Dropdown m_A1;

    [SerializeField] TMP_Dropdown m_Player;

    [SerializeField] TMP_Dropdown m_Map;
    [SerializeField] TMP_Dropdown m_NoTimes;

    public void GoBwah()
    {

        
        PlayerPrefs.SetInt("A1", m_A1.value);
        PlayerPrefs.SetInt("Player", m_Player.value);
        PlayerPrefs.SetInt("MapSelection", m_Map.value);
        PlayerPrefs.SetInt("PlayerMatch", 1);

        PlayerPrefs.SetInt("NoTimes", m_NoTimes.value);


        if (m_Map.value == 3)
        {
            PlayerPrefs.SetInt("Map", Random.Range(0, 3));
        }
        else
        {
            PlayerPrefs.SetInt("Map", m_Map.value);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);


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

/*
 * 
 */