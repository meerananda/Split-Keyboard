using System.Collections;
using System.Collections.Generic;
using OculusSampleFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyInput : MonoBehaviour
{
    public InputField TextField;
    public GameObject TypingController;
    private TypingController controller;

    public bool HandTrackingEnabled;
    void Awake()
    {
        if (TypingController != null)
            controller = TypingController.GetComponent<TypingController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!HandTrackingEnabled)
        {
            if (!controller.IfGameOn())
            {
                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    ClearTextField();
                    controller.StartGame();
                }
            }
        }
        
    }

    public void ButtonStateChanged(InteractableStateArgs obj)
    {
        if (obj.NewInteractableState == InteractableState.ActionState)
        {

        }
    }

    public void InsertAlphabet(Object obj)
    {
        TextField.text += obj.name;
    }

    public void InsertAlphabet(GameObject obj)
    {
        TextField.text += obj.transform.GetChild(0).GetComponent<Text>().text.ToLower();
    }

    public void BackSpace()
    {
        if (TextField.text.Length > 0)
        {
            TextField.text = TextField.text.Remove(TextField.text.Length - 1);
            controller.UpdateBackspaceHit();
        }
    }

    public void InsertSpace()
    {
        TextField.text += " ";
    }

    public void ReadyPressed()
    {
        ClearTextField();
        controller.StartGame();
    }

    public void ReturnEntry()
    {
        //TextField.text
        controller.SaveCurrData(TextField.text);
        ClearTextField();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // empty the text
    void ClearTextField()
    {
        TextField.text = "";
    }


    //public void QuitPressed()
    //{
    //    controller.QuitApp();
    //}
}
