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
    public List<Texture> nightEvent;
    public List<string> nightEventStrings;
    public Texture textBoxImage;

    private List<string> callsToSleep = new List<string>();

    private bool isSleeping = false;
    private int index;

    // Use this for initialization
    void Start()
    {
        callsToSleep.Add("It's " + hourToSleep + " o'clock time for dinner.");
        callsToSleep.Add("It's getting dark outside.");
        callsToSleep.Add("Come inside it's getting cold.");

        tod.speed = speedDay;
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
                    index = Random.Range(0, callsToSleep.Count - 1);
                }
            }
            else
            {
                if (currentTime >= hourToGetUp &&  currentTime < hourToSleep)
                {
                    tod.speed = 0;
                    index = Random.Range(0, nightEvent.Count - 1);
                }
            }
        }
    }

    void OnGUI()
    {
        if (tod.speed == 0)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.black;
            style.fontSize = 28;
            style.wordWrap = true;
            int textBoxWidth = textBoxImage.width / 2;
            int textBoxHeight = textBoxImage.height / 2;
            GUI.DrawTexture(new Rect(Screen.width / 2 - textBoxWidth / 2, Screen.height - textBoxHeight - 10, textBoxWidth, textBoxHeight), textBoxImage);
            if (!isSleeping)
            {  
                GUI.Label(new Rect(Screen.width / 2 + 10 - textBoxWidth / 2, Screen.height - textBoxHeight, textBoxWidth - 100, textBoxHeight), new GUIContent(callsToSleep[index], callToSleep), style);
                if (GUI.Button(new Rect(Screen.width / 2 + textBoxWidth / 2 - 90, Screen.height - textBoxHeight / 2 - 25, 80, 50), "Continue"))
                {
                    isSleeping = true;
                    tod.speed = speedNight;
                }
            }
            else
            {
                GUI.Label(new Rect(Screen.width / 2 + 10 - textBoxWidth / 2, Screen.height - textBoxHeight, textBoxWidth - 100, textBoxHeight), new GUIContent(nightEventStrings[index], nightEvent[index]), style);
                if (GUI.Button(new Rect(Screen.width / 2 + textBoxWidth / 2 - 90, Screen.height - textBoxHeight/2 - 25, 80, 50), "Continue"))
                {
                    isSleeping = false;
                    tod.speed = speedDay;
                }
            }
        }
    }
}
