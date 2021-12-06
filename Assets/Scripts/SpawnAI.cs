using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAI : MonoBehaviour
{
    [SerializeField] GameObject m_AIPrefab = null;
    [SerializeField] GameObject m_RedLocation = null;
    [SerializeField] GameObject m_BlueLocation = null;
    [SerializeField] Material m_BlueMaterial = null;

    GameObject m_Red;
    GameObject m_Blue;

    // Start is called before the first frame update
    void Start()
    {
        if (m_AIPrefab != null && m_RedLocation != null && m_BlueLocation != null)
        {
            m_Red = Instantiate(m_AIPrefab, m_RedLocation.transform.position, m_RedLocation.transform.rotation);
            m_Blue = Instantiate(m_AIPrefab, m_BlueLocation.transform.position, m_BlueLocation.transform.rotation);

            m_Blue.GetComponent<MeshRenderer>().material = m_BlueMaterial;






            var redAI = m_Red.GetComponent<AiController>();
            var blueAI = m_Blue.GetComponent<AiController>();

            redAI.SetWeapon(Weapon.Type.Bow);
            blueAI.SetWeapon(Weapon.Type.Pike);

            redAI.m_Opponent = m_Blue;
            blueAI.m_Opponent = m_Red;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
