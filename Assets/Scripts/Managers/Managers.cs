using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    EquipManager equip = new EquipManager();
    GameManager game = new GameManager();
    InventoryManager inven = new InventoryManager();
    QuestManager quest = new QuestManager();
    SkillManager skill = new SkillManager();

    public static EquipManager Equip { get { return Instance.equip; } }
    public static GameManager Game { get { return Instance.game; } }
    public static InventoryManager Inventory { get { return Instance.inven; } }
    public static QuestManager Quest { get { return Instance.quest; } }
    public static SkillManager Skill { get { return Instance.skill; } }
    #endregion

    #region Core
    DataManager data = new DataManager();
    InputManager input = new InputManager();
    OptionManager option = new OptionManager();
    ResourceManager resource = new ResourceManager();
    PoolManager pool = new PoolManager();
    SceneManagerEx scene = new SceneManagerEx();
    SoundManager sound = new SoundManager();
    UIManager ui = new UIManager();

    public static DataManager Data { get { return Instance.data; } }
    public static InputManager Input { get { return Instance.input; } }
    public static OptionManager Option { get { return Instance.option; } }
    public static ResourceManager Resource { get { return Instance.resource; } }
    public static PoolManager Pool { get { return Instance.pool; } }
    public static SceneManagerEx EScene { get { return Instance.scene; } }
    public static SoundManager Sound { get { return Instance.sound; } }
    public static UIManager UI { get { return Instance.ui; } }
    #endregion

    void Update()
    {
        input.OnUpdate();
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
            s_instance.sound.Init();
            s_instance.pool.Init();
            s_instance.quest.Init();
            s_instance.inven.Init();
        }
    }

    static public void Clear()
    {
        Input.Clear();
        UI.Clear();
        EScene.Clear();
        Pool.Clear();
    }
}
