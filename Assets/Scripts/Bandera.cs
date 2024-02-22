using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Bandera : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new[]
        {
            new Vector3 (0,0,0),
            new Vector3 (0,1,0),
            new Vector3 (1,0,0),
        };

        mesh.vertices = vertices;

        triangles = new[] { 0, 1, 2 };

        mesh.triangles = triangles;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
