using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // for load scene

public class GameController : MonoBehaviour
{

    #region Variable declarations-------------------------------------------------------------------------

    public GameObject obj_canvas, but_home, but_end;
    public AudioSource audSrc;
    public AudioClip aud_menu, aud_game;


    #endregion

    #region Callback functions---------------------------------------------------------------------------



    #endregion

    #region Self-defined functions-----------------------------------------------------------------------

    bool IsCursorOnObj(GameObject obj) {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null) {
            if (hit.transform.gameObject == obj) return true;
        }

        return false;
    }

    

    public void BTTM()
    {
        obj_canvas.SetActive(true);
        if (audSrc.clip == aud_game)
        {
            audSrc.clip = aud_menu;
            audSrc.Play();
        }
    }

    #endregion

    #region Setup----------------------------------------------------------------------------------------

    void Start() {

    }

    #endregion

    #region Loop-----------------------------------------------------------------------------------------

    void Update() {
        
    }

    #endregion

}
