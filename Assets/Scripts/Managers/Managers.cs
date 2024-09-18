using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    EquipManager _equip         = new EquipManager();
    GameManager _game           = new GameManager();
    InventoryManager _inven     = new InventoryManager();
    QuestManager _quest         = new QuestManager();

    public static EquipManager Equip { get { return Instance._equip; } }
    public static GameManager Game { get { return Instance._game; } }
    public static InventoryManager Inventory { get { return Instance._inven; } }
    public static QuestManager Quest { get { return Instance._quest; } }
    #endregion

    #region Core
    DataManager _data           = new DataManager();
    InputManager _input         = new InputManager();
    OptionManager _option       = new OptionManager();
    ResourceManager _resource   = new ResourceManager();
    PoolManager _pool           = new PoolManager();
    SceneManagerEx _scene       = new SceneManagerEx();
    SoundManager _sound         = new SoundManager();
    UIManager _ui               = new UIManager();

    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static OptionManager Option { get { return Instance._option; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static SceneManagerEx EScene { get { return Instance._scene; } }
    public static SoundManager ESound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("Managers");
            if(go == null)
            {
                go = new GameObject { name = "Managers" };
                go.AddComponent<Managers>();
            }

            s_instance = go.GetComponent<Managers>();
            DontDestroyOnLoad(go);

            BindKey.Init();
            s_instance._sound.Init();
            s_instance._pool.Init();
            s_instance._quest.Init();
            s_instance._inven.Init();

        }
    }

    static public void Clear()
    {
        ESound.Clear();
        Input.Clear();
        UI.Clear();
        EScene.Clear();
        Pool.Clear();
    }
}
