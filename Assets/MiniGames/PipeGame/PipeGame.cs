using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PipeState
{
    North,
    East,
    South,
    West,
    Any
}

public class Matrix3<T>
{
    public List<List<T>> data = new List<List<T>>(3);

    public Matrix3()
    {
        foreach(var row in data)
        {
            row.Capacity = 3;
        }
    }
   
    public List<T> this[int i]
    {
        get { return data[i]; }
        set { data[i] = value; }
    }
}

public class PipeData
{
    public PipeState currentPipeState = PipeState.North;
    public Transform pipeTransform;
}

public class PipeGame : MonoBehaviour
{
    private Matrix3<PipeData> pipeMatrix = new Matrix3<PipeData>();
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
