using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Build()
	{
        var mesh = GetComponent<NavMeshSurface>();


        if (mesh)
        {
            mesh.RemoveData();

            mesh.BuildNavMesh();
        }

    }

    // Update is called once per frame
    void Update()
    {

       

        
    }
}
