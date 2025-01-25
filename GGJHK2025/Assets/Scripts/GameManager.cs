using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints,itemPos;
    public
    GameObject[] bubbles,items,borders;//halfGoogHalfBad
    float cloudScale=1,nextSpawn = 1,spt;
    public Transform mediumYAxis;
    public int Score,badScore,finalScore;
    public
    Collider2D col;
    float parameter;
    UIScript uI;
    GameObject[] itemList;
    [SerializeField]
    GameObject cursorHammer;

    [SerializeField]
    Animator anim;

    public bool freezed;
    bool start, end;
    // Start is called before the first frame update
    void Start()
    {
        
        itemList = new GameObject[items.Length];
        for(int i = 0;i<items.Length;i++)
        {
            GameObject it = Instantiate(items[i], itemPos[i].position, Quaternion.identity);
            itemList[i] = it;
            it.SetActive(false);
        }
        uI = GetComponent<UIScript>();
        //RandomSpawn();
        spt = nextSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            spt -= 1 * Time.deltaTime;
            if (spt < 0)
            {
                RandomSpawn();
                nextSpawn = Random.Range(0.5f, 0.8f);
                spt = nextSpawn;
            }
            uI.ScoreDisplay(Score, badScore);
            EndGameCal();
        }
        else
        {
            Bubble[] allBubbles = FindObjectsOfType<Bubble>();
            foreach (Bubble b in allBubbles)
            {
                Destroy(b.gameObject);
            }
        }
        
        
    }
    void RandomSpawn()
    {
        int type = Random.Range(0, bubbles.Length - 1);
        int hole = Random.Range(0, spawnPoints.Length - 1);
        GameObject bubble = Instantiate(bubbles[type], spawnPoints[hole].position, spawnPoints[hole].rotation);
    }
    public void AddGameScore(int scoreValue,int badScoreValuse)
    {
        Score += scoreValue;
        badScore += badScoreValuse;
    }
    void EndGameCal()
    {
        int result = Score - badScore;
        float normalizedValue = (30 + result) / 60f;
        normalizedValue = Mathf.Clamp01(normalizedValue);
        col.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(normalizedValue, normalizedValue, normalizedValue, 1);
        switch (result)
        {

            case 10:
                itemList[0].SetActive(true);
                break;
            case 20:
                itemList[1].SetActive(true);
                break;
            case 30:
                itemList[2].SetActive(true);
                anim.SetTrigger("End");
                start = false;
                
                break;
            case -10:
                itemList[3].SetActive(true);
                break;
            case -20:
                itemList[4].SetActive(true);
                break;
            case -30:
                itemList[5].SetActive(true);
                start = false;
                anim.SetTrigger("End");

                break;

        }
    }
    public void QuitGame()
    {
        start = false;
        Bubble[] allBubbles = FindObjectsOfType<Bubble>();
        foreach(Bubble b in allBubbles)
        {
            Destroy(b.gameObject);
        }
        
        
    }
    IEnumerator Hammer(float time)
    {
        yield return new WaitForSeconds(time);
        cursorHammer.SetActive(false);
    }
    public void ScoringEffect(int addScore)
    {
        cursorHammer.SetActive(true);
        cursorHammer.GetComponent<TMPro.TMP_Text>().text = addScore.ToString();

        // Get the click position in screen coordinates
        Vector2 clickPosition = Input.mousePosition;

        // Convert the screen position to world position if needed
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(clickPosition.x, clickPosition.y, 10f));
        cursorHammer.transform.position = worldPosition;
        StartCoroutine(Hammer(0.5f));
    }
    public void StartGame()
    {
        anim.SetBool("Start",true);
        StartCoroutine(WaitToBeginAndEnd());
    }
    IEnumerator WaitToBeginAndEnd()
    {
        yield return new WaitForSeconds(8f);
        anim.SetBool("Start", false);
        start = true;
    }
}
