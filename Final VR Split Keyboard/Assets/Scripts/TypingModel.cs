using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class TypingModel : MonoBehaviour
{
    public TextAsset phrasesFile;

    private List<string> phrasesLst;
    private List<string> EntryLst;
    private int TotalCount;

    // dynamic data passed from Typing controller
    private int HitsCount;
    private double PracticeTime;
    private double TaskTime;
    private string TrialEntry;
    private string Timestamp;

    void Awake()
    {
        ResetData();
        PraseTextFile(phrasesFile);
    }

    private void ResetData()
    {
        phrasesLst = new List<string>();
        EntryLst = new List<string>();
        TotalCount = 0;
        HitsCount = 0;
        PracticeTime = 0f;
        TaskTime = 0f;
        TrialEntry = "";
        Timestamp = "";
    }

    private void PraseTextFile(TextAsset txt)
    {
        if (txt != null && phrasesLst != null)
        {
            var arrStr = txt.text.Split('\n');
            foreach (var line in arrStr)
                phrasesLst.Add(line);

            TotalCount = phrasesLst.Count;
        }
    }

    public int GetPhrasesCount()
    {
        return TotalCount;
    }


    public string GetPhrase(int i)
    {
        if (phrasesLst != null && i < TotalCount)
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
        if (EntryLst != null)
            EntryLst.Add(str);
    }

    public void SaveHits(int i)
    {
        HitsCount = i;
    }

    public void SavePracticeTime(double t)
    {
        PracticeTime = t;
    }

    public void SaveTaskTime(double t)
    {
        TaskTime = t;
    }


    public int GetEntriesCount()
    {
        return EntryLst.Count;
    }

    private string CurrData()
    {
        string data = "==========================" + "\n";
        data += Timestamp + "\n";
        data += "Practice: [" + TrialEntry + "]";
        data += "\n" + HitsCount + " hits";
        data += "\n" + "Practice Time: " + PracticeTime + " seconds";
        data += "\n" + "Task Time: " + TaskTime + " seconds" + "\n";
        data += string.Join("\n", EntryLst.ToArray());
        data += "\n";
        return data;
    }

    public void SaveDateForRef(DateTime time)
    {
        Timestamp = time.ToString();
    }

    public void SaveDataToLocal()
    {
        string data = CurrData();
        ResetData();
        try
        {
            File.AppendAllText(Application.persistentDataPath + "/TypingRecords.txt", data);
        }
        catch (Exception e)
        {

        }


    }


}
