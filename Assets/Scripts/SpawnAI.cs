using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAI : MonoBehaviour
{
    [SerializeField] GameObject m_AIPrefab;
    [SerializeField] GameObject m_RedLocation;
    [SerializeField] GameObject m_BlueLocation;
    [SerializeField] Material m_BlueMaterial;

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
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
