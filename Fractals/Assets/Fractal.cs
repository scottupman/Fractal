using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Fractal : MonoBehaviour
{
    // you can use ( ""this"" keyword for accessing these variables )
    public float childScale = .5f;
    public Mesh mesh;
    public Material material;
    public int maxDepth = 5;
    int depth = 0;
    Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left, Vector3.up };
    Quaternion[] orientations = { Quaternion.Euler(90f, 0f, 0f), Quaternion.Euler(-90f, 0f, 0f), Quaternion.Euler(0f, 0f, -90f), Quaternion.Euler(0f, 0f, 90f), Quaternion.identity };    
    
    // Start is called before the first frame update
    void Start()
    {
        float generatedNumber = Random.Range(1, 40);

        Debug.Log("Random Number = " + generatedNumber);

        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
             
        // This is why we get 5 squares each child because the depth only increases by one per 5 children
        if (depth < maxDepth)
        {
           CreateChildren();           
        }        
    }

    void Initialize(Fractal parent, int childIndex)
    {
        this.mesh = parent.mesh;
        this.material = parent.material;
        this.material.color = parent.material.color;
        
        // this.depth is essentially the new parent.depth we pass into the Initialize method
        // It doesn't do it for each square because we are adding the Fractal component to each child made
        // So we have to assign the depth to each child using the field value
        this.depth = parent.depth + 1;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = directions[childIndex] * childScale + directions[childIndex] * (childScale/2f);
        transform.localRotation = orientations[childIndex];
    }

    void CreateChildren()
    {       
        for (int i = 0; i < directions.Length; i++)
        {           
            new GameObject("Child Fractal").AddComponent<Fractal>().Initialize(this, i);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 30f * Time.deltaTime, 0f);
    }
}
