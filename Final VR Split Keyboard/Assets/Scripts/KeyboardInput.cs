using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{

    string word = null;
    int wordIndex = -1;
    string alpha = null;
    string alpha2 = null;
    char[] inputChar = new char[30];
    //public Text index = null;
    public Text inputText = null;

    public void alphabetFunction(string alphabet)
    {
        //wordIndex++;
        //word = word + alphabet;
        //inputText.text = word;

        wordIndex++;
        char[] keepChar = alphabet.ToCharArray();
        inputChar[wordIndex] = keepChar[0];
        alpha = inputChar[wordIndex].ToString();
        word = word + alpha;
        inputText.text = word;
        //index = wordIndex.ToString();
    }

    public void backspaceFunction(string alphabet)
    {
        if (wordIndex > 0)
        {
            wordIndex--;
            alpha2 = null;

            for (int i = 0; i < wordIndex + 1; i++)
            {
                alpha2 = alpha2 + inputChar[i].ToString();
            }

            word = alpha2;
            inputText.text = word;

        }
    }
}