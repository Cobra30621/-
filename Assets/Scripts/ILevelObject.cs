using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILevelObject : MonoBehaviour
{
    public bool hadTrigger;

    public virtual void Reset(){
        Debug.Log("我重置拉");
        hadTrigger = false;
    }

    public virtual void OnTrigger(){
        
    }
}