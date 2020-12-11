using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeField : MonoBehaviour
{
    public Matrix3<PipeData> pipeMatrix = new Matrix3<PipeData>();
    private Matrix3<PipeData> correctKey = new Matrix3<PipeData>();
    [SerializeField] PipeState correctKey00PipeState;
    [SerializeField] PipeState correctKey01PipeState;
    [SerializeField] PipeState correctKey02PipeState;
    [SerializeField] PipeState correctKey10PipeState;
    [SerializeField] PipeState correctKey11PipeState;
    [SerializeField] PipeState correctKey12PipeState;
    [SerializeField] PipeState correctKey20PipeState;
    [SerializeField] PipeState correctKey21PipeState;
    [SerializeField] PipeState correctKey22PipeState;

    void Start()
    {
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                correctKey[i][j] = new PipeData();
            }
        }

        correctKey[0][0].currentPipeState = correctKey00PipeState;
        correctKey[0][1].currentPipeState = correctKey01PipeState;
        correctKey[0][2].currentPipeState = correctKey02PipeState;
        correctKey[1][0].currentPipeState = correctKey10PipeState;
        correctKey[1][1].currentPipeState = correctKey11PipeState;
        correctKey[1][2].currentPipeState = correctKey12PipeState;
        correctKey[2][0].currentPipeState = correctKey20PipeState;
        correctKey[2][1].currentPipeState = correctKey21PipeState;
        correctKey[2][2].currentPipeState = correctKey22PipeState;

        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                pipeMatrix[i][j] = new PipeData();
                pipeMatrix[i][j].pipeTransform = GameObject.Find($"Pipe{i}{j}").transform;
                pipeMatrix[i][j].currentPipeState = (PipeState)Random.Range(0, 4);
                pipeMatrix[i][j].UpdateTransform();
            }
        }
    }

    void Update()
    {
        bool anyMissing = true;
        for(int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                pipeMatrix[i][j].UpdateTransform();
                anyMissing = anyMissing && pipeMatrix[i][j].Compare(correctKey[i][j]);
            }
        }
        if(anyMissing)
        {
            GetComponentInParent<PipeGame>().Finished();
        }
    }
}
