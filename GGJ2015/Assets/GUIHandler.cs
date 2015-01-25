using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour {
    public Texture2D texture;
    public GameObject panel;
	// Use this for initialization
    void OnGUI()
    {
       
        Debug.Log("Type panel: " + ((RectTransform) panel.transform).rect.yMin);

        GUI.skin.button.normal.background = texture;
        GUI.skin.button.hover.background = texture;
        GUI.skin.button.active.background = texture;
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        Vector2 size = style.CalcSize(new GUIContent("Start"));
        float multiplier = Mathf.Max(size.x / texture.width, size.y / texture.height);
        Rect rect = new Rect(Screen.width / 2 - size.x / 2, Mathf.Abs(((RectTransform)panel.transform).rect.yMin), texture.width * multiplier, texture.height * multiplier);
        Debug.Log("rect: " + rect);
        if (GUI.Button(rect, new GUIContent("Start"), style)) {
            Debug.Log("Button Pressed");
        }
    }
 
}
