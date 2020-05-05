using System;
using UnityEngine;
using UnityEngine.UI;

public class TypingController : MonoBehaviour
{
    public GameObject modelData;
    private TypingModel model;

    public Text PhraseDisplay;
    public Text InstructionsDisplay;
    public Text hitsDisplay;
    public Text DebugDisplay;

    private bool GameOn = false;

    private DateTime StartTime;
    private double elapsed = 0;
    private int currPhraseInd = -1;
    private int backspaceHitsCount = 0;

    void Awake()
    {
        if (modelData != null)
            model = modelData.GetComponent<TypingModel>();
    }

    void Update()
    {
        //todo: for debugging
        //if (GameOn)
        //{
        //    hitsDisplay.text = backspaceHitsCount.ToString() + " hits";
        //    DebugDisplay.text = DateTime.Now.Subtract(StartTime).TotalSeconds.ToString();
        //}
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
            InstructionsDisplay.text = "Congrats! The task is finished.";
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
        elapsed = DateTime.Now.Subtract(StartTime).TotalSeconds;   // elapsed time since game starts

        GameOn = false;                                 // toggle off the game state

        // prompts model to save the final data
        model.SaveHits(backspaceHitsCount);
        model.SaveElapsedTime(elapsed);

        model.SaveDataToLocal();
    }

    // when ready key is pressed
    public void StartGame()
    {
        if (!GameOn)
        {
            StartTime = DateTime.Now;   // register the start time
            GameOn = true;              // toggle on the game state
            InstructionsDisplay.text = "Type down the phrase and hit return to submit.";
            UpdateNext();               // retrieve the first phrase
        }
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

    public void QuitApp()
    {
        Application.Quit();
    }

}
