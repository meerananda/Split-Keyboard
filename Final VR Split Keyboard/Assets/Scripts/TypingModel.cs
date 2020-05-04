using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class TypingModel : MonoBehaviour
{
    public TextAsset phrasesFile;

    private List<string> phrasesLst;
    private List<string> entryLst;
    private int totalCount;

    // dynamic data passed from Typing controller
    private int hitsCount;
    private float elapsedTime;
    private string TrialEntry;

    void Awake()
    {
        ResetData();
        PraseTextFile(phrasesFile);

    }

    private void ResetData()
    {
        phrasesLst = new List<string>();
        entryLst = new List<string>();
        totalCount = 0;
        hitsCount = 0;
        elapsedTime = 0f;
        TrialEntry = "";
    }

    private void PraseTextFile(TextAsset txt)
    {
        if (txt != null && phrasesLst != null)
        {
            var arrStr = txt.text.Split('\n');
            foreach (var line in arrStr)
                phrasesLst.Add(line);

            totalCount = phrasesLst.Count;
        }
    }

    public int GetPhrasesCount()
    {
        return totalCount;
    }


    public string GetPhrase(int i)
    {
        if (phrasesLst != null && i < totalCount)
        {
            return phrasesLst[i];
        }
        return null;
    }


    public void SaveTrialData(string str)
    {
        TrialEntry = str;
    }

    public void SaveCurrEntry(string str)
    {
        if (entryLst != null)
            entryLst.Add(str);
    }

    public void SaveHits(int i)
    {
        hitsCount = i;
    }

    public void SaveElapsedTime(float t)
    {
        elapsedTime = t;
    }

    public int GetEntriesCount()
    {
        return entryLst.Count;
    }

    private string CurrData()
    {
        string data = "==========================" + "\n";
        data += "Practice: [" + TrialEntry + "]";
        data += "\n" + hitsCount + " hits";
        data += "\n" + elapsedTime + " seconds" + "\n";
        data += string.Join("\n", entryLst.ToArray());
        data += "\n";
        return data;
    }

    public string DataUpdate()
    {
        return string.Join("\n", entryLst.ToArray());
    }

    public void SaveDataToLocal()
    {
        string data = CurrData();
        ResetData();
        try
        {
            File.AppendAllText(Application.persistentDataPath + "/TypingRecords.txt", data);
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream fs = new FileStream(Application.persistentDataPath + "/TypingRecords.txt", FileMode.OpenOrCreate);
            //bf.Serialize(fs, data);
            //fs.Close();
        }
        catch (Exception e)
        {

        }


    }
}
