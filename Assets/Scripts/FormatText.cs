using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormatText
{
    public FormatText()
    {

    }

    public List<char[]> Format(List<string> allLines)
    {
        int numberOfRows = allLines.Count / 4;
        
        for (int i = 0; i < allLines.Count; i++)
        {
            allLines[i] = allLines[i].Replace("\t", "   ");
        }

        int[] maxlength = new int[numberOfRows * 4]; 
        for (int i = 0; i < numberOfRows; i++) // Get the max length of each 4 rows
        {
            int length = allLines[i * 4].Length;
            for (int g = 1; g < 4; g++)
            {
                if (length < allLines[i * 4 + g].Length)
                    length = allLines[i * 4 + g].Length;
            }

            for (int r = 0; r < 4; r++)
            {
                maxlength[i * 4 + r] = length;
            }
        }

        List<char[]> chars = new List<char[]>();
        for (int i = 0; i < numberOfRows * 4; i++) //fills every symbol with space
        {
            chars.Add(new char[maxlength[i] + 1]);
            for (int r = 0; r < maxlength[i] + 1; r++)
            {
                chars[i][r] = ' ';
            }
        }

        for (int i = 0; i < allLines.Count; i++) //replace the spaces for the read content
        {
            char[] charsInLine = allLines[i].ToArray();
            for (int w = 0; w < allLines[i].Length; w++)
            {
                chars[i][w] = charsInLine[w];
            }
        }

        return chars;
    }
}
