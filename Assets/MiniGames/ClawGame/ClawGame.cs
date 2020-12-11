using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CraneState
{
    Stationary,
    IsExtending,
    StartClosing,
    IsClosing,
    StartOpening,
    IsOpening,
    IsRetracting,
    IsSlidingIn,
    IsSlidingOut
}
public class ClawGame : MonoBehaviour
{
    [SerializeField] private List<GameObject> armLinks;
    [SerializeField] private List<GameObject> clawArms;

    [SerializeField] private float maxExtensionLength;
    [SerializeField] private float extensionSpeed;
    [SerializeField] private float maxSlideLength;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float clawCloseTimer;

    [SerializeField] CraneState currentCraneState = CraneState.Stationary;

    public bool Extend = false;
    public bool Slide = false;
    private bool once = false;

    private float currentExtensionLength;
    private float currentSlideLength;
    private float slerpDelta = 0.0f;

    private Vector3 leftInitialClawPosition;
    private Vector3 rightInitialClawPosition;

    private Quaternion leftInitialRotation;
    private Quaternion rightInitialRotation;

    void Start()
    {
        currentExtensionLength = 0.0f;
        currentSlideLength = 0.0f;

        leftInitialClawPosition = clawArms[0].transform.localPosition;
        rightInitialClawPosition = clawArms[0].transform.localPosition;
    }

    void Update()
    {
        if(Extend)
        {
            currentCraneState = CraneState.IsExtending;
            Extend = false;
        }

        if(Slide)
        {
            currentCraneState = CraneState.IsSlidingOut;
        }
        else if(currentCraneState == CraneState.IsSlidingOut)
        {
            currentCraneState = CraneState.Stationary;
        }

        switch (currentCraneState)
        {
            case CraneState.IsExtending:
                if (currentExtensionLength < maxExtensionLength)
                {
                    if(once)
                    {
                        clawArms[0].GetComponent<Rigidbody2D>().freezeRotation = false;
                        clawArms[1].GetComponent<Rigidbody2D>().freezeRotation = false;
                        once = false;
                    }
                    currentExtensionLength += extensionSpeed;
                    UpdateArmLinks();
                }
                else
                {
                    currentCraneState = CraneState.StartClosing;
                }
                break;

            case CraneState.StartClosing:
                StartCoroutine("CloseClaw");
                currentCraneState = CraneState.IsClosing;
                break;

            case CraneState.StartOpening:
                leftInitialRotation = clawArms[0].transform.rotation;
                rightInitialRotation = clawArms[1].transform.rotation;
                currentCraneState = CraneState.IsOpening;
                break;

            case CraneState.IsOpening:
                OpenClaw();
                break;

            case CraneState.IsRetracting:
                if (currentExtensionLength > 0.0f)
                {
                    currentExtensionLength -= extensionSpeed;
                    UpdateArmLinks();
                }
                else
                {
                    currentCraneState = CraneState.IsSlidingIn;
                    clawArms[0].GetComponent<HingeJoint2D>().useMotor = false;
                    clawArms[1].GetComponent<HingeJoint2D>().useMotor = false;
                    clawArms[0].GetComponent<Rigidbody2D>().freezeRotation = true;
                    clawArms[1].GetComponent<Rigidbody2D>().freezeRotation = true;
                    once = true;
                }
                break;

            case CraneState.IsSlidingIn:
                if (currentSlideLength > 0.0f)
                {
                    currentSlideLength -= slideSpeed;
                    transform.localPosition = new Vector3(currentSlideLength, 0.0f, 0.0f);
                }
                else
                {
                    currentCraneState = CraneState.StartOpening;
                }
                break;

            case CraneState.IsSlidingOut:
                if (currentSlideLength < maxSlideLength)
                {
                    currentSlideLength += slideSpeed;
                    transform.localPosition = new Vector3(currentSlideLength, 0.0f, 0.0f);
                }
                else
                {
                    currentCraneState = CraneState.Stationary;
                }
                break;

            case CraneState.Stationary:
            case CraneState.IsClosing:
            default:
                break;
        }

        foreach (var Arm in clawArms)
        {
            Arm.transform.localPosition = leftInitialClawPosition;
        }
    }

    void UpdateArmLinks()
    {
        for (int i = 0; i < armLinks.Count; ++i)
        {
            armLinks[i].transform.localPosition = new Vector3(0.0f, currentExtensionLength * ((float)(i + 1) / (float)armLinks.Count), 0.0f);
        }
    }

    private void OpenClaw()
    {
        if(slerpDelta > 1.0f)
        {
            currentCraneState = CraneState.Stationary;
            slerpDelta = 0.0f;
            return;
        }
        clawArms[0].transform.rotation = Quaternion.Slerp(leftInitialRotation, Quaternion.Euler(0.0f, 0.0f, -45.0f), slerpDelta);
        clawArms[1].transform.rotation = Quaternion.Slerp(rightInitialRotation, Quaternion.Euler(0.0f, 0.0f, -135.0f), slerpDelta);
        slerpDelta += Time.fixedDeltaTime * 0.5f;
    }

    private IEnumerator CloseClaw()
    {
        foreach(var Arm in clawArms)
        {
            Arm.GetComponent<HingeJoint2D>().useMotor = true;
        }
        yield return new WaitForSeconds(clawCloseTimer);
        currentCraneState = CraneState.IsRetracting;
    }

    public CraneState GetCurrentState()
    {
        return currentCraneState;
    }
}
