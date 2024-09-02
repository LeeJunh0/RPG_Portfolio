# 3D Mini RPG
만든 FrameWork를 바탕으로 제작한 Mini RPG형식 게임입니다.

# 개발환경
- Visual Studio 2022
- Unity 2021.3.39f1


# 프레임워크 기능구성
- Json형식 데이터 파싱 및 로드
- Addressable을 이용한 비동기적 에셋 로드
- UI 자동화
- 확장메서드 및 정적메서드 활용
- 비동기 SceneLoad
- Input 관리 매니저
  
# 컨텐츠 기능구성
- 카메라 제어
- 3D 캐릭터의 행동 및 애니메이션 제어
- 인벤토리
- 퀘스트

# 프레임워크 기능 설명
# 1. Json 데이터 파싱 및 로드
ILoader 인터페이스를 상속하는 클래스들을 직렬화 시켜 제네릭타입에 맞게 데이터를 로드해줍니다.
<details open>
<summary>Parsing & Load Code Open</summary>
    
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

# 2. Addressable 에셋 비동기 로드
<img src = https://github.com/user-attachments/assets/24ee439c-b832-40de-a69d-00abb4dc3e21 width = "450px" height = "500px">
   
Addressable 에셋을 사용해 Scene에 필요한 에셋들만 로드해 사용합니다. 

<details>
<summary>AsyncLoadAsset Code Open</summary>
    
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

# 3. UI 자동화
UI의 클래스 이름과 오브젝트 이름을 동일하게 하여 생성시 스크립트를 붙게 만들었으며 리플렉션을 이용해 바인딩 할수있도록 설계 했고   
바인딩 한 것을 꺼내 쓰기위한 메서드 또한 제네릭메서드로 만든후 쓰기 편하도록 타입별로 묶어 구현 했습니다.

<details>
  <summary>Bind Code Open</summary>
    
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
  <summary>Generic Code Open</summary>
    
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

# 4. Extension & Static 메서드 활용
Unity 기능들을 한 메서드에서 묶어 사용하거나, 자주 사용할 유틸적 기능들을 Static 혹은 Extend메서드로 만들어 사용에 용이하게 만들었습니다.

<details open>
    <summary>Extension Code Open</summary>
    
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
    <summary>Static Code Open</summary>
    
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

# 5. 비동기 Scene Load
Scene을 이동할때 에셋들을 불러오면서 화면에 다른것을 연출하거나 기능을 추가할수 있도록 구현 했습니다.

<details open>
    <summary>AsyncLoad Code Open</summary>
    
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

# 6. Input 관리 매니저
리스너 패턴을 사용하여 구독 해놓은 입력 이벤트 관리 및 실행을 하도록 구현 했습니다.

<details open>
<summary>InputManager Code</summary>

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

# 컨텐츠 기능설명
# 1. 카메라 제어
게임의 카메라는 3인칭 쿼터뷰 이며 플레이어와 카메라 사이에 사물이 들어올시 카메라의 위치를 조절합니다.

<details>
<summary>Camera Code Open</summary>
    
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

# 2. 3D 캐릭터의 행동 및 애니메이션 제어
State를 프로퍼티로 관리하여 Set 안에서 Switch로 구별하여 애니메이션을 동작시키고    
Update 메서드들을 오버라이드 하여 개별 관리하도록 합니다.
    
<img src = https://github.com/user-attachments/assets/cd1c64ff-4cb1-4edf-ae63-8ba8e1dc0c9a width = "1000px" height = "500px">        


<details>
<summary>Property Code Open</summary>
    
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
<summary>State Code Open</summary>
    
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

