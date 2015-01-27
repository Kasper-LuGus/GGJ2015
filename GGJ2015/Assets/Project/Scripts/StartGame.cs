using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    public MouseLook xLook;
    public MouseLook yLook;
    public Canvas menuCanvas;
    public TOD tod;

    void Awake()
    {
       // PlayerStateManager.use.onStateChanged += OnStateChanged;
    }

//    void OnStateChanged(PlayerStateManager.PlayerState state)
//    {
//        if (state == PlayerStateManager.PlayerState.Menu)
//        {
//            xLook.enabled = false;
//            yLook.enabled = false;
//            menuCanvas.enabled = true;
//            tod.enabled = false;
//        }
//        else
//        {
//            xLook.enabled = true;
//            yLook.enabled = true;
//            menuCanvas.enabled = false;
//            tod.enabled = true;
//        }
//    }
    // Use this for initialization
    public void StartClick()
    {
      //  PlayerStateManager.use.state = PlayerStateManager.PlayerState.Free;

		Application.LoadLevel("GameScene");

    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
