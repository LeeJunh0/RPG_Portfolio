# 3D Mini RPG
만들어 놓았던 FrameWork를 바탕으로 제작한 Mini RPG형식 게임입니다.

# 개발환경
- Visual Studio 2022
- Unity 2021.3.39f1


# 프레임워크 기능구성
- Json형식 데이터 파싱 및 로드
- Addressable을 이용한 비동기적 에셋 로드
- UI 자동화
- 확장메서드 및 정적메서드 활용
- 비동기 SceneLoad
- 3D 캐릭터의 행동 및 애니메이션 제어

# 컨텐츠 기능구성
- 인벤토리
- 퀘스트

# 프레임워크 기능 설명
# 1. Json 데이터 파싱 및 로드
ILoader 인터페이스를 상속하는 클래스들을 직렬화 시켜 제네릭타입에 맞게 데이터를 로드해줍니다.
```C#
T LoadJson<T, Key, Value>(string path) where T : ILoader<Key, Value>
{
    TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);
    return JsonUtility.FromJson<T>(textAsset.text);
}
```

# 2. Addressable 에셋 비동기 로드
![AddressablePNG](https://github.com/user-attachments/assets/24ee439c-b832-40de-a69d-00abb4dc3e21)   
   
Addressable 에셋을 사용해 Scene에 필요한 에셋들만 로드해 사용합니다. 
# 3. UI 자동화
UI의 클래스 이름과 오브젝트 이름을 동일하게 하여 생성시 스크립트를 붙게 만들었으며 리플렉션을 이용해 바인딩 할수있도록 설계 했습니다.
```C#
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

