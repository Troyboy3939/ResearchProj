using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour
{
    [Tooltip("The point at which raycast should be done on this sword, to check if it hit an enemy")]
    [SerializeField] protected GameObject m_HitPoint = null;


   [Tooltip("How much damage the weapon does every hit")]
   [SerializeField] protected float m_Damage = 25.0f;

    protected Type m_Type;


   public enum Type
	{
        Sword,
        Pike,
        Bow
	}

    abstract public void Attack();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

   public Type GetWeaponType()
	{
        return m_Type;
	}
}
