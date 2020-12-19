using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal: ILevelObject
{
    protected Vector3 startPosition;
    public GameObject winPanel;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Init() {
        startPosition = transform.position;
        winPanel.SetActive(false);
    }

    public override void Reset(){
        transform.position = startPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("通關");
            hadTrigger = true;
            winPanel.SetActive(true);
        }
            
    }
}
