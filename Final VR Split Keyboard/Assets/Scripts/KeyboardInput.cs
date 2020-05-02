using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{

    string word = null;
    int wordIndex = 0;
    string alpha;
    public Text inputText = null;

    public void alphabetFunction(string alphabet)
    {
        wordIndex++;
        word = word + alphabet;
        inputText.text = word;

    }
}