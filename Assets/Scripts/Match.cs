using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match 
{
	AiController m_Red = null;
	AiController m_Blue = null;
	PlayerController m_Player = null;


	int m_RedTech = 0;
	int m_BlueTech = 0;


	int m_MapType = 0;

	bool m_AIMatch = false;

	bool m_Finished = false;

	bool m_RedWin = false;

	bool m_Tie = false;

	public Match(AiController red, AiController blue, int redTech, int blueTech, int map)
	{
		m_Red = red;
		m_Blue = blue;

		m_RedTech = redTech;
		m_BlueTech = blueTech;

		m_MapType = map;
		m_AIMatch = true;
		

	}


	public Match(AiController red, PlayerController blue, int redTech, int blueTech, int map)
	{
		m_Red = red;
		m_Player = blue;

		m_RedTech = redTech;
		m_BlueTech = blueTech;

		m_MapType = map;

		m_AIMatch = false;

		
	}

	public Weapon.Type GetBlueWeapon()
	{
		if(m_Blue)
		{
			return m_Blue.GetWeaponType();
		}

		if(m_Player)
		{
			return m_Player.GetWeaponType();
		}

		return Weapon.Type.Sword;
	}

	public Weapon.Type GetRedWeapon()
	{
		return m_Red.GetWeaponType();
	}

	public string GetWinner()
	{
		if(m_Tie)
		{
			return "Tie";
		}

		if(m_RedWin)
		{
			return "Red";
		}


		return "Blue";
	}

	public void SetFinished(bool value)
	{
		m_Finished = value;
	}

	public bool GetFinished()
	{
		return m_Finished;
	}

	public int GetMap()
	{
		return m_MapType;
	}

	public int GetBlueTech()
	{
		return m_BlueTech;
	}

	public int GetRedTech()
	{
		return m_RedTech;
	}

	public bool GetAIMatch()
	{
		return m_AIMatch;
	}
	public void Finish()
	{
		m_Finished = true;


		var red = false;
		var blue = false;
		if(m_Red)
		{
			if(m_Red.isActiveAndEnabled)
			{
				m_RedWin = true;
				red = true;
				PlayerPrefs.SetString("LastWinner", "Red");
			}
		}

		if(m_Blue)
		{
			if (m_Blue.isActiveAndEnabled)
			{
				m_RedWin = false;

				blue = true;
				PlayerPrefs.SetString("LastWinner", "Blue");
				
			}
		}

		

		if (m_Player)
		{
			if (m_Player.isActiveAndEnabled)
			{
				m_RedWin = false;
				blue = true;
				PlayerPrefs.SetString("LastWinner", "Blue");
			}
		}

		if(!blue && !red || blue && red)
		{
			m_Tie = true;
			PlayerPrefs.SetString("LastWinner", "Tie");
		}
		else
		{
			m_Tie = false;
		}

		m_Red.gameObject.SetActive(false);

		if(m_Blue)
		{
			m_Blue.gameObject.SetActive(false);
		}
		else if(m_Player)
		{
			m_Player.gameObject.SetActive(false);
		}

	}

}
