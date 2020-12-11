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
    public List<List<T>> data = new List<List<T>>();

    public Matrix3()
    {
        for (int i = 0; i < 3; ++i)
        {
            data.Add(new List<T>());
            for (int j = 0; j < 3; ++j)
            {
                data[i].Add(default(T));
            }
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

    public bool Compare(PipeData other)
    {
        return currentPipeState == other.currentPipeState || (currentPipeState == PipeState.Any || other.currentPipeState == PipeState.Any);
    }

    public void UpdateTransform()
    {
        switch(currentPipeState)
        {
            case PipeState.North:
                pipeTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;
            case PipeState.East:
                pipeTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
                break;
            case PipeState.South:
                pipeTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                break;
            case PipeState.West:
                pipeTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                break;
            default:
                break;
        }
    }
}

public class PipeGame : MonoBehaviour, IMiniGame
{
    [SerializeField] private Camera ClawCamera;
    [SerializeField] private List<Camera> OtherCameras = new List<Camera>();
    [SerializeField] private List<GameObject> pipeFields;
    private PipeData currentPipeData;
    private int activeBoard;
    public bool IsFinished { get; private set; } = false;
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Select00()
    {
        currentPipeData = pipeFields[activeBoard].GetComponent<PipeField>().pipeMatrix[0][0];
    }

    public void Select01()
    {
        currentPipeData = pipeFields[activeBoard].GetComponent<PipeField>().pipeMatrix[0][1];
    }

    public void Select02()
    {
        currentPipeData = pipeFields[activeBoard].GetComponent<PipeField>().pipeMatrix[0][2];
    }

    public void Select10()
    {
        currentPipeData = pipeFields[activeBoard].GetComponent<PipeField>().pipeMatrix[1][0];
    }

    public void Select11()
    {
        currentPipeData = pipeFields[activeBoard].GetComponent<PipeField>().pipeMatrix[1][1];
    }

    public void Select12()
    {
        currentPipeData = pipeFields[activeBoard].GetComponent<PipeField>().pipeMatrix[1][2];
    }

    public void Select20()
    {
        currentPipeData = pipeFields[activeBoard].GetComponent<PipeField>().pipeMatrix[2][0];
    }

    public void Select21()
    {
        currentPipeData = pipeFields[activeBoard].GetComponent<PipeField>().pipeMatrix[2][1];
    }

    public void Select22()
    {
        currentPipeData = pipeFields[activeBoard].GetComponent<PipeField>().pipeMatrix[2][2];
    }

    public void RotateRight()
    {
        if (currentPipeData != null)
        {
            currentPipeData.currentPipeState = (PipeState)((int)++currentPipeData.currentPipeState % 4);
        }
    }

    public void RotateLeft()
    {
        if (currentPipeData != null)
        {
            currentPipeData.currentPipeState = (PipeState)(((int)--currentPipeData.currentPipeState + 4) % 4);
        }
    }

    public void OnMalfunctionStart()
    {
        IsFinished = false;
    }

    public void StartMiniGame()
    {
        FindObjectOfType<MalfunctionManager>().PauseMalfunctionCreation = true;
        if (!gameObject.activeSelf)
		{
            activeBoard = Random.Range(0, pipeFields.Count);
            pipeFields[activeBoard].SetActive(true);
        }
        OtherCameras.ForEach(camera => camera.enabled = false);
        ClawCamera.enabled = true;
        gameObject.SetActive(true);
    }

    public void Finished()
    {
        FindObjectOfType<MalfunctionManager>().PauseMalfunctionCreation = false;
        pipeFields[activeBoard].SetActive(false);
        gameObject.SetActive(false);
        ClawCamera.enabled = false;
        OtherCameras.ForEach(camera => camera.enabled = true);
        IsFinished = true;
    }
}
