using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    [Tooltip("This should always be the AI")]
    [SerializeField] SpawnAI m_AI;


    int m_Matches = 1;


    int m_BlueWon = 0;
    int m_RedWon = 0;


    [SerializeField]TMPro.TextMeshProUGUI m_RedWin = null;
    [SerializeField]TMPro.TextMeshProUGUI m_BlueWin = null;
    [SerializeField]TMPro.TextMeshProUGUI m_RedHealth = null;
    [SerializeField]TMPro.TextMeshProUGUI m_BlueHealth = null;


	// Start is called before the first frame update
	void Start()
    {
        if (m_BlueWin)
        {
            m_BlueWin.text = "Blue Wins: " + m_BlueWon;
        }

        if (m_RedWin)
        {
            m_RedWin.text = "Red Wins: " + m_RedWon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_AI)
		{
            if(m_AI.GetMatchesSize() > m_Matches)
			{
                m_Matches++;

                var winner = PlayerPrefs.GetString("LastWinner");


                if(winner == "Blue")
				{
                    m_BlueWon++;

                    if(m_BlueWin)
					{
                        m_BlueWin.text = "Blue Wins: " + m_BlueWon;
					}
				}

                if (winner == "Red")
                {
                    m_RedWon++;

                    if(m_RedWin)
                    {
                        m_RedWin.text = "Red Wins: " + m_RedWon;
                    }
                }

                if (winner == "Tie")
                {
                    m_RedWon++;
                    m_BlueWon++;

                    if (m_BlueWin)
                    {
                        m_BlueWin.text = "Blue Wins: " + m_BlueWon;
                    }

                    if (m_RedWin)
                    {
                        m_RedWin.text = "Red Wins: " + m_RedWon;
                    }
                }
            }


            var redGo = m_AI.GetRed();
            var blueGo = m_AI.GetBlue();


            if(redGo && blueGo)
			{
                var redFighter = redGo.GetComponent<FighterScript>();
                var blueFighter = blueGo.GetComponent<FighterScript>();


                if(redFighter && blueFighter && m_RedHealth && m_BlueHealth)
				{
                    m_RedHealth.text = "Red Health: " + redFighter.GetHealth();
                    m_BlueHealth.text = "Blue Health: " + blueFighter.GetHealth();
				}
			}

            


		}
    }
}
