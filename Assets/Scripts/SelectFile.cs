using SFB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Windows.Forms;
using TMPro;
using System.Linq;

public class SelectFile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI defaultDisplay;
    public void OpenFile()
    {
        //Imported library for getting files
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "txt", true);

        StreamReader reader = new StreamReader(path[0]);
        string line;
        List<string> allLines = new List<string>();
        while ((line = reader.ReadLine()) != null)
        {
            allLines.Add(line);
        }

        allLines.RemoveRange(allLines.Count - allLines.Count%4, allLines.Count % 4); //Make sure ther row count is a divider of 4
        int numberOfRows = allLines.Count/4;

        string defaultResult = "";
        foreach (string s in allLines)
            defaultResult += s + "\n";
        defaultDisplay.text = defaultResult; //display the encoded text

        FormatText formatter = new FormatText();

        GetComponent<DecodeFile>().Decodetext(formatter.Format(allLines));
    }
}
