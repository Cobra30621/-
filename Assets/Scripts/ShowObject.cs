using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowObject : ILevelObject
{
    private Vector3 startPos = new Vector3(0,0,1);
    public AudioClip audio;

    [Range(0f, 100f)]
    public float moveHeight;
    [Range(0f, 2f)]
    public float animeDur;

    public void Show(){
        Sequence mySequence = DOTween.Sequence();
        float moveTo = transform.localPosition.y + moveHeight;
        PlaySE();
        mySequence.Append(transform.DOLocalMoveY(moveTo, animeDur ));
        mySequence.OnComplete(() =>{gameObject.SetActive(false);}); 
    }

    public void Reset(){
        transform.localPosition = startPos;
    }

    private void PlaySE(){
		AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position);
	}
}
