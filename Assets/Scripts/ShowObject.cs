using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowObject : ILevelObject
{
    private Vector3 startPos;
    public AudioClip audio;

    [Range(0f, 100f)]
    public float moveHeight;
    [Range(0f, 2f)]
    public float animeDur;

    public void Show(){
        startPos = transform.position;
        Sequence mySequence = DOTween.Sequence();
        float moveTo = transform.position.y + moveHeight;
        PlaySE();
        mySequence.Append(transform.DOLocalMoveY(moveTo, animeDur ));
        mySequence.OnComplete(() =>{gameObject.SetActive(false);}); 
    }

    public void Reset(){
        gameObject.SetActive(true);
        transform.position = startPos;
    }

    private void PlaySE(){
		AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position);
	}
}
