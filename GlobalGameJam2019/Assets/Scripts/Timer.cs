using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float timer=0;
    public TextMeshProUGUI timetext;
    // Start is called before the first frame update
    void Start()
    {
        timetext =GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timetext.text = string.Format("Time: {0}", timer.ToString("0.00"));   
    }
}
