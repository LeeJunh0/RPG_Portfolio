# 3D RPG Mini
만든 FrameWork를 바탕으로 제작한 RPG형식 게임입니다.

# 개발환경
- Visual Studio 2022
- Unity 2021.3.39f1

# 목차
- [프레임워크 기능 구성](#프레임워크-기능-구성-1)
- [컨텐츠 기능 구성](#컨텐츠-기능-구성-1)
- [만들며 느꼈던 점](#만들며-느꼈던-점)  

## 프레임워크 기능 구성
- Json형식 데이터 파싱 및 로드
- Addressable을 이용한 비동기적 에셋 로드
- UI 자동화
- 확장메서드 및 정적메서드 활용
- 비동기 SceneLoad
- Input 관리 매니저
  
## 컨텐츠 기능 구성
- 카메라 제어
- 3D 캐릭터의 행동 및 애니메이션 제어
- 인벤토리
- 퀘스트 관리
- 퀘스트 NPC

## 프레임워크 기능 구성
### 1. Json 데이터 파싱 및 로드
ILoader 인터페이스를 상속하는 클래스들을 직렬화 시켜 제네릭 타입에 맞게 데이터를 로드해 줍니다.
<details open>
<summary>데이터 파싱 및 로드 Code Open</summary>
    
```C#
T LoadJson<T, Key, Value>(string path) where T : ILoader<Key, Value>
{
    TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);
    return JsonUtility.FromJson<T>(textAsset.text);
}
```    
</details>

<details>
<summary>Full Code</summary>

```C#
public interface ILoader<Key,Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
    public Dictionary<int, QuestInfo> QuestDict { get; private set; } = new Dictionary<int, QuestInfo>();
    public Dictionary<int, Iteminfo> ItemDict { get; private set; } = new Dictionary<int, Iteminfo>();
    public Dictionary<string, DropInfo> DropDict { get; private set; } = new Dictionary<string, DropInfo>();

    public void Init()
    {
        StatDict = LoadJson<StatData, int, Data.Stat>("StatData").MakeDict();        
        QuestDict = LoadJson<QuestData, int, QuestInfo>("QuestData").MakeDict();
        ItemDict = LoadJson<ItemData, int, Iteminfo>("ItemsData").MakeDict();
        DropDict = LoadJson<DropData, string, DropInfo>("DropData").MakeDict();
    }

    T LoadJson<T, Key, Value>(string path) where T : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);
        return JsonUtility.FromJson<T>(textAsset.text);
    }
}
```
</details>

### 2. Addressable 에셋 비동기 로드   
Addressable 에셋을 사용해 해당 Scene에 필요한 에셋들만 로드해 사용합니다. 

<img src = https://github.com/user-attachments/assets/24ee439c-b832-40de-a69d-00abb4dc3e21 width = "450px" height = "500px">

<details>
<summary>비동기 로드 Code Open</summary>
    
```C#
public void LoadAllAsync<T>(string label, Action<string, int, int> callback = null) where T : Object
{
    var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
    opHandle.Completed += (op) =>
    {
        int curCount = 0;
        int totalCount = op.Result.Count;

        foreach (var result in opHandle.Result)
        {
            LoadAsync<T>(result.PrimaryKey, (obj) =>
            {
                curCount++;
                callback.Invoke(result.PrimaryKey, curCount, totalCount);
            });
        }
    };
}
```
</details>

### 3. UI 자동화
오브젝트 이름과 컴포넌트 이름을 동일하게 하여 생성 시 스크립트를 붙게 만들었으며 리플렉션을 이용해 바인딩 할 수 있도록 설계했고 바인딩 한 것을 꺼내 쓰기 위한 메서드 또한 제네릭 메서드로 만든 후 쓰기 편하도록 타입별로 묶어 구현했습니다.
<details>
  <summary>바인딩 Code Open</summary>
    
```C#
// 바인딩 및 매핑
protected void Bind<T>(Type type) where T : UnityEngine.Object
{
    string[] names = Enum.GetNames(type);
    UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
    Objects.Add(typeof(T), objects);

    for (int i = 0; i < names.Length; i++)
    {
        if (typeof(T) == typeof(GameObject))
            objects[i] = Util.FindChild(gameObject, names[i], true);
        else
            objects[i] = Util.FindChild<T>(gameObject, names[i], true);

        if (objects[i] == null)            
            Debug.Log($"Faild to bind ({names[i]})");       
    }
}
```
</details>

<details>
  <summary>제네릭 Code Open</summary>
    
```C#
// 제네릭메서드
protected T Get<T>(int idx) where T : UnityEngine.Object
{
    UnityEngine.Object[] objects = null;
    if (Objects.TryGetValue(typeof(T), out objects) == false)
        return null;

    return objects[idx] as T;
}

// 제네릭메서드를 타입별로 묶어 구현
protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
protected Text GetText(int idx) { return Get<Text>(idx); }
protected Image GetImage(int idx) { return Get<Image>(idx); }
protected Button GetButton(int idx) { return Get<Button>(idx); }
protected Slider GetSlider(int idx) { return Get<Slider>(idx); }
```
</details>

### 4. Extension & Static 메서드 활용
Unity 기능들을 한 메서드에서 묶어 사용하거나, 자주 사용할 유틸적 기능들을 Static 혹은 Extend 메서드로 만들어 사용에 용이하게 만들었습니다.

<details open>
    <summary>확장메서드 Code Open</summary>
    
```C#
public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
{       
    return Util.GetOrAddComponent<T>(go);
}

public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.EUiEvent type = Define.EUiEvent.Click)
{
    UIButton.BindEvent(go, action, type);
}
```
</details>

<details>
    <summary>정적메서드 Code Open</summary>
    
```C#
public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
{
    T Component = go.GetComponent<T>();
    if (Component == null)
    {
        Component = go.AddComponent<T>();
    }
    return Component;
}

// 해당 타입의 자식을 recursive 여부에 따라 재귀로 탐색할지 결정
public static T FindChild<T>(GameObject parent, string name = null, bool recursive = false) where T : UnityEngine.Object
{
    if (parent == null)
        return null;

    if(recursive == false)
    {
        for(int i = 0; i < parent.transform.childCount; i++)
        {
            Transform transform = parent.transform.GetChild(i);
            if (string.IsNullOrEmpty(name) || transform.name == name)
            {
                T component = transform.GetComponent<T>();
                if (component != null)
                    return component;
            }
        }
    }
    else
    {
        foreach (T component in parent.GetComponentsInChildren<T>())
        {
            if(string.IsNullOrEmpty(name) || component.name == name)            
                return component;          
        }
    }
    return null;
}
```
</details>

### 5. 비동기 Scene Load
Scene을 이동할 때 에셋들을 불러오면서 화면에 다른 것을 연출하거나 기능을 추가할 수 있도록 구현했습니다.

<details open>
    <summary>비동기 로딩 Code Open</summary>
    
 ```C#
 Managers.Resource.LoadAllAsync<Object>(label, (key, count, total) => 
 {
     Debug.Log($"Load Asset {label} : {key} {count}/{total}");
     slider.value = Mathf.Clamp(count % total, count, total);
     slider.maxValue = total;

     if(count >= total)
     {
         Debug.Log("Loading Complete");
         StartCoroutine("SceneAsyncLoad", type);
     }
 });
 ```
</details>

### 6. Input 관리 매니저
리스너 패턴을 사용하여 구독해놓은 입력 이벤트 관리 및 실행을 하도록 구현했습니다.

<details open>
<summary>Input 관리 매니저 Code Open</summary>

```C#
public class InputManager
{
    public Action<KeyCode> KeyAction = null;
    public Action<Define.EMouseEvent> MouseAction = null;
    ...
    private void OnUpdate()
    {
        if(KeyAction != null)
        {
            if(Input.GetKeyDown(BindKey.Inventory))
                KeyAction.Invoke(인벤토리);
            if(Input.GetKeyDown(BindKEy.Quest))
                KeyAction.Invoke(퀘스트);
            ...
        }
    }
}
```
</details>

## 컨텐츠 기능 구성
### 1. 카메라 제어
게임의 카메라는 3인칭 쿼터뷰이며 플레이어와 카메라 사이에 사물이 들어올 시 카메라의 위치를 조절합니다.

<details>
<summary>카메라 Code Open</summary>
    
```C#
// 카메라는 플레이어가 움직인 후에 이동 Update -> LateUpdate
void LateUpdate()
{
    if(Mode == Define.ECameraMode.QuaterView)
    {
        if (Player.IsValid() == false)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Player.transform.position, Delta, out hit, Delta.magnitude, LayerMask.GetMask("Block")))
        {
            float dis = (hit.point - Player.transform.position).magnitude * 0.8f;
            transform.position = Player.transform.position + Delta.normalized * dis;
        }
        else
        {
            transform.position = Player.transform.position + Delta;
            transform.LookAt(Player.transform);
        }
    }        
}
```
</details>

### 2. 3D 캐릭터의 행동 및 애니메이션 제어
State를 프로퍼티로 관리하여 Set 안에서 Switch로 구별하여 애니메이션을 동작시키고    
Update 메서드들을 오버라이드 하여 개별 관리하도록 합니다.
    
<img src = https://github.com/user-attachments/assets/cd1c64ff-4cb1-4edf-ae63-8ba8e1dc0c9a width = "1000px" height = "500px">        
<details>
<summary>프로퍼티 Code Open</summary>
    
```C#

protected Define.EState curState = Define.EState.Idle;

public virtual Define.EState EState
{
    get { return curState; }
    set
    {
        curState = value;
        Animator anim = GetComponent<Animator>();

        switch (curState)
        {
            case Define.EState.Die:
                break;
            case Define.EState.Idle:
                anim.CrossFade("WAIT", 0.1f);
                break;
            case Define.EState.Move:
                anim.CrossFade("MOVE", 0.1f);
                break;
            case Define.EState.Skill:
                anim.CrossFade("ATTACK", 0.1f, -1, 0);
                break;
        }
    }
}
```
</details>

<details>
<summary>스테이트 Code Open</summary>
    
```C#
// 마우스 이벤트
void OnMouseEvent(Define.EMouseEvent evt)
{
    switch (EState)
    {
        case Define.EState.Idle:
            OnMouseEvent_IdelRun(evt);
            break;
        case Define.EState.Move:
            OnMouseEvent_IdelRun(evt);
            break;
        case Define.EState.Die:
            break;
        case Define.EState.Skill:
            if (evt == Define.EMouseEvent.PointerUp)
                stopSkill = true;
            break;
    }
}  
```   
</details>

### 3. 인벤토리
기본적인 아이템 추가, 삭제, 정렬기능을 구현했고 아이템 데이터를 관리하는 Manager 와 화면에 나타날 UI를 관리할 UI_Inven으로 구분 지어 커플링을 줄이며 디버깅에 용이하도록 설계했습니다.

<details open> 
<summary>인벤토리 매니저 Code Open</summary>

```C#
public class InventoryManager
{
    public class ItemComparer : IComparer<Iteminfo>
    {
        public int Compare(Iteminfo x, Iteminfo y)
        {...}
    }
    ItemComparer comparer = new ItemComparer();

    public void Init() {...}
    public void UpdateSlotInfo(int index) {...}
    public void UpdateAllSlot() {...}
    public void AddItem(Iteminfo item) {...}
    public void RemoveItem(int index) {...}
    public void TrimAll() {...}
    public void SortAll() {...}
    public void SetInvenReference(UI_Inven_Item[] icons) {...}
    public void InterLocking() {...}
    public int GetItemCount(string target) {...}
}

```
<details>
  <summary>FullCode Open</summary>

```C#
public class InventoryManager
{
    Iteminfo[] invenInfos;
    UI_Inven_Item[] invenIcons;

    public int SelectIndex { get; set; }
    public Iteminfo[] InvenInfos    { get { return invenInfos; } private set { } }

    // Array.Sort를 쓰기위한 IComparer를 구현
    public class ItemComparer : IComparer<Iteminfo>
    {
        public int Compare(Iteminfo x, Iteminfo y)
        {
            if (x == null) 
                return 1;
            else if (y == null) 
                return -1;
            else if (x == null || y == null)
                return 0;
            else 
                return x.id.CompareTo(y.id);
        }      
    }

    ItemComparer comparer = new ItemComparer();

    // 가상 인벤토리 초기화
    public void Init()
    {
        invenInfos = new Iteminfo[Managers.Option.InventoryCount];

        for (int i = 0; i < invenInfos.Length; i++)
            invenInfos[i] = null;
    }

    // 아이콘 업데이트
    public void UpdateSlotInfo(int index)
    {
        if (invenIcons == null)
            return;

        invenIcons[index].SetInfo(invenInfos[index]);       
    }

    // 아이콘 전체 업데이트
    public void UpdateAllSlot()
    {
        for (int i = 0; i < invenInfos.Length; i++)
            UpdateSlotInfo(i);
    }

    // 스택 아이템 여부에 따른 추가 및 퀘스트 확인
    public void AddItem(Iteminfo item)
    {
        if (invenInfos.Length <= 0 || item == null)
            return;
        
        if (item.uiInfo.isStack == true)
        {
            for (int i = 0; i < invenInfos.Length; i++)
            {
                if (invenInfos[i] == null)
                    continue;

                if (invenInfos[i].id == item.id)
                {
                    invenInfos[i].MyStack++;
                    Managers.Quest.OnGetQuestAction?.Invoke(item.uiInfo.name, GetItemCount(item.uiInfo.name));
                    UpdateSlotInfo(i);
                    return;
                }
            }
        }

        for (int i = 0; i < invenInfos.Length; i++)
        {
            if (invenInfos[i] != null)
                continue;

            invenInfos[i] = item;
            Managers.Quest.OnGetQuestAction?.Invoke(item.uiInfo.name, GetItemCount(item.uiInfo.name));
            UpdateSlotInfo(i);
            break;
        }
    }

    public void RemoveItem(int index)
    {
        if (invenInfos.Length <= 0)
            return;

        invenInfos[index] = null;
        UpdateSlotInfo(index);
    }

    // 인벤토리 공백 제거
    public void TrimAll()
    {
        if (invenInfos.Length <= 0)
            return;

        // 가장 앞 빈칸을 찾는 while 문
        int voidSearch = -1;
        while (++voidSearch < invenInfos.Length && invenInfos[voidSearch] != null)
        {
            if (voidSearch >= invenInfos.Length)
            {
                Debug.Log("빈칸이 존재하지 않아 공백제거 불가.");
                return;
            }
        }

        int itemSearch = voidSearch;
        while (true)
        {
            while (++itemSearch < invenInfos.Length && invenInfos[itemSearch] == null) ;

            if (itemSearch >= invenInfos.Length)
                break;

            invenInfos[voidSearch] = invenInfos[itemSearch];
            invenInfos[itemSearch] = null;
            voidSearch++;
        }
        UpdateAllSlot();
    }

    // 인벤토리 정렬
    public void SortAll()
    {
        TrimAll();

        // IComperer를 쓴 정렬
        Array.Sort(invenInfos, comparer);
        UpdateAllSlot();
    }

    // 아이콘 List를 받아옴
    public void SetInvenReference(UI_Inven_Item[] icons)
    {
        invenIcons = icons;
    }

    // 가상의 인벤토리와 아이콘 List를 연결해줌
    public void InterLocking()
    {
        if (invenIcons == null)
            return;

        UpdateAllSlot();  
    }

    public int GetItemCount(string target)
    {
        if (target == "")
            return -1;

        int curCount = 0;
        for(int i = 0; i < invenInfos.Length; i++)
        {
            if (invenInfos[i] == null)
                continue;

            curCount = invenInfos[i].uiInfo.name == target ? curCount + invenInfos[i].MyStack : curCount;
        }                  
        return curCount;
    }
}

```
</details>
    
</details>

<details>
<summary>인벤토리 UI Code Open</summary>

```C#
public class UI_Inven : UIScene
{
    enum GameObjects
    {
        Sorting,
        Create,
        UI_Inven_Popup
    }

    GameObject InvenUI;
    UI_Inven_Item[] iconInfos;
    GameObject popup;
    int curIndex = int.MaxValue;

    public override void Init() {...}
    public void InfosInit() {...}
    public void OnInvenPopup(int index) {...}
```

<details>
<summary>FullCode Open</summary>
  
```C#
public class UI_Inven : UIScene
{
    enum GameObjects
    {
        Sorting,
        Create,
        UI_Inven_Popup
    }

    GameObject InvenUI;
    UI_Inven_Item[] iconInfos;
    GameObject popup;
    int curIndex = int.MaxValue;
    
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        InvenUI = Util.FindChild(this.gameObject, "Content", true);
        popup = GetObject((int)GameObjects.UI_Inven_Popup);

        // 버튼이벤트 할당
        GetObject((int)GameObjects.Sorting).BindEvent((evt) =>
        {
            Debug.Log("정렬버튼 on");
            Managers.Inventory.SortAll();
        });
        GetObject((int)GameObjects.Create).BindEvent((evt) =>
        {
            int random = Random.Range(102, 106);
            Managers.Inventory.AddItem(new Iteminfo(Managers.Data.ItemDict[random]));
        });
        InfosInit();
        popup.SetActive(false);
    }

    private void Start()
    {
        // 인벤토리 매니저에 IconList 전달
        Managers.Inventory.SetInvenReference(iconInfos);
        Managers.Inventory.InterLocking();
    }

    public void InfosInit()
    {
        iconInfos = new UI_Inven_Item[Managers.Option.InventoryCount];

        for(int i = 0; i < iconInfos.Length; i++)
        {
            iconInfos[i] = InvenUI.transform.GetChild(i).GetOrAddComponent<UI_Inven_Item>();
            iconInfos[i].SetIndex(i);
        }
    }

    // 인벤 팝업창 On Off 및 팝업창 Init수행
    public void OnInvenPopup(int index)
    {
        Iteminfo iteminfo = Managers.Inventory.InvenInfos[index];
        popup.GetComponent<UI_Inven_Popup>().InvenPopupInit(iteminfo);

        if (curIndex != index)
        {
            curIndex = index;
            return;
        }

        curIndex = int.MaxValue;
        popup.SetActive(false);
    }
}

```
</details>
</details>

### 4. 퀘스트 관리
Action을 이용한 리스너 패턴식 퀘스트 상태 관리 와 조건 업데이트를 구현했고 NPC들이 사용할 기능들을 이벤트로써 관리하도록 구현했습니다.

<details open>
<summary>퀘스트 매니저 Code Open</summary>

```C#
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class QuestManager
{
    List<Quest> activeQuests = new List<Quest>();
    public IReadOnlyList<Quest> ActiveQuests => activeQuests;

    // 상태 관리 이벤트들
    public Action<QuestInfo> OnStartQuest = null;
    public Action<Quest> OnCompletedQuest = null;
    public Action<Quest> OnRewardsQuest = null;

    // 조건 업데이트 이벤트들
    public Action<string> OnKillQuestAction = null;
    public Action<string, int> OnGetQuestAction = null;
    public Action<int> OnLevelQuestAction = null;

    public Action<int> OnCurrentUpdate = null;
    public void Init() {...}

    // 추가 삭제 완료 메서드들
    public void AddQuest(QuestInfo questInfo) {...}
    public void RemoveQuest(Quest quest) {...}
    public void CompleteQuest(Quest quest) {...}

    // 조건 이벤트들의 메서드들
    public void UpdateKill(string target) {...}
    public void UpdateGet(string target, int count) {...}
    public void UpdateLevel(int curLevel) {...}
}
```

<details>
<summary>FullCode Open</summary>

```C#
public class QuestManager
{
    List<Quest> activeQuests = new List<Quest>();

    // 
    public Action<QuestInfo> OnStartQuest = null;
    public Action<Quest> OnCompletedQuest = null;
    public Action<Quest> OnRewardsQuest = null;

    public Action<string> OnKillQuestAction = null;
    public Action<string, int> OnGetQuestAction = null;
    public Action<int> OnLevelQuestAction = null;

    public Action<int> OnCurrentUpdate = null;

    public IReadOnlyList<Quest> ActiveQuests => activeQuests;

    public void Init()
    {
        OnStartQuest -= AddQuest;
        OnStartQuest += AddQuest;
        OnCompletedQuest -= CompleteQuest;
        OnCompletedQuest += CompleteQuest;

        OnKillQuestAction -= UpdateKill;
        OnKillQuestAction += UpdateKill;
        OnGetQuestAction -= UpdateGet;
        OnGetQuestAction += UpdateGet;
        OnLevelQuestAction -= UpdateLevel;
        OnLevelQuestAction += UpdateLevel;
    }

    public void AddQuest(QuestInfo questInfo) 
    {     
        Quest quest = new Quest(questInfo);

        if(activeQuests.Count <= 0)
            activeQuests.Add(quest);
        else
        {
            bool check = true;
            for (int i = 0; i < activeQuests.Count; i++)
            {
                if (activeQuests[i].QuestName == quest.QuestName)
                {
                    check = false;
                    break;
                }    
            }

            if (check == true)
                activeQuests.Add(quest);
        }

        if (quest.Task.Type == (int)Define.EQuestEvent.Level)
            OnLevelQuestAction?.Invoke(Managers.Game.GetPlayer().GetComponent<PlayerStat>().Level);

        if (quest.Task.Type == (int)Define.EQuestEvent.Get)
            OnGetQuestAction?.Invoke(quest.Task.Target, Managers.Inventory.GetItemCount(quest.Task.Target));
    }

    public void RemoveQuest(Quest quest) 
    { 
        activeQuests.Remove(quest); 
    }

    public void CompleteQuest(Quest quest)
    {
        RemoveQuest(quest);

        foreach(string name in quest.Rewards.items)
        {
            for(int i = 0; i < Managers.Data.ItemDict.Count - 1; i++)
            {
                if (Managers.Data.ItemDict[102 + i].uiInfo.name == name)
                {
                    Managers.Inventory.AddItem(new Iteminfo(Managers.Data.ItemDict[102 + i]));
                }
            }
        }
            
    }

    public void UpdateKill(string target)
    {
        if (activeQuests.Count <= 0)
            return;

        for(int i = 0; i < ActiveQuests.Count; i++)
            ActiveQuests[i].Task.Counting(target);      
    }

    public void UpdateGet(string target, int count)
    {
        if (activeQuests.Count <= 0)
            return;

        for (int i = 0; i < ActiveQuests.Count; i++)
            ActiveQuests[i].Task.CountSet(target, count);
    }

    public void UpdateLevel(int curLevel)
    {
        if (activeQuests.Count <= 0)
            return;

        for (int i = 0; i < ActiveQuests.Count; i++)
            ActiveQuests[i].Task.CountSet("Level", curLevel);
    }
}

```
</details>
</details>

<details>
<summary>퀘스트 UI Code Open</summary>

```C#
public class UI_Quest : UIPopup
{
    enum GameObjects
    {
        QuestList,
        UI_Quest_Popup
    }

    GameObject list;
    GameObject popup;
    int questId = int.MaxValue;

    public override void Init() {...}
    void QuestListInit() {...}
    public void OnQuestPopup(int id) {...}
    private void Update() {...}
}

```

<details>
<summary>FullCode Open</summary>

```C#
public class UI_Quest : UIPopup
{
    enum GameObjects
    {
        QuestList,
        UI_Quest_Popup
    }

    GameObject list;
    GameObject popup;
    int questId = int.MaxValue;

    
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Debug.Log("UI_Quest Binding");

        base.Init();
        list = GetObject((int)GameObjects.QuestList);
        popup = GetObject((int)GameObjects.UI_Quest_Popup);

        QuestListInit();
        popup.SetActive(false);
    }

    // 아이콘 List Update
    void QuestListInit()
    {
        for (int i = list.transform.childCount; i < Managers.Quest.ActiveQuests.Count; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Quest_Item>(parent: list.transform).gameObject;
            UI_Quest_Item questItem = item.GetOrAddComponent<UI_Quest_Item>();
            questItem.SetInfo(Managers.Quest.ActiveQuests[i].QuestName, i);
            item.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    // 퀘스트 팝업창 On Off 및 팝업창 Init
    public void OnQuestPopup(int id)
    {
        popup.GetOrAddComponent<UI_Quest_Popup>().QuestPopupInit(id);
        if (id != questId)
        {
            questId = id;
            return;
        }

        questId = int.MaxValue;
        popup.SetActive(false);
    }

    private void Update()
    {
        QuestListInit();
    }
}

```
</details>
</details>

### 5. 퀘스트 NPC
퀘스트 매니저의 기능들을 가져와 UI 적으로 플레이어에게 제공하도록 구현했습니다.

<details open>
<summary>퀘스트 NPC Code Open</summary>

```C#
public class QuestGiver : MonoBehaviour
{
    List<QuestInfo> quests = new List<QuestInfo>();
    public IReadOnlyList<QuestInfo> Quests => quests;

    // 현재 가능한 퀘스트 검색
    public void Search() {...}
    // 현재 NPC의 퀘스트팝업창 On
    public void GiverUIOpen() {...}
}

```

<details>
<summary>FullCode Open</summary>

```C#
public class QuestGiver : MonoBehaviour
{
    List<QuestInfo> quests = new List<QuestInfo>();
    public IReadOnlyList<QuestInfo> Quests => quests;

    private void Awake()
    {
        Search();
    }

    public void Search()
    {
        for (int i = 0; i < Managers.Data.QuestDict.Count; i++)
            quests.Add(new QuestInfo(Managers.Data.QuestDict[i]));
    }

    public void GiverUIOpen()
    {
        if (Util.FindChild<UI_Giver>(Managers.UI.Root, "UI_Giver") != null)        
            return;
        
        UI_Giver ui = Managers.UI.ShowPopupUI<UI_Giver>();

        ui.UIListInit(quests);
    }
}

```
</details>  
</details>

<details>
<summary>UI Code Open</summary>

```C#
public class UI_Giver : UIPopup
{
    enum GiverObject
    {
        PossibleList,
        GiverQuest_Popup,
        UI_Giver_ExitButton
    }

    // 현재 NPC의 퀘스트목록
    List<QuestInfo> ownerQuests = new List<QuestInfo>();
    GameObject popup;
    GameObject list;

    
    public override void Init() {...}
    public void ListUp() {...}
    public void UIListInit(List<QuestInfo> quests) {...}
    public void OnGiverPopup(QuestInfo quest) {...}
}

```

<details>
<summary>FullCode Open</summary>

```C#
public class UI_Giver : UIPopup
{
    enum GiverObject
    {
        PossibleList,
        GiverQuest_Popup,
        UI_Giver_ExitButton
    }

    List<QuestInfo> ownerQuests = new List<QuestInfo>();
    GameObject popup;
    GameObject list;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GiverObject));
        popup = GetObject((int)GiverObject.GiverQuest_Popup);
        list = GetObject((int)GiverObject.PossibleList);
        
        popup.SetActive(false);

        GetObject((int)GiverObject.UI_Giver_ExitButton).BindEvent((evt) =>
        {
            Managers.UI.ClosePopupUI();
        });
    }

    // 퀘스트 아이콘들 초기화
    public void ListUp()
    {
        if (ownerQuests == null) 
            return;

        for(int i = 0; i < ownerQuests.Count; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Giver_Item>(parent: list.transform).gameObject;
            UI_Giver_Item giverItem = item.GetOrAddComponent<UI_Giver_Item>();
            giverItem.SetInfo(ownerQuests[i]);
        }
    }

    // 현재 NPC에 퀘스트 목록을 불러와 아이콘화 시킴
    public void UIListInit(List<QuestInfo> quests) 
    { 
        ownerQuests = quests;
        ListUp();
    }

    // 해당 퀘스트로 팝업창을 Init후 On 
    public void OnGiverPopup(QuestInfo quest) 
    {
        popup.GetOrAddComponent<GiverQuest_Popup>().SetInfo(quest);
        popup.GetOrAddComponent<GiverQuest_Popup>().GiverPopupInit();
    }
}

```
</details>
</details>

## 만들며 느꼈던 점
### 신경썼던 점
강의를 보며 만든 프레임워크를 최대한 이해하고 내것으로 만들기위해 프레임워크를 바탕으로 기능들을 만들었고 최대한 독립성을 유지하도록 구현했습니다.  

### 아쉬운 점
퀘스트 구현에서 데드라인에 쫓겨 다소 조잡한 부분이있어 다음 RPG 나 퀘스트구현하는 프로젝트에선 좀더 시간배분을 하여 만들꺼 같습니다.

긴 글 읽어 주셔서 감사합니다.
이준호였습니다.
