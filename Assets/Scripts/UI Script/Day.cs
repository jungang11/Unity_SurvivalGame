using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day : MonoBehaviour
{
    public int day = 1;
    private float time;
    public Text dayText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if((int)time == 3)
        {
            day = 2;
        }
        if ((int)time == 6)
        {
            day = 3;
        }
        if ((int)time == 9)
        {
            day = 4;
        }
        if ((int)time == 12)
        {
            day = 5;
        }
        if ((int)time == 15)
        {
            day = 6;
        }
        dayText.text = day.ToString();
    }
}
