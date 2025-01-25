using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIScript : MonoBehaviour
{
    [SerializeField]
    TMP_Text[] scoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScoreDisplay(int score,int badScore)
    {
       // scoreTxt[0].text = score.ToString();
       // scoreTxt[1].text = badScore.ToString();
    }
}
