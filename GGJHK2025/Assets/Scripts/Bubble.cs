using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    bool badMemory;
    [SerializeField]
    int hP;
    float riseSpeed;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up* 5f * Time.deltaTime;
        float time = 2f;
        int hRate = 1;
        transform.position += Vector3.right * 2 * Time.deltaTime * hRate;
        if(time<=0)
        {
            hRate *= -1;
        }
    }

    private void OnMouseDown()
    {
        hP -= 1;
    }
}
