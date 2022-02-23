using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvAController : MonoBehaviour
{
    [SerializeField] TMP_Dropdown m_A1;

    [SerializeField] TMP_Dropdown m_A2;

    [SerializeField] TMP_Dropdown m_Map;
    [SerializeField] TMP_Dropdown m_NoTimes;

    public void GoBwah()
	{
        PlayerPrefs.SetInt("A1",m_A1.value);
        PlayerPrefs.SetInt("A2",m_A2.value);
        PlayerPrefs.SetInt("MapSelection",m_Map.value);

        PlayerPrefs.SetInt("PlayerMatch", 0);

        PlayerPrefs.SetInt("NoTimes",m_NoTimes.value);


        if(m_Map.value == 3)
		{
            PlayerPrefs.SetInt("Map", Random.Range(0, 3));
		}
		else
		{
            PlayerPrefs.SetInt("Map",m_Map.value);
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
