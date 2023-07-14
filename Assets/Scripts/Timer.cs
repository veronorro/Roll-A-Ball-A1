using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float currentTime;
    bool timing;

    // Start is called before the first frame update
    public void StartTimer()
    {
        timing = true; 
    }

    // To stop the timer once the game ends
    public void StopTimer()
    {
        timing = false;
    }

    // Allows the display the current time 
    public float GetTime()
    {
        return currentTime;
    }

    // Update is called once per frame
    void Update()
    {
       if (timing == true)
        {
            currentTime += Time.deltaTime;
        }
    }
}
