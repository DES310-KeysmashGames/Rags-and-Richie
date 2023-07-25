using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichieScript : MonoBehaviour
{
    [SerializeField] TextDialogue[] dayStart;
    [SerializeField] TextDialogue[] custEnter;
    [SerializeField] TextDialogue[] custLeave;
    [SerializeField] TextDialogue[] dayEnd;
    [SerializeField] TextDialogue[] tutorialText;
    [SerializeField] TextDialogue[] day1; 
    [SerializeField] TextDialogue[] day2;
    [SerializeField] TextDialogue[] day3;

    public string GetDayStart()
    {
        int i = UnityEngine.Random.Range(0, dayStart.Length);
        return dayStart[i].lineOfDialogue;
    }

    public string GetDayEnd()
    {
        int i = UnityEngine.Random.Range(0, dayEnd.Length);
        return dayEnd[i].lineOfDialogue;
    }

    public string GetCustEnter()
    {
        int i = UnityEngine.Random.Range(0, custEnter.Length);
        return custEnter[i].lineOfDialogue;
    }

    public string GetCustLeave()
    {
        int i = UnityEngine.Random.Range(0, custLeave.Length);
        return custLeave[i].lineOfDialogue;
    }

    public string GetToxicTowers()
    {
        int i = UnityEngine.Random.Range(0, day1.Length);
        return day1[i].lineOfDialogue;
    }

    public string GetBurnington()
    {
        return day2[0].lineOfDialogue;
    }

    public string GetSkyHigh()
    {
        return day2[1].lineOfDialogue;
    }

    public string GetBrokenMetro()
    {
        return day3[0].lineOfDialogue;
    }

    public string GetVacancy()
    {
        return day3[1].lineOfDialogue;
    }
    
    public string GetLostAngeles()
    {
        return day3[2].lineOfDialogue;
    }

    public string GetTutorial(int index)
    {
        return tutorialText[index].lineOfDialogue;
    }

    public int GetTutorialLength()
    {
        return tutorialText.Length;
    }
}
