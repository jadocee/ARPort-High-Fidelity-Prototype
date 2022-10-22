using System;
using TMPro;
using UnityEngine;

public class TMPCurrentTime : MonoBehaviour
{
    [SerializeField] private TMP_Text currentTime;

    // Update is called once per frame
    private void Update()
    {
        // Use current time, with a format string.
        var time = DateTime.Now;
        var timeFormat = "t";
        var date = DateTime.Now;
        var dateFormat = "D";
        currentTime.text = time.ToString(timeFormat) + "\n" + date.ToString(dateFormat);
    }
}