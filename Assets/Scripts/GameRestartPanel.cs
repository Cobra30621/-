using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRestartPanel : MonoBehaviour
{
    private static GameRestartPanel instance;
    public Text lab_heart;
    public LevelManager _levelManager;
    public float waitStartTime = 2;
    public GameObject _panel;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        RefreshInfo();
    }

    public static void Show(){
        instance.ShowPanel();
    }

    public void ShowPanel(){
        RefreshInfo();
        _panel.SetActive(true);
        StartCoroutine(WaitClose());
    }

    IEnumerator WaitClose() {
        yield return new WaitForSeconds(waitStartTime); 
        Close();
    }

    public void Close(){
        _panel.SetActive(false);
        _levelManager.GameStart();
    }

    public void RefreshInfo(){
        int heart = _levelManager.heart;
        lab_heart.text = $"x {heart}";
    }
}
