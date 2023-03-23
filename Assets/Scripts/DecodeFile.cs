using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

[System.Serializable]
struct codedNumbers
{
    public string Zeichenfolge;
    public string representiveNumber;
}
public class DecodeFile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI decodeTextUI;
    [SerializeField] private List<codedNumbers> codedNumbers;
    public void Decodetext(List<char[]> chars)
    {
        if (chars.Count % 4 == 0)
        {
            List<string> seperatedText = new List<string>();
            
            for (int i = 0; i < chars.Count/4; i++) //goes through every 4 rows
            {
                int capture = -1;
                for (int g = 0; g < getMinLength(i,chars); g++) //checks if the column is empty
                {
                    bool empty = true;
                    for (int m = 0; m < 4; m++) 
                    {
                        if (chars[i * 4 + m][g] != ' ')
                        {
                            empty = false;
                            break;
                        }
                    }

                    if(empty) // save the progress from the last empty line to this one
                    {
                        if(g - capture > 1 || g == getMinLength(i, chars) -1)
                        {
                            string areaResult = "";
                            for (int h = 0; h < 4; h++)
                            {
                                for (int r = 0; r < g - capture; r++)
                                {
                                    if (!(h == 3 && r == g - capture - 1))
                                        areaResult += chars[h + i * 4][r + capture + 1];
                                }
                            }
                            seperatedText.Add(areaResult);
                        }

                        capture = g;
                    }
                }
            }

            string result = "";
            foreach(string s in seperatedText) //checks for each letter if it's correct
            {
                foreach(codedNumbers c in codedNumbers)
                {
                    if(s == c.Zeichenfolge)
                    {
                        result += c.representiveNumber + ", ";
                        break;
                    }    
                }
            }

            decodeTextUI.text= result;
        }
        else
            Debug.LogError("Incorrect format. Line Count: " + chars.Count);
    }

    private int getMinLength(int row, List<char[]> array) // get the mininmum length of 4 rows
    {
        int minlength = array[row * 4].Length;
        for (int i = 1; i < 4; i++)
        {
            if (minlength > array[row * 4 + i].Length)
                minlength = array[row * 4 + i].Length;
        }
        return minlength;
    }
}
