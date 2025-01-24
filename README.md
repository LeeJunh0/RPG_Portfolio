# 3D RPG Mini
만든 FrameWork를 바탕으로 제작한 RPG형식 게임입니다.

# 개발환경
- Visual Studio 2022
- Unity 2021.3.39f1

# 목차
- [컨텐츠 기능 구성](#컨텐츠-기능-구성-1)
- [만들며 느꼈던 점](#만들며-느꼈던-점)  

## 컨텐츠 기능 구성
- 스킬
- 인벤토리
- 퀘스트 관리
- 퀘스트 NPC

## 컨텐츠 기능 구성

### 1. 스킬

### 2. 인벤토리
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

### 3. 퀘스트 관리
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

### 4. 퀘스트 NPC
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
스킬설계와 인벤토리의 기능, 퀘스트 조건을 탐색하는 부분들 인것같습니다.

### 아쉬운 점
퀘스트 구현에서 데드라인에 쫓겨 다소 조잡한 부분이있어 다음 RPG 나 퀘스트구현하는 프로젝트에선 좀더 시간배분을 하여 만들꺼 같습니다.

긴 글 읽어 주셔서 감사합니다.
이준호였습니다.
