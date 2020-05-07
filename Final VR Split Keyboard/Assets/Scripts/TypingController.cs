using System;
using UnityEngine;
using UnityEngine.UI;
using static OVRHand;

public class TypingController : MonoBehaviour
{
    public GameObject modelData;
    private TypingModel model;

    public Text PhraseDisplay;
    public Text InstructionsDisplay;
    public Text DebugDisplay;

    private bool GameOn = false;

    private bool PieMenuOn = false;

    private DateTime PracticeStartTime;
    private DateTime TaskStartTime;
    private double TaskTime = 0;
    private double PracticeTime = 0;
    private int currPhraseInd = -1;
    private int backspaceHitsCount = 0;

    public OVRHand lefthand;
    public OVRHand righthand;

    public GameObject menu;
    public GameObject keyboard;

    public GameObject returnButton;
    public GameObject mainMenuButton;


    void Awake()
    {
        if (modelData != null)
            model = modelData.GetComponent<TypingModel>();
        
        PracticeStartTime = DateTime.Now;
        //DebugDisplay.text = "AWAKE";
    }

    void Start()
    {
        //if (modelData != null)
        //    model = modelData.GetComponent<TypingModel>();
        //model.SaveDateForRef(DateTime.Now);
        //PracticeStartTime = DateTime.Now;
    }

    private bool BothIndexPinching(OVRHand LHand, OVRHand RHand)
    {
        if (LHand.GetFingerConfidence(HandFinger.Index) == TrackingConfidence.High && RHand.GetFingerConfidence(HandFinger.Index) == TrackingConfidence.High)
        {
            if (LHand.GetFingerIsPinching(HandFinger.Index) && lefthand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > 0.9
                && RHand.GetFingerIsPinching(HandFinger.Index) && righthand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > 0.9)
                return true;
        }
        return false;
    }

    void Update()
    {
        if (keyboard != null && !GameOn)
        {
            if (!PieMenuOn)
            {
                keyboard.SetActive(true);
                if (BothIndexPinching(lefthand, righthand))
                    ShowPieMenu();
            }
            else
                keyboard.SetActive(false);
        }
    }

    private void ShowPieMenu()
    {
        Vector3 newPos = (lefthand.transform.position + righthand.transform.position) / 2;
        newPos.y += 0.02f;
        newPos.z += 0.1f;

        menu.transform.position = newPos;
        menu.gameObject.SetActive(true);
        PieMenuOn = true;
    }

    private void UpdateNext()
    {
        if (!GameOn)
            return;
        if (currPhraseInd < model.GetPhrasesCount())
            currPhraseInd += 1;
        UpdateCurrProgress();
        string s = model.GetPhrase(currPhraseInd);
        if (s != null)
            PhraseDisplay.text = s;
        else
        {
            PhraseDisplay.text = "";
            InstructionsDisplay.text = "Congrats! The task is finished." + "\n" + "Please return to the main menu.";
            EndGame();
        }
    }

    //todo
    private void UpdateCurrProgress()
    {
        InstructionsDisplay.text = "Completed " + currPhraseInd.ToString() + "/" + model.GetPhrasesCount().ToString();
    }

    private void EndGame()
    {

        TaskTime = DateTime.Now.Subtract(TaskStartTime).TotalSeconds;   // elapsed time since game starts

        GameOn = false;                                 // toggle off the game state

        // prompts model to save the final data
        model.SaveDateForRef(PracticeStartTime);
        model.SaveHits(backspaceHitsCount);
        model.SaveTaskTime(TaskTime);
        model.SavePracticeTime(PracticeTime);

        model.SaveDataToLocal();

        if (mainMenuButton != null)
            mainMenuButton.SetActive(true);
        if (returnButton != null)
            returnButton.SetActive(false);
    }

    // when ready key is pressed
    public void StartGame()
    {
        TaskStartTime = DateTime.Now;   // register the start time
        PracticeTime = TaskStartTime.Subtract(PracticeStartTime).TotalSeconds;
        model.SavePracticeTime(PracticeTime);
        GameOn = true;              // toggle on the game state
        InstructionsDisplay.text = "Type down the phrase and hit return to submit.";
        UpdateNext();               // retrieve the first phrase
    }

    // when backspace key is pressed
    public void UpdateBackspaceHit()
    {
        if (GameOn)
            backspaceHitsCount += 1;
    }

    // when return key is pressed
    public void SaveCurrData(string str)
    {
        if (!GameOn)
            model.SaveTrialData(str);
        else
        {
            model.SaveCurrEntry(str);
            UpdateNext();
        }
    }


    public bool IfGameOn()
    {
        return GameOn;
    }

    public void PieMenuOff()
    {
        PieMenuOn = false;
    }


    //public void QuitApp()
    //{
    //    Application.Quit();
    //}

}
