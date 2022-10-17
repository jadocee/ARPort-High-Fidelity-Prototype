using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TMPCurrentTime : MonoBehaviour
{
    
    [SerializeField] TMP_Text currentTime;

    // Update is called once per frame
    void Update()
    {
        // Use current time, with a format string.
        DateTime time = DateTime.Now;
        string timeFormat = "t";
        DateTime date = DateTime.Now;
        string dateFormat = "D";
        currentTime.text = time.ToString(timeFormat) + "\n" + date.ToString(dateFormat);
    }
}
