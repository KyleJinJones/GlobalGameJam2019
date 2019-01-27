using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float timer=0;
    public TextMeshProUGUI timetext;
    private bool stopped = false;
    // Start is called before the first frame update
    void Start()
    {
        timetext =GetComponent<TextMeshProUGUI>();
        if (timetext.text =="End")
        {
            stopped = true;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped)
        {
            timetext.text = string.Format("Your Time: {0}", timer.ToString("0.00"));
        }
        else
        {
            timer += Time.deltaTime;
            timetext.text = string.Format("Time: {0}", timer.ToString("0.00"));
        }
    }
}
