using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MapName{Level1, Level2};
public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public int heart;
    private GameObject _player;
    private PlayerController _playerController;
    private Vector3 _playerStartPos;
    // Start is called before the first frame update
    // List<IItem>
    public List<ILevelObject> levelObjects = new List<ILevelObject>();

    // 地圖切換
    public MapName _nowMap; // 地圖場景狀態
    public GameObject[] Cameras;
    public GameObject mainCamera;

    public GameObject startPanel;
    public bool gameStart;

    public AudioSource BGMPlayer;


    void Start()
    {
        instance = this;
        gameStart = false;

        heart = 2;
        GameObject[] GOs = GameObject.FindGameObjectsWithTag("LevelObject");
        foreach(GameObject go in GOs){
            levelObjects.Add(go.GetComponent<ILevelObject>());
        }
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKey(KeyCode.Return) && gameStart == false)
        {
            gameStart = true;
            startPanel.SetActive(false);
            GameRestartPanel.Show();
            ResetLevelObject();
            ResetPlayer();
            // _playerController.SetGravity(true);
        }
            
    }

    public void Init(){
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _playerStartPos = _player.transform.position;
        _playerController.SetGravity(false); // 開啟介面用
        ResetLevelObject();
        // FindCamera();
        // SetCamera(_nowMap);
        startPanel.SetActive(true);
    }

    public void PlayerDead(){ 
        heart --;
        GameRestartPanel.Show();
        ResetLevelObject();
        ResetPlayer();
        
    }

    public static void StopBGM(){
        instance.BGMPlayer.Stop();
    }


    public void GameStart(){
        _playerController.SetEanbleControl();
        BGMPlayer.Play();
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

    private void FindCamera(){
        Cameras = GameObject.FindGameObjectsWithTag("VSCamera"); // 找到所有的Camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public static void ChangeScene(Vector3 vec , MapName map){
        instance.ChangeSceneOAO(vec, map);
    }

    public void ChangeSceneOAO(Vector3 vec, MapName map){
        SetCamera(map); // 設置攝影機
        SetPlayerPos(vec); // 轉移玩家位置
    }

    // 設置攝影機
    private void SetCamera(MapName map){
        _nowMap = map;

        foreach(GameObject camera in Cameras){
            camera.SetActive(false);
            if(camera.GetComponent<ICamera>()._mapName == map)
            {
                camera.SetActive(true);
                Debug.Log($"打開攝影機{map}");
            }
        }
    }

    public void SetPlayerPos(Vector3 pos){    
        _player.transform.position = pos;
    }
}
