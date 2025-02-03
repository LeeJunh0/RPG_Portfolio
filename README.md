# 3D RPG Mini
Unity3D의 RPG형식 게임입니다.

# 개발환경
- Visual Studio 2022
- Unity 2022.3.21f1

# 목차
- [컨텐츠 기능 구성](#컨텐츠-기능-구성-1)
- [만들며 느꼈던 점](#만들며-느꼈던-점)  

## 컨텐츠 기능 구성
- 스킬, 범위표시
- 스킬파츠 3가지
    - 속성(Elemental)
    - 구성
    - 움직임

- 인벤토리
- 장비창
- 스킬창
- 퀘스트 관리
- 퀘스트 NPC
- 상인 NPC
- 보스 패턴
- 각종 작은기능들
  
## 컨텐츠 기능 구성

### 1. 스킬 및 범위표시
|발사체공격|범위공격|
|------|---|
|<img src="https://github.com/user-attachments/assets/a7d0b8e7-e71f-428a-9529-bf3646c31eb9" width= "300">|<img src="https://github.com/user-attachments/assets/5cded845-937b-4092-afd2-a4873488da2c" width="300">|

### 2. 보스스킬 및 범위표시
|보스|범위공격|
|--|--|
|<img src="https://github.com/user-attachments/assets/d35fd5c2-7408-457c-b556-6a7d662e2e7f" width="300">|<img src="https://github.com/user-attachments/assets/a060a02d-d315-4494-899d-72a8c18d59a9" width="300">|

### 3. 인벤토리와 세부기능
|슬롯드래그|아이템 툴팁|
|--|--|
|<img src="https://github.com/user-attachments/assets/c5e6f977-4856-4500-a39b-fa141bd44eea" width="400">|<img src="https://github.com/user-attachments/assets/020a2837-262b-4e02-adae-7f52ad637c87" width="400">|

|아이템 정렬|아이템 사용|
|--|--|
|<img src="https://github.com/user-attachments/assets/4fe937c3-705e-4776-9c17-8f205e3c948e" width="400">|<img src="https://github.com/user-attachments/assets/5fd1930c-f106-44eb-8369-9ef80ace78c7" width="400">|

### 4. 장비창과 세부기능
|장착확인|장착해제|
|--|--|
|<img src="https://github.com/user-attachments/assets/44301a29-95e6-4f47-96a2-e4b3d1d86167" width="400">|<img src="https://github.com/user-attachments/assets/4c13870a-70bb-469f-a795-d978b9b2afef" width="400">|

### 5. 스킬창과 세부기능
|슬롯하이라이트|스킬툴팁|스킬탭|
|--|--|--|
|<img src="https://github.com/user-attachments/assets/75cb6cba-46c8-49ce-b275-fe1f569718c4" width="300">|<img src="https://github.com/user-attachments/assets/57d90e18-55b6-479c-8fbe-f77f5e545dc8" width="300">|<img src="https://github.com/user-attachments/assets/afb70c76-f09c-4706-9a0d-1af5715e2766" width="300">|

### 6. 퀘스트창 과 세부기능
|퀘스트리스트 및 수락|수락퀘스트 확인|
|--|--|
|<img src="https://github.com/user-attachments/assets/2a7b9fd8-1c01-4249-88a5-d93e2eda38df" width="400">|<img src="https://github.com/user-attachments/assets/3e3a16a8-303d-4f11-9a63-c44c11107fa6" width="400">|

### 7. 상점과 세부기능
|아이템구매|아이템판매|
|--|--|
|<img src="https://github.com/user-attachments/assets/ad6dc11b-bff7-405f-a06d-154510d06bd8" width="400">|<img src="https://github.com/user-attachments/assets/e3aee9ab-2a65-4188-b800-3a7e699718aa" width="400">|



### 1. 스킬
베이스가 되는 스킬과 파츠로 나뉘어진 기능들이 합쳐져 만들어지는 스킬을 구현하였습니다.

<details Open>
<summary>스킬 인벤토리 Code Open</summary>

```C#
using Data;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class SkillInventory : MonoBehaviour
{
    public Dictionary<SkillInfo, MotifyInfo[]> skillMotifies;
    public List<SkillInfo> skillInven;
    public List<SkillInfo> mySkills;
    
    private BaseController owner;
    private bool duplicateCheck = false;

    private void Start()
    {
        skillMotifies = new Dictionary<SkillInfo, MotifyInfo[]>();
        InitSkills();
    }

    private void Update()
    {
        if (Input.GetKeyDown(BindKey.SkillSlot_1) && duplicateCheck == false)
            StartCoroutine(SkillActivation(mySkills[0]));
        if (Input.GetKeyDown(BindKey.SkillSlot_2) && duplicateCheck == false)
            StartCoroutine(SkillActivation(mySkills[1]));
    }

    public void InitSkills() {...}
    public void AddSkill(SkillInfo skillInfo) {...}
    public void RemoveSkill(SkillInfo skillInfo) {...}
    public void AddMotify(MotifyInfo info) {...}
    public void RemoveByNameMotify(SkillInfo skill, MotifyInfo info)  {...}
    public void RemoveByTypeMotify(SkillInfo skill, MotifyInfo info) {...}
    private bool CoolTimeCheck(SkillInfo skill) {...}
    private IEnumerator PlayerSetIndicator(SkillInfo skill) {...}
    public void RangeIndicatorSet(Indicator indicator) {...}
    public void SkillExecute(SkillInfo skill, Vector3 pos) {...}
    public float SkillManaSum(SkillInfo skill) {...}
    public bool SkillManaCheck(float mana) {...}
    public void PlayerManaUse(float mana) {...}
    private IEnumerator SkillActivation(SkillInfo skill) {...}
    private void OnDisable() {...}
}
```
</details>

<details>
<summary> 스킬 인벤토리 Full Open </summary>

```C#
using Data;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class SkillInventory : MonoBehaviour
{
    public Dictionary<SkillInfo, MotifyInfo[]> skillMotifies;
    public List<SkillInfo> skillInven;
    public List<SkillInfo> mySkills;
    
    private BaseController owner;
    private bool duplicateCheck = false;

    private void Start()
    {
        skillMotifies = new Dictionary<SkillInfo, MotifyInfo[]>();
        InitSkills();
    }

    private void Update()
    {
        if (Input.GetKeyDown(BindKey.SkillSlot_1) && duplicateCheck == false)
            StartCoroutine(SkillActivation(mySkills[0]));
        if (Input.GetKeyDown(BindKey.SkillSlot_2) && duplicateCheck == false)
            StartCoroutine(SkillActivation(mySkills[1]));
    }

    public void InitSkills()
    {
        owner = GetComponent<BaseController>();
        Stat ownerStat = owner.GetComponent<Stat>();

        foreach (SkillInfo skillinfo in Managers.Data.SkillDict.Values)
        {
            if (skillinfo.useObject == owner.WorldObjectType)
            {
                skillInven.Add(skillinfo);

                if (ownerStat.Level >= skillinfo.level)
                {
                    AddSkill(skillinfo);
                    skillMotifies.Add(skillinfo, new MotifyInfo[3]);
                }
            }
        }
    }

    public void AddSkill(SkillInfo skillInfo) { mySkills.Add(skillInfo); }
    public void RemoveSkill(SkillInfo skillInfo) { mySkills.Remove(skillInfo); }

    public void AddMotify(MotifyInfo info)
    {
        SkillInfo skill = skillMotifies.FirstOrDefault(x => x.Key.type == Managers.Skill.curMainSkill).Key;

        if (skill == null)
            return;

        if (Managers.Skill.MotifyNameEqule(skillMotifies[skill], info) == true)
        {
            RemoveByNameMotify(skill, info);
            Debug.Log("중복파츠입니다.");
            return;
        }

        if (Managers.Skill.MotifyTypeEqule(skillMotifies[skill], info) == true)       
            RemoveByTypeMotify(skill, info);

        switch (info.type)
        {
            case EMotifyType.Initialize:
                skillMotifies[skill][0] = info;
                break;
            case EMotifyType.Embodiment:
                skillMotifies[skill][1] = info;
                break;
            case EMotifyType.Movement:
                skillMotifies[skill][2] = info;
                break;
        }
    }

    public void RemoveByNameMotify(SkillInfo skill, MotifyInfo info) 
    {
        for (int i = 0; i < skillMotifies[skill].Length; i++)
        {
            if (skillMotifies[skill][i].NameEquals(info) == true)
            {
                skillMotifies[skill][i] = null;
                break;
            }  
        }
    }

    public void RemoveByTypeMotify(SkillInfo skill, MotifyInfo info)
    {
        for (int i = 0; i < skillMotifies[skill].Length; i++)
        {
            if (skillMotifies[skill][i].TypeEquals(info) == true)
            {
                skillMotifies[skill][i] = null;
                break;
            }
        }
    }

    private bool CoolTimeCheck(SkillInfo skill) { return skill.isActive; }

    private IEnumerator PlayerSetIndicator(SkillInfo skill)
    {
        float manaSum = SkillManaSum(skill);
        bool manaCheck = SkillManaCheck(manaSum);

        if (manaCheck == false)
            yield break;

        GameObject prefab = Managers.Skill.SetIndicator(skill.indicator);
        Indicator indicator = prefab.GetComponent<Indicator>();
        indicator.SetInfo(skill.indicator, skill.length, skill.radius);

        if (skill.indicator == EIndicator.CircleIndicator)
            RangeIndicatorSet(indicator);

        bool isCancel = false;
        while (Input.GetKey(BindKey.SkillSlot_1) || Input.GetKey(BindKey.SkillSlot_2))
        {
            if (Input.GetMouseButtonDown(1))
            { 
                isCancel = true;
                break;
            }
            
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(indicator.transform.position).z; 
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            indicator.UpdatePosition(worldPos);

            yield return null;
        }
        
        if (isCancel == false)        
            SkillExecute(skill, indicator.transform.position);

        Managers.Resource.Destroy(indicator.gameObject); 
        duplicateCheck = false;
    }

    public void RangeIndicatorSet(Indicator indicator)
    {
        GameObject range = Managers.Skill.SetIndicator(EIndicator.RangeIndicator);
        CircleIndicator circle = indicator.GetComponent<CircleIndicator>();
        circle.SetRange(range);
    }

    public void SkillExecute(SkillInfo skill, Vector3 pos)
    {
        GameObject skillPrefab = new GameObject(name: "SkillObject");
        Managers.Resource.AddComponentByName(skill.name, skillPrefab);
        Skill instanceSkill = skillPrefab.GetComponent<Skill>();
        instanceSkill.motifies = skillMotifies[skill].ToArray();
        instanceSkill.targetPos = pos;
        instanceSkill.Execute();

        float mana = SkillManaSum(skill);
        PlayerManaUse(mana);
    }

    public float SkillManaSum(SkillInfo skill)
    {
        float usingMana = skill.mana;
        for (int i = 0; i < skillMotifies[skill].Length; i++)
        {
            if (skillMotifies[skill][i] == null)
                continue;

            usingMana += skillMotifies[skill][i].mana;
        }

        return usingMana;
    }

    public bool SkillManaCheck(float mana)
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        if (playerStat.Mp < mana)
            return false;

        return true;
    }

    public void PlayerManaUse(float mana)
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        playerStat.Mp -= mana;
    }

    private IEnumerator SkillActivation(SkillInfo skill)
    {
        if (CoolTimeCheck(skill) == false)
            yield break;

        duplicateCheck = true;
        StartCoroutine(PlayerSetIndicator(skill));
        
        skill.isActive = false;
        yield return new WaitForSeconds(skill.coolTime);
        skill.isActive = true;
    }

    private void OnDisable()
    {
        if (owner.WorldObjectType != EWorldObject.Monster)
            return;

        StopAllCoroutines();
    }
}
```
</details>

<details>
<summary> 스킬 클래스 Full Code Open </summary>

```C#
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    void Execute(Skill _skill);
}

public class Skill : MonoBehaviour
{
    public SkillInfo skillData;
    public Vector3 targetPos;

    public List<GameObject> objects = new List<GameObject>();
    public MotifyInfo[]     motifies = new MotifyInfo[3];
    public GameObject       prefab;

    public Motify           initialize;
    public Motify           embodiment;
    public Motify           movement;

    public virtual void Execute() 
    {
        SetMotifies();
        StartCoroutine(DestroySkill());

        initialize.Execute(this);
        embodiment.Execute(this);
        movement.Execute(this);
    }

    public void SetMotifies()
    {
        foreach (MotifyInfo info in motifies)
        {
            if (info == null)
                continue;

            Motify motify = Managers.Skill.SetMotify(info.skillName);

            if (motify is InitializeMotify)
                SetInitializeMotify(motify);
            else if(motify is EmbodimentMotify)
                SetEmbodimentMotify(motify);
            else
                SetMovementMotify(motify);
        }

        if (initialize == null) { SetInitializeMotify(new InitializeMotify()); }
        if (embodiment == null) { SetEmbodimentMotify(new EmbodimentMotify()); }
        if (movement == null) { SetMovementMotify(new MoveMotify()); }
    }

    public void SetInitializeMotify(Motify motify)    { initialize = motify; }
    public void SetEmbodimentMotify(Motify motify)    { embodiment = motify; }
    public void SetMovementMotify(Motify motify)      { movement = motify; }

    protected IEnumerator DestroySkill()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    protected virtual IEnumerator OnDamaged()
    {
        int layer = 1 << (int)Define.ELayer.Monster;

        yield return new WaitForSeconds(0.5f);

        Collider[] monsters = Physics.OverlapSphere(targetPos, skillData.radius / 2, layer);
    }

    private void OnDestroy()
    {
        movement.StopRun();
        StopCoroutine(OnDamaged());
    }
}
```

</details>

<details>
<summary> 발사체 스킬 Full Code Open </summary>
    
```C#
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static Define;

public class ProjectileSkill : Skill
{
    public GameObject       hitVFX;
    public GameObject       muzzleVFX;
    public int              count = 1;

    Rigidbody rigid;
    
    private void Awake()
    {       
        rigid = Util.GetOrAddComponent<Rigidbody>(gameObject);
        skillData = new SkillInfo(Managers.Data.SkillDict["ProjectileSkill"]);        
    }

    public override void Execute()
    {
        DefaultShoot();
        base.Execute();
    }

    private void DefaultShoot()
    {
        transform.position = new Vector3(Managers.Game.GetPlayer().transform.position.x, 1f, Managers.Game.GetPlayer().transform.position.z);       
        transform.forward = GetDirection();
        rigid.AddForce(transform.forward * 20f, ForceMode.Impulse);
    }

    public Vector3 GetDirection() 
    {
        Vector3 direction = (targetPos - Managers.Game.GetPlayer().transform.position).normalized;
        direction.y = 0f;

        return direction;
    }
}
```
</details>

<details>
<summary> 범위 지정스킬 Full Code Open</summary>

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : Skill
{
    private void Awake()
    {
        skillData = new Data.SkillInfo(Managers.Data.SkillDict["ExplosionSkill"]);
    }
    public override void Execute()
    {
        base.Execute();
    }
}
```
</details>

### 2. 스킬 범위표시
플레이어와 보스에 스킬타입마다 각각 다른 스킬범위표시를 플레이어에게 제공합니다.



### 3. 스킬 구성 3가지
#### - 속성(Elemental)
스킬의 속성마다 고유한 시각적 효과와 기능들을 가질수 있도록 구현하였습니다.

<details Open>
<summary> 속성 Code </summary>

```C#
public class InitializeMotify : Motify
{
    public ESkill_Elemental    elemental = ESkill_Elemental.None;

    // 스킬속성에 맞는 프리팹의 이름을 저장
    protected Dictionary<ESkill_Elemental, string> projectileDict = new Dictionary<ESkill_Elemental, string>() {...}
    protected Dictionary<ESkill_Elemental, string> hitVFXDict = new Dictionary<ESkill_Elemental, string>() {...}
    protected Dictionary<ESkill_Elemental, string> muzzleVFXDict = new Dictionary<ESkill_Elemental, string>() {...}
    protected Dictionary<ESkill_Elemental, string> areaDict = new Dictionary<ESkill_Elemental, string>() {...}

    public virtual void SetElemental() {...}
    public override void Execute(Skill _skill) {...}
}
```
</details>

<details>
<summary> 속성 Full Code Open </summary>

```C#
public class InitializeMotify : Motify
{
    public ESkill_Elemental    elemental = ESkill_Elemental.None;

    protected Dictionary<ESkill_Elemental, string> projectileDict = new Dictionary<ESkill_Elemental, string>()
    {
        { ESkill_Elemental.None,   "NormalProjectile" },
        { ESkill_Elemental.Fire,   "FireProjectile" },
        { ESkill_Elemental.Ice,    "IceProjectile" },
        { ESkill_Elemental.Wind,  "WindProjectile" }
    };
    protected Dictionary<ESkill_Elemental, string> hitVFXDict = new Dictionary<ESkill_Elemental, string>()
    {
        { ESkill_Elemental.None,   "NormalHit" },
        { ESkill_Elemental.Fire,   "FireHit" },
        { ESkill_Elemental.Ice,    "IceHit" },
        { ESkill_Elemental.Wind,  "WindHit" }
    };
    protected Dictionary<ESkill_Elemental, string> muzzleVFXDict = new Dictionary<ESkill_Elemental, string>()
    {
        { ESkill_Elemental.None,   "NormalMuzzle" },
        { ESkill_Elemental.Fire,   "FireMuzzle" },
        { ESkill_Elemental.Ice,    "IceMuzzle" },
        { ESkill_Elemental.Wind,  "WindMuzzle" }
    };
    protected Dictionary<ESkill_Elemental, string> areaDict = new Dictionary<ESkill_Elemental, string>()
    {
        { ESkill_Elemental.None, "NormalArea" },
        { ESkill_Elemental.Fire, "FireArea" },
        { ESkill_Elemental.Ice,"IceArea" },
        { ESkill_Elemental.Wind,"WindArea" }
    };
    public virtual void SetElemental()
    {
        switch (skill.skillData.type)
        {
            case ESkill.Projectile:
                {
                    ProjectileSkill projectileSkill = skill as ProjectileSkill;
                    projectileSkill.prefab = Managers.Resource.Load<GameObject>(projectileDict[elemental]);
                    projectileSkill.hitVFX = Managers.Resource.Load<GameObject>(hitVFXDict[elemental]);
                    projectileSkill.muzzleVFX = Managers.Resource.Load<GameObject>(muzzleVFXDict[elemental]);
                }
                break;
            case ESkill.AreaOfEffect:
                {
                    ExplosionSkill explosionSkill = skill as ExplosionSkill;
                    explosionSkill.prefab = Managers.Resource.Load<GameObject>(areaDict[elemental]);
                }
                break;
            default:
                break;
        }
    }

    public override void Execute(Skill _skill)
    {
        base.Execute(_skill);

        SetElemental();
    }
}

```   
</details>

#### - 구성
스킬의 생성 갯수 혹은 생성패턴을 바꾸도록 구현했습니다.

<details Open>
<summary> 구성 Code </summary>

```C#
public class EmbodimentMotify : Motify
{
    protected int count = 1;

    public virtual void Embodiment(Transform pos) {...}
    public void AreaEmbodiment(Transform pos) {...}
    public void ProjectileEmbodiment(Transform pos) {...}
    public override void Execute(Skill _skill) {...}
}
```
</details>

<details>
<summary> 구성 Full Code Open </summary>

```C#
public class EmbodimentMotify : Motify
{
    protected int count = 1;

    public virtual void Embodiment(Transform pos) 
    {
        switch (skill.skillData.type)
        {
            case Define.ESkill.Projectile:
                ProjectileEmbodiment(pos);
                break;
            case Define.ESkill.AreaOfEffect:
                AreaEmbodiment(pos);
                break;
            default:
                break;
        }
    }

    public void AreaEmbodiment(Transform pos)
    {
        for (int i = 0; i < count; i++)
        {
            ExplosionSkill explosionSkill = skill as ExplosionSkill;
            GameObject go = Managers.Resource.Instantiate(explosionSkill.prefab);
            go.transform.position = skill.targetPos;

            go.transform.SetParent(skill.transform);
            objects.Add(go);
        }
    }

    public void ProjectileEmbodiment(Transform pos)
    {
        for (int i = 0; i < count; i++)
        {
            ProjectileSkill projectileSkill = skill as ProjectileSkill;
            GameObject go = Managers.Resource.Instantiate(projectileSkill.prefab);
            go.transform.position = pos.position;
            go.transform.forward = projectileSkill.GetDirection();

            Projectile projectile = go.GetComponent<Projectile>();
            projectile.hitVFX = projectileSkill.hitVFX;
            projectile.muzzleVFX = projectileSkill.muzzleVFX;

            go.transform.SetParent(skill.transform);
            objects.Add(go);
        }
    }

    public override void Execute(Skill _skill)
    {
        base.Execute(_skill);

        Embodiment(skill.transform);
    }
    public override void SetMana() { }
}
```
</details>

#### - 움직임
스킬들의 움직임을 바꾸도록 구현했습니다.

<details Open>
<summary> 움직임 Code </summary>

```C#
public class MoveMotify : Motify
{
    protected float speed = 40f;

    public virtual IEnumerator Movement() {...}
    public override void Execute(Skill _skill) {...}
    public override void StopRun() {...}
    public override void SetMana()  {...}
}
```
</details>

<details>
<summary>움직임 Full Code Open </summary>

```C#
using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class MoveMotify : Motify
{
    protected float speed = 40f;

    public virtual IEnumerator Movement()
    {
        yield return null;
        for (int i = 0; i < objects.Count; i++)
        {
            Rigidbody rigid = objects[i].GetComponent<Rigidbody>();
            rigid.AddForce(objects[i].transform.forward * speed, ForceMode.Impulse);
        }
    }

    public override void Execute(Skill _skill) 
    { 
        base.Execute(_skill);

        // 스킬마다 다른 기능사용
        switch (skill.skillData.type)
        {
            case ESkill.Projectile:
                CoroutineRunner.Instance.StartCoroutine(Movement());
                break;
            default:
                break;
        }
    }

    // 사용한 코루틴중지
    public override void StopRun() { CoroutineRunner.Instance.StopRunCoroutine(Movement(), GetType().Name); }
    public override void SetMana()  { skill.skillData.mana += 10; }
}
```
</details>

### 4. 인벤토리
기본적인 아이템 추가, 삭제, 정렬기능을 구현했고 데이터와 UI를 분리하여 보다 유연하게 기능들이 작동할수 있도록 구현하였습니다.

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
    public bool RemoveItem(Iteminfo item) {...}
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
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager
{
    private UsingItem[] invenInfos;
    private UI_Inven_Slot[] invenIcons;

    public int SelectIndex { get; set; }
    public Item[] InvenInfos { get { return invenInfos; } private set { } }
    public UI_Inven_Slot[] InvenIcons { get { return invenIcons; } private set { } }

    public class ItemComparer : IComparer<UsingItem>
    {
        public int Compare(UsingItem x, UsingItem y)
        {
            if (x.ItemInfo == null) 
                return 1;
            else if (y.ItemInfo == null) 
                return -1;
            else if (x.ItemInfo == null || y.ItemInfo == null)
                return 0;
            else 
                return x.ItemInfo.id.CompareTo(y.ItemInfo.id);
        }      
    }

    ItemComparer comparer = new ItemComparer();

    public void Init()
    {
        invenInfos = new UsingItem[Managers.Option.InventoryCount];

        for (int i = 0; i < invenInfos.Length; i++)
            invenInfos[i] = new UsingItem();        
    }

    public void OnInventory()
    {
        if (Input.GetKeyDown(BindKey.Inventory))
            Managers.UI.OnGameUIPopup<UI_Inven>();
    }

    public void UpdateSlotInfo(int index)
    {
        if (Util.FindChild<UI_Inven>(Managers.UI.Root) == false)
            return;

        invenIcons[index].SetInfo(invenInfos[index].ItemInfo);       
    }

    public void UpdateAllSlot()
    {
        for (int i = 0; i < invenInfos.Length; i++)
            UpdateSlotInfo(i);
    }

    public void AddItem(Iteminfo item)
    {
        if (invenInfos.Length <= 0 || item == null)
            return;
        
        if (item.isStack == true)
        {
            for (int i = 0; i < invenInfos.Length; i++)
            {
                if (invenInfos[i].ItemInfo == null)
                    continue;

                if (invenInfos[i].ItemInfo.id == item.id)
                {
                    invenInfos[i].ItemInfo.curStack++; 
                    UpdateSlotInfo(i);
                    Managers.Quest.OnGetQuestAction?.Invoke(item.GetItemName(), GetItemCount(item.GetItemName()));
                    return;
                }
            }
        }

        for (int i = 0; i < invenInfos.Length; i++)
        {
            if (invenInfos[i].ItemInfo != null)
                continue;

            invenInfos[i].SetItem(item);
            UpdateSlotInfo(i);
            Managers.Quest.OnGetQuestAction?.Invoke(item.uiInfo.name, GetItemCount(item.uiInfo.name));
            break;
        }
    }

    public void RemoveItem(int index)
    {
        if (invenInfos.Length <= 0)
            return;

        invenInfos[index].SetItem(null);
        UpdateSlotInfo(index);
    }

    public bool RemoveItem(Iteminfo item)
    {
        for (int i = 0; i < invenInfos.Length; i++)
        {
            if (invenInfos[i].ItemInfo == null)
                continue;

            if (invenInfos[i].ItemInfo.Equals(item) == true)
            {
                if (invenInfos[i].ItemInfo.isStack == true)
                {
                    invenInfos[i].ItemInfo.curStack -= 1;
                    UpdateSlotInfo(i);

                    if (invenInfos[i].ItemInfo.curStack <= 0)
                    {
                        invenInfos[i].SetItem(null);
                        UpdateSlotInfo(i);
                        TrimAll();
                    }
                }
                else
                {
                    invenInfos[i].SetItem(null);
                    UpdateSlotInfo(i);
                    TrimAll();                
                }

                return true;
            }
        }

        return false;
    }

    public void ChangeItem(int item1, int item2)
    {
        Iteminfo item = invenInfos[item1].ItemInfo;
        invenInfos[item1].SetItem(invenInfos[item2].ItemInfo);
        invenInfos[item2].SetItem(item);
    }

    public void TrimAll()
    {
        if (invenInfos.Length <= 0)
            return;

        int voidSearch = -1;
        while (++voidSearch < invenInfos.Length && invenInfos[voidSearch].ItemInfo != null)
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
            while (++itemSearch < invenInfos.Length && invenInfos[itemSearch].ItemInfo == null) ;

            if (itemSearch >= invenInfos.Length)
                break;

            invenInfos[voidSearch].SetItem(invenInfos[itemSearch].ItemInfo);
            invenInfos[itemSearch].SetItem(null);
            voidSearch++;
        }
        UpdateAllSlot();
    }

    public void SortAll()
    {
        TrimAll();
        Array.Sort(invenInfos, comparer);
        UpdateAllSlot();
    }

    public void SetInvenReference(UI_Inven_Slot[] icons) { invenIcons = icons; }

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
            if (invenInfos[i].ItemInfo == null)
                continue;
            else
            {
                curCount = invenInfos[i].ItemInfo.uiInfo.name == target ? curCount + invenInfos[i].ItemInfo.curStack : curCount;
                break;
            }
        }                  
        return curCount;
    }

    public void InfoLink(int item1, int item2)
    {
        ChangeItem(item1, item2);
        UpdateSlotInfo(item1);
        UpdateSlotInfo(item2);
    }
}
```
</details>
    
</details>

<details Open>
<summary> 인벤토리 UI Code </summary>

```C#
using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven : UIPopup
{
    enum GameObjects
    {
        UI_Inven_Sorting,
        UI_Sliver_Text,
        Content
    }

    UI_Inven_Slot[] iconInfos;
    Text sliverText;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        sliverText = GetObject((int)GameObjects.UI_Sliver_Text).GetComponent<Text>();

        GetObject((int)GameObjects.UI_Inven_Sorting).BindEvent((evt) => { Managers.Inventory.SortAll(); });
        InfosInit();
    }

    private void Start()
    {
        Managers.Inventory.SetInvenReference(iconInfos);
        Managers.Inventory.InterLocking();
    }

    private void Update()
    {
        // 실시간 화폐 데이터 연동
        SetSliverText();     
    }

    public void InfosInit() {...}
    public void SetSliverText() {...}
}
```

<details>
<summary>FullCode Open</summary>
  
```C#
using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven : UIPopup
{
    enum GameObjects
    {
        UI_Inven_Sorting,
        UI_Sliver_Text,
        Content
    }

    UI_Inven_Slot[] iconInfos;
    Text sliverText;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        sliverText = GetObject((int)GameObjects.UI_Sliver_Text).GetComponent<Text>();

        GetObject((int)GameObjects.UI_Inven_Sorting).BindEvent((evt) => { Managers.Inventory.SortAll(); });
        InfosInit();
    }

    private void Start()
    {
        Managers.Inventory.SetInvenReference(iconInfos);
        Managers.Inventory.InterLocking();
    }

    private void Update()
    {
        SetSliverText();     
    }

    public void InfosInit()
    {
        iconInfos = new UI_Inven_Slot[Managers.Option.InventoryCount];

        foreach (Transform child in GetObject((int)GameObjects.Content).transform)
            Managers.Resource.Destroy(child.gameObject);

        for(int i = 0; i < iconInfos.Length; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI_Inven_Slot");
            item.transform.SetParent(GetObject((int)GameObjects.Content).transform);
            item.GetComponent<RectTransform>().localScale = Vector3.one;
            iconInfos[i] = item.GetOrAddComponent<UI_Inven_Slot>();
            iconInfos[i].SetIndex(i);           
        }
    }

    public void SetSliverText() 
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        int curSliver = playerStat.Gold;
 
        sliverText.text = string.Format("{0:#,0}", curSliver);
    }
} 
```
</details>

### 5. 퀘스트 관리
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
</details>

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
    private void QuestListInit() {...}
    public void OnQuestPopup(int id) {...}
    private void Update() {...}
}

```
</details>

<details>
<summary>FullCode Open</summary>

```C#
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        base.Init();

        BindObject(typeof(GameObjects));
        Debug.Log("UI_Quest Binding");

        list = GetObject((int)GameObjects.QuestList);
        popup = GetObject((int)GameObjects.UI_Quest_Popup);

        QuestListInit();
        popup.SetActive(false);
    }

    private void QuestListInit()
    {
        for (int i = list.transform.childCount; i < Managers.Quest.ActiveQuests.Count; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Quest_Item>(parent: list.transform).gameObject;
            UI_Quest_Item questItem = item.GetOrAddComponent<UI_Quest_Item>();
            questItem.SetInfo(Managers.Quest.ActiveQuests[i].QuestName, i);
            item.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

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

### 6. 퀘스트 NPC
퀘스트 매니저의 기능들을 가져와 UI 적으로 플레이어에게 제공하도록 구현했습니다.

<details open>
<summary>퀘스트 NPC Code Open</summary>

```C#
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NpcController
{
    List<QuestInfo> quests = new List<QuestInfo>();
    public List<QuestInfo> Quests => quests;

    public override void Init() {...}
    public void Search() {...} // 가능한 퀘스트들 리스트업 후 저장
    public void OnTypeUI() {...} // 퀘스트리스트 UI 오픈
}
```
</details>

<details>
<summary>FullCode Open</summary>

```C#
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NpcController
{
    List<QuestInfo> quests = new List<QuestInfo>();
    public List<QuestInfo> Quests => quests;

    public override void Init()
    {
        npcType = Define.ENpc.Giver;
        base.Init();

        Search();
    }

    public void Search()
    {
        for (int i = 0; i < Managers.Data.QuestDict.Count; i++)
            quests.Add(new QuestInfo(Managers.Data.QuestDict[i]));
    }

    public void OnTypeUI()
    {
        if (Util.FindChild<UI_Giver>(Managers.UI.Root, "UI_Giver") != null)
            return;

        UI_Giver ui = Managers.UI.ShowPopupUI<UI_Giver>();
        ui.UIListInit(quests);
    }
}
```
</details>  

<details>
<summary>UI Code Open</summary>

```C#
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public override void Init() {...}
    public void ListUp() {...}
    public void UIListInit(List<QuestInfo> quests) {...}
    public void OnGiverPopup(QuestInfo quest) {...}
}
```
</details>

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

### 7. 상인 NPC
아이템리스트를 띄워주는 UI발사대로 UI에 파싱된 데이터를 가져와 유저에게 제공하고 Buy, Sell기능도 UI에서 제공하도록 합니다.    

<details Open>
<summary> 상인NPC Code </summary>
    
```C#
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderController : NpcController
{
    public override void Init()
    {
        npcType = Define.ENpc.Trader;
        base.Init();
    }

    // 상점 UI 오픈과 함께 인벤토리도 오픈
    public void OnTraderUI()
    {
        if (Util.FindChild<UI_Trader>(Managers.UI.Root, "UI_Trader") != null)
            return;

        Managers.UI.ShowPopupUI<UI_Trader>();
        Managers.UI.OnGameUIPopup<UI_Inven>();
    }
}
```
</details>

<details>
<summary> 상점UI FullCode Open </summary>
    
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Trader : UIPopup
{
    enum GameObjects
    {
        UI_Trader_ItemList,
        UI_Trader_ExitButton
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        GetObject((int)GameObjects.UI_Trader_ExitButton).BindEvent(evt =>
        {
            Managers.UI.ClosePopupUI();
        });

        ItemListCreate();
    }

    // 파싱된 아이템데이터를 받아와 아이템슬롯을 만들어 해당 슬롯에 아이템 데이터를 입력해줌
    public void ItemListCreate()
    {
        GameObject list = GetObject((int)GameObjects.UI_Trader_ItemList);
        for (int i = 0; i < Managers.Data.ItemDict.Count - 1; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Trader_Item>(parent: list.transform).gameObject;
            UI_Trader_Item traderItem = item.GetOrAddComponent<UI_Trader_Item>();
            item.transform.localScale = Vector3.one;

            traderItem.ItemInit(Managers.Data.ItemDict[102 + i]);
        }
    }
}
```
</details>

<details Open>
<summary> 상점 아이템슬롯 Code </summary>

```C#
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Trader_Item : UIPopup
{
    enum ItemButtons
    {
        UI_Trader_Sell,
        UI_Trader_Buy
    }

    enum ItemImage
    {
        UI_Trader_Item_Slot
    }

    enum ItemTexts
    {
        UI_Trader_Item_Name,
        UI_Trader_Item_Sell
    }

    public override void Init() {...}
    public void ItemInit(Iteminfo info) {...}
    public void ItemBuy(Iteminfo item) {...}
    public void ItemSell(Iteminfo item) {...}
}
```    
</details>
<details>
<summary> 상점 아이템슬롯 FullCode Open </summary>

```C#
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Trader_Item : UIPopup
{
    enum ItemButtons
    {
        UI_Trader_Sell,
        UI_Trader_Buy
    }

    enum ItemImage
    {
        UI_Trader_Item_Slot
    }

    enum ItemTexts
    {
        UI_Trader_Item_Name,
        UI_Trader_Item_Sell
    }

    public override void Init()
    {
        BindButton(typeof(ItemButtons));
        BindImage(typeof(ItemImage));
        BindText(typeof(ItemTexts));
    }

    public void ItemInit(Iteminfo info)
    {
        Image slot = GetImage((int)ItemImage.UI_Trader_Item_Slot);
        Texture2D texture = Managers.Resource.Load<Texture2D>(info.uiInfo.icon);
        slot.sprite = Managers.UI.TextureToSprite(texture);

        GetText((int)ItemTexts.UI_Trader_Item_Name).text = info.uiInfo.name;
        GetText((int)ItemTexts.UI_Trader_Item_Sell).text = string.Format("${0}", info.gold);

        GetButton((int)ItemButtons.UI_Trader_Buy).gameObject.BindEvent(evt => { ItemBuy(info); });
        GetButton((int)ItemButtons.UI_Trader_Sell).gameObject.BindEvent(evt => { ItemSell(info); });
    }

    public void ItemBuy(Iteminfo item)
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();

        if(playerStat.Gold - item.gold < 0)
        {
            Debug.Log("화폐 부족");
            return;
        }

        playerStat.Gold -= item.gold;
        Managers.Inventory.AddItem(new Iteminfo(item));
        Debug.Log($"{item.uiInfo.name} 구매성공");
    }

    public void ItemSell(Iteminfo item)
    {
        bool isCheck = Managers.Inventory.RemoveItem(item);
        if (isCheck == false)
        {
            Debug.Log("아이템이 없는데 팔려고 시도함");
            return;
        }

        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        playerStat.Gold += item.gold;
        Debug.Log($"{item.uiInfo.name} 판매 {item.gold} +");
    }
}
``` 
</details>


## 만들며 느꼈던 점
### 신경썼던 점
구현할 컨텐츠가 많은 RPG인 만큼 최대한 많은걸 구현하며 버그를 줄이기 위해 노력했습니다.

### 아쉬운 점
많은 기능들에 많은버그들로 시간이 많이걸렸고 버그를 잡는과정에서 깨달은것이 많아 아쉽긴해도 다음 RPG요소가 있는 게임을 만들때 참고할 점들을 많이 챙겼습니다.

긴 글 읽어 주셔서 감사합니다.
이준호였습니다.
