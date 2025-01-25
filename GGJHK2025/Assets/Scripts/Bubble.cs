using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public
    bool badMemory, haveItem, blow;
    [SerializeField]
    int hP,score;
    float riseSpeed, riseRate;
    GameManager gameManager;
    int hRate = 1;
    float time = 3f,stayTime=60f;
    public bool dragged;
    private Vector3 dragOrigin, currentDrag, offset;
    public
    Transform itemPos;
    public
    int ind = 0;
    public
    GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Destroy(gameObject, stayTime);
        itemPos = transform;
        InitBubble();
        float randomAngle = Random.Range(0, 180);
        float finalAngle =
        Mathf.Clamp(randomAngle, -70, 70);
        
        transform.rotation = Quaternion.Euler(0, 0, finalAngle+Random.Range(-10,10));

    }

    // Update is called once per frame
    void Update()
    {
        if(!(Input.GetMouseButton(0)))
        {
            dragged = false;

        }
        if (!dragged)
        {
            if(gameManager.freezed)
            {
                transform.position += transform.up * 1* Time.deltaTime * 0.5f;
            }
            else
            {
                transform.position += transform.up * riseSpeed * Time.deltaTime * riseRate;
            }
            


        }
        else
        {
            return;
        }
        if(haveItem)
        {
            item.transform.rotation = Quaternion.Euler(0, 0, 0);
            //item.transform.localScale = new Vector3(transform.localScale.x * 0.6f, transform.localScale.x * 0.6f, 1);
        }

        if (transform.localScale.x<1.25)
        {
            //addScore
            if(haveItem)
            {
                item.transform.SetParent(null)
                ;
                item.AddComponent<Rigidbody2D>()
                    ;
                item.transform.Rotate(0, 0, 8);
                Destroy(item, 15f);
            }
            
            Destroy(gameObject);
        }

    }

    private void OnMouseDown()
    {
        dragged = true;
        dragOrigin = Input.mousePosition;
        offset = transform.position - MWP();

    }
    private void OnMouseUp()
    {
        currentDrag = Input.mousePosition;
        if(currentDrag==dragOrigin)
        {
            dragged = false;
        }
        
        
       
        transform.localScale -= new Vector3(0.25f, 0.25f, 0);
        

    }
    private void OnMouseDrag()
    {
        transform.position = MWP() + offset;


    }
    Vector3 MWP()
    {
        var mPos = Input.mousePosition;
        mPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mPos);
    }
    void ColorControl()
    {

    }
    void Merge(Bubble otherBubble)
    {
        if (otherBubble.ind == ind)
        {
            // Determine which bubble is larger
            bool isCurrentBubbleSmaller = transform.localScale.x < otherBubble.gameObject.transform.localScale.x;

            // Choose the position and scale based on which bubble is larger
            Vector3 mergePosition = isCurrentBubbleSmaller ? otherBubble.gameObject.transform.position : transform.position;
            //float newScale = Mathf.Sqrt(Mathf.Pow(otherBubble.gameObject.transform.localScale.x, 2) + Mathf.Pow(transform.localScale.x, 2));
            //Debug.Log($"{otherBubble.gameObject.transform.localScale}, {transform.localScale.x}, {newScale}");
            // Create the new bubble
            GameObject newB = Instantiate(gameManager.bubbles[ind], mergePosition, Quaternion.identity);
            float y=0f;
            float randomSize = Random.Range(1, 3);
            newB.gameObject.transform.localScale = new Vector3(randomSize, randomSize, 1);//size
            //transform.localScale = new Vector3(y * 1.5f, y * 1.5f, 1);
            
            // Handle item assignment
            //HandleItemAssignment(newB);

            // Destroy the merged bubbles
            Destroy(otherBubble.gameObject);
            Destroy(gameObject);
        }
    }

    private void HandleItemAssignment(GameObject newBubble)
    {
        if (haveItem)
        {
            item.transform.SetParent(newBubble.transform);
            newBubble.GetComponent<Bubble>().item = item;
            item.transform.position = newBubble.transform.position;
        }
        else
        {
            float newScale = newBubble.transform.localScale.x; // Assuming uniform scale
            if (newScale > 1.2f)
            {
                haveItem = true;
                //item = Instantiate(gameManager.items[Random.Range(0, gameManager.items.Length)]);
                item.transform.position = itemPos.position; // Ensure itemPos is defined
                item.transform.localScale = new Vector3(newBubble.transform.localScale.x * 0.6f, newBubble.transform.localScale.x * 0.6f, 1);
                item.transform.SetParent(newBubble.transform);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bubble otherBubble = collision.gameObject.GetComponent<Bubble>();
        if (otherBubble != null && dragged)
        {
            Merge(otherBubble);
        }
        if(collision==gameManager.col)
        {
            if(badMemory)
            {
                gameManager.AddGameScore(0, 1*(int)transform.localScale.x);
            }
            else
            {
                gameManager.AddGameScore(1* (int)transform.localScale.x, 0);
            }
            Destroy(gameObject);
        }
        for(int i =0;i<gameManager.borders.Length;i++)
        {
            if (collision.gameObject == gameManager.borders[i])
            {
                transform.rotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
            }
        }
        


    }
    void InitBubble()
    {
        float randomSpeed = Random.Range(0.1f, 0.5f);
        float randomSize = Random.Range(1,4);
        transform.localScale = new Vector3(randomSize, randomSize, 1);//size
        
        //iden item

        //int rand = Random.Range(0, 8);
        if(randomSize>1.4f)
        {
            haveItem = true;
            int rangeX = 0, rangeY = 0;
            if(ind<3)
            {
                rangeX = 0;
                rangeY = 2;
            }
            else
            {
                rangeX = 3;
                rangeY = 5;
            }
            item = Instantiate(gameManager.items[Random.Range(rangeX, rangeY)]);
            item.transform.position = itemPos.position;
            item.transform.localScale = new Vector3(transform.localScale.x*0.6f, transform.localScale.x*0.6f, 1);
            item.transform.SetParent(transform);
            
        }
        //set item
        
        

        //setSpeed
        riseRate = randomSpeed;
        float rSpeed = Random.Range(2, 4);
        riseSpeed = rSpeed;


    }
    
    void BubbleTypeVariants(int type)
    {
        //explosion
        Bubble[] allBubbles = FindObjectsOfType<Bubble>();
        foreach(Bubble bb in allBubbles)
        {
            if(Vector3.Distance(transform.position,bb.gameObject.transform.position)<40f)
            {
                Destroy(bb.gameObject);
                Destroy(gameObject);
            }
        }

        //freeze
        gameManager.freezed = true;
        float freezeTime = 5f;
        freezeTime -= 1 * Time.deltaTime;
        if(freezeTime<0)
        {
            freezeTime = 5f;
            gameManager.freezed = false;
        }

        //
    }


}
