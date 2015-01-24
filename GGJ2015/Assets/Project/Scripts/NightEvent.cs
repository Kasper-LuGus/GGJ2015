using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NightEvent : MonoBehaviour
{

    public TOD tod;
    public int hourToSleep = 18;
    public int hourToGetUp = 8;
    public int speedDay = 100;
    public int speedNight = 20;

    public Texture callToSleep;
    public Texture nightEvent;

    private List<string> callsToSleep = new List<string>();
    private List<string> nightEvents = new List<string>();

    private bool isSleeping = false;
    private string text;

    // Use this for initialization
    void Start()
    {
        callsToSleep.Add("It's " + hourToSleep + " o'clock time for dinner.");
        callsToSleep.Add("It's getting dark outside.");
        callsToSleep.Add("Come inside it's getting cold.");

        nightEvents.Add("Oh it's looks like it has rained.");
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = tod.Hour;
        if (tod.speed != 0)
        {
            if (!isSleeping)
            {
                if (currentTime >= hourToSleep)
                {
                    tod.speed = 0;
                    text = callsToSleep[Random.Range(0, callsToSleep.Count - 1)];
                }
            }
            else
            {
                if (currentTime >= hourToGetUp &&  currentTime < hourToSleep)
                {
                    tod.speed = 0;
                    text = nightEvents[Random.Range(0, nightEvents.Count - 1)];
                }
            }
        }
    }

    void OnGUI()
    {
        if (tod.speed == 0)
        {
            if (!isSleeping)
            {
                GUI.Label(new Rect(800, 800, 300, 100), new GUIContent(text, callToSleep));
                if (GUI.Button(new Rect(1100, 800, 100, 50), "Continue"))
                {
                    isSleeping = true;
                    tod.speed = speedNight;
                }
            }
            else
            {
                GUI.Label(new Rect(800, 800, 300, 100), new GUIContent(text, nightEvent));
                if (GUI.Button(new Rect(1100, 800, 100, 50), "Continue"))
                {
                    isSleeping = false;
                    tod.speed = speedDay;
                }
            }
        }
    }
}
