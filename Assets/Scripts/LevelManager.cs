using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int heart;
    private GameObject _player;
    private PlayerController _playerController;
    private Vector3 _playerStartPos;
    // Start is called before the first frame update
    // List<IItem>
    public List<ILevelObject> levelObjects = new List<ILevelObject>();
    void Start()
    {
        heart = 2;
        GameObject[] GOs = GameObject.FindGameObjectsWithTag("LevelObject");
        foreach(GameObject go in GOs){
            levelObjects.Add(go.GetComponent<ILevelObject>());
        }
        Init();
    }

    public void Init(){
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _playerStartPos = _player.transform.position;
        ResetLevelObject();
    }

    public void PlayerDead(){ 
        heart --;
        GameRestartPanel.Show();
        ResetLevelObject();
        ResetPlayer();
        
    }

    public void GameStart(){
        _playerController.SetEanbleControl();
    }

    private void ResetPlayer(){
        Debug.Log("重置玩家");
        _playerController.Reset();
        _player.transform.position = _playerStartPos;
    }

    public void ResetLevelObject(){
        Debug.Log("重置關卡物件");
        foreach(ILevelObject lob in levelObjects){
            lob.Reset();
        }
    }
}
