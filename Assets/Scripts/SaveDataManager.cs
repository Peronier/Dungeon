using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public Field field;
    public GameObject player;
    private SaveData saveData;
    private const string saveKey = "GameData";

    void Start()
    {
        saveData = new SaveData();
    }

    /**
     * データを保存する
     */
    private void Save()
    {
        PlayerMovement playerMove = player.GetComponent<PlayerMovement>();
        ActorSaveData playerSaveData = new ActorSaveData();
        playerSaveData.grid = new Pos2D();
        playerSaveData.grid.x = playerMove.grid.x;
        playerSaveData.grid.z = playerMove.grid.z;
        playerSaveData.direction = playerMove.direction;
        saveData.playerData = playerSaveData;
        MapSaveData mapSaveData = new MapSaveData();
        mapSaveData.map = field.GetMapData();
        saveData.mapData = mapSaveData;
        PlayerPrefs.SetString(saveKey, JsonUtility.ToJson(saveData));
    }

    /**
     * データを読み込む
     */
    private void Load()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            var data = PlayerPrefs.GetString(saveKey);
            JsonUtility.FromJsonOverwrite(data, saveData);
            field.Reset();
            field.Create(saveData.mapData.map);
            PlayerMovement playerMove = player.GetComponent<PlayerMovement>();
            playerMove.SetPosition(saveData.playerData.grid.x, saveData.playerData.grid.z);
            playerMove.SetDirection(saveData.playerData.direction);
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown("s"))
            {
                Save();
                Message.add("セーブしました！");
            }
            if (Input.GetKeyDown("l"))
            {
                Load();
                Message.add("ロードしました！");
            }
        }
    }
}
