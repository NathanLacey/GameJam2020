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
public class ClawGame : MonoBehaviour, IMiniGame
{
    [SerializeField] private GameObject PlayerHud;
    [SerializeField] private Camera ClawCamera;
    [SerializeField] private List<Camera> OtherCameras = new List<Camera>();
    [SerializeField] private Bag bag;
    [SerializeField] private List<GameObject> armLinks;
    [SerializeField] private List<GameObject> clawArms;

    [SerializeField] private GameObject toySpawner;

    [SerializeField] private float maxExtensionLength;
    [SerializeField] private float extensionSpeed;
    [SerializeField] private float maxSlideLength;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float clawCloseTimer;

    [SerializeField] CraneState currentCraneState = CraneState.Stationary;

    private bool onceExtend = true;
    private bool onceSlide = true;
    private bool onceFreezeRotation = false;

    private float currentExtensionLength;
    private float currentSlideLength;
    private float slerpDelta = 0.0f;

    private Vector3 initialClawPosition;

    private Quaternion leftInitialRotation;
    private Quaternion rightInitialRotation;

    public bool IsFinished { get; private set; } = false;

	void Start()
    {
        Reset();
        initialClawPosition = clawArms[0].transform.localPosition;
    }

    void Reset()
	{
        currentExtensionLength = 0.0f;
        currentSlideLength = 0.0f;
    }

    void FixedUpdate()
    {
        if(bag.HasWon)
		{
            toySpawner.SetActive(false);
            ClawCamera.enabled = false;
            OtherCameras.ForEach(camera => camera.enabled = true);
            PlayerHud.SetActive(true);
            IsFinished = true;
            FindObjectOfType<MalfunctionManager>().PauseMalfunctionCreation = false;
            return;
        }

        switch (currentCraneState)
        {
            case CraneState.IsExtending:
                if (currentExtensionLength < maxExtensionLength)
                {
                    if(onceFreezeRotation)
                    {
                        clawArms[0].GetComponent<Rigidbody2D>().freezeRotation = false;
                        clawArms[1].GetComponent<Rigidbody2D>().freezeRotation = false;
                        onceFreezeRotation = false;
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
                onceSlide = true;
                onceExtend = true;
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
                    onceFreezeRotation = true;
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
            Arm.transform.localPosition = initialClawPosition;
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

    public void StartSliding()
    {
        if (onceSlide)
        {
            if (currentCraneState != CraneState.IsSlidingOut)
            {
                currentCraneState = CraneState.IsSlidingOut;
            }
            else
            {
                currentCraneState = CraneState.Stationary;
                onceSlide = false;
            }
        }
    }

    public void StartExtending()
    {
        if (onceExtend && currentCraneState == CraneState.Stationary)
        {
            currentCraneState = CraneState.IsExtending;
            onceExtend = false;
        }
    }

	public void OnMalfunctionStart()
	{
        IsFinished = false;
        bag.HasWon = false;
        Reset();
    }

	public void StartMiniGame()
	{
        toySpawner.SetActive(true);
        PlayerHud.SetActive(false);
        OtherCameras.ForEach(camera => camera.enabled = false);
        ClawCamera.enabled = true;
        FindObjectOfType<MalfunctionManager>().PauseMalfunctionCreation = true;
    }
}
