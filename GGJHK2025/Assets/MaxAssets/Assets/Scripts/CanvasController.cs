using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // for load scene

public class CanvasController : MonoBehaviour
{

    #region Variable declarations-------------------------------------------------------------------------

    public GameObject obj_canvas, obj_scrn, obj_tut;
    public Button but_start, but_tut, but_clsVid, but_skipVid, but_clsTut, but_home,btn;
    public VideoPlayer vidPlayer;
    public AudioSource audSrc;
    public AudioClip aud_menu, aud_game;
    public
    GameManager manager;
    [SerializeField]
    VideoClip[] video;
    
    SoundController soundController;
    // for tutorial //

    public Button but_hitVid, but_merVid;


    #endregion

    #region Callback functions---------------------------------------------------------------------------

    void OnClickToMenu()
    {
        obj_canvas.SetActive(true);
        manager.QuitGame();
    }
    void OnVideoFinished(VideoPlayer vp) {
        Debug.Log("Video has finished playing.");
        but_clsVid.gameObject.SetActive(true);
        obj_scrn.SetActive(false);
        obj_canvas.SetActive(false);
        
        audSrc.clip = aud_game;
        audSrc.Play();
        manager.StartGame();
    }

    void OnStartButClicked() {
        //Debug.Log("but_start is clicked .");
        vidPlayer.loopPointReached += OnVideoFinished;
        obj_scrn.SetActive(true);
        but_clsVid.gameObject.SetActive(false);
        vidPlayer.clip = video[1];
        vidPlayer.Prepare();
        vidPlayer.Play();
        audSrc.Stop();
    }
    public void PlayVideoInList(int type)
    {
        vidPlayer.clip = video[type];
        vidPlayer.Prepare();
        vidPlayer.Play();

    }
    void OnTutButClicked() {
        //Debug.Log("but_tut is clicked .");
        obj_tut.SetActive(true);
    }

    void OnVidClsButClicked() {
        Debug.Log("but_clsVid is clicked .");
        vidPlayer.Stop();
        obj_scrn.SetActive(false);
    }

    void OnVidSkipButClicked() {
        Debug.Log("but_skipVid is clicked .");
        vidPlayer.time = vidPlayer.length - 1f;
    }

    void OnHitVidButClicked() {
        Debug.Log("but_hitVid is clicked .");
        obj_scrn.SetActive(true);
        vidPlayer.clip = video[0];
        vidPlayer.Prepare();
        vidPlayer.Play();
    }

    void OnMerVidButClicked() {
        Debug.Log("but_merVid is clicked .");
        obj_scrn.SetActive(true);
        vidPlayer.Prepare();
        vidPlayer.Play();
    }

    void OnClsTutButClicked() {
        //Debug.Log("but_clsTut is clicked .");
        obj_tut.SetActive(false);
    }

    #endregion

    #region Self-defined functions-----------------------------------------------------------------------



    #endregion

    #region Setup----------------------------------------------------------------------------------------

    void Start() {
        MusicSetup();
        ButtonSetup();


    }
    
    void MusicSetup() {
        audSrc.Play();
    }

    void ButtonSetup() {
        but_start.onClick.AddListener(OnStartButClicked);
        but_tut.onClick.AddListener(OnTutButClicked);
        but_home.onClick.AddListener(OnClickToMenu);
        but_clsVid.onClick.AddListener(OnVidClsButClicked);
        but_skipVid.onClick.AddListener(OnVidSkipButClicked);

        but_hitVid.onClick.AddListener(OnHitVidButClicked);
        //but_merVid.onClick.AddListener(OnMerVidButClicked);
        but_clsTut.onClick.AddListener(OnClsTutButClicked);
    }

    #endregion

    #region Loop-----------------------------------------------------------------------------------------

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            
        }
    }

    #endregion

}
