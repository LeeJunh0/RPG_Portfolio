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

- 보스 패턴
- 인벤토리
- 장비창
- 스킬창
- 퀘스트 관리
- 퀘스트 NPC
- 상인 NPC
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



### 1. 스킬 및 파츠들
베이스가 되는 스킬과 파츠로 나뉘어진 기능들이 합쳐져 만들어지는 스킬을 구현하였습니다.

<details Open>
<summary> 스킬 매니저 Code </summary>

```C#
public class SkillManager
{
    public SkillInventory playerInventory;
    public Define.ESkill curMainSkill = Define.ESkill.Projectile;

    public void GetSkillInventory() { playerInventory = Managers.Game.GetPlayer().GetComponent<SkillInventory>(); }
    public Motify SetMotify(string name) {...}
    public GameObject SetIndicator(Define.EIndicator type) {...}
    public bool MotifyNameEqule(MotifyInfo[] infos, MotifyInfo info) {...}
    public bool MotifyTypeEqule(MotifyInfo[] infos, MotifyInfo info) {...}
}
```
</details>

<details Open>
<summary>스킬 인벤토리 Code Open</summary>

```C#
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
<summary> 스킬 매니저 FullCode Open </summary>

```C#
public class SkillManager
{
    public SkillInventory playerInventory;
    public Define.ESkill curMainSkill = Define.ESkill.Projectile;
    public void GetSkillInventory() { playerInventory = Managers.Game.GetPlayer().GetComponent<SkillInventory>(); }

    public Motify SetMotify(string name)
    {
        switch (name)
        {
            case "IceElemental":
                return new IceElemental();
            case "WindElemental": 
                return new WindElemental();
            case "FireElemental":
                return new FireElemental();
            case "EmbodimentMotify":
                return new EmbodimentMotify();
            case "HorizontalDeployment":
                return new HorizontalDeployment();           
            case "MoveMotify":
                return new MoveMotify();
            case "CircleMove":
                return new CircleMove();
        }

        return null;
    }

    public GameObject SetIndicator(Define.EIndicator type)
    {
        string typeName = Enum.GetName(typeof(Define.EIndicator), type);
        return Managers.Resource.Instantiate(typeName);
    }

    public bool MotifyNameEqule(MotifyInfo[] infos, MotifyInfo info)
    {
        for(int i = 0; i < infos.Length; i++)
        {
            if (infos[i] == null) continue;

            if (infos[i].skillName == info.skillName)
                return true;
        }

        return false;
    }
    public bool MotifyTypeEqule(MotifyInfo[] infos, MotifyInfo info)
    {
        for (int i = 0; i < infos.Length; i++)
        {
            if (infos[i] == null) continue;

            if (infos[i].TypeEquals(info) == true)
                return true;
        }

        return false;
    }
}
```
</details>

<details>
<summary> 스킬 인벤토리 FullCode Open </summary>

```C#
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
public class ProjectileSkill : Skill
{
    public GameObject       hitVFX;
    public GameObject       muzzleVFX;
    public int              count = 1;

    private Rigidbody rigid;
    
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

<details>
<summary> 범위 지정스킬 공격 FullCode Open </summary>

```C#
public class Explosion : Attack
{
    private void Start()
    {
        StartCoroutine(OnDamaged());   
    }

    private void OnDestroy()
    {
        StopCoroutine(OnDamaged());
    }
}
```   
</details>

### 스킬 구성 3가지
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


### 2. 스킬 범위표시
플레이어와 보스에 스킬타입마다 각각 다른 스킬범위표시를 플레이어에게 제공합니다.

<details Open>
<summary> Indicator Code </summary>

```C#
public class IndicatorInfo
{
    public Define.EIndicator type = Define.EIndicator.RangeIndicator;
    public float maxLength = 5f;
    public float maxRadius = 5f;
}

public class Indicator : MonoBehaviour
{
    public IndicatorInfo info;

    public void SetInfo(Define.EIndicator type, float maxLength, float maxRadius) {...}
    public void InitRotate(Vector3 direction) {...}
    public virtual void UpdatePosition(Vector3 inputPos)  {...}
    public virtual void UpdateRotate(Vector3 direction, float speed) {...}
}
```
</details>

<details>
<summary> Indicator FullCode Open </summary>

```C#
public class IndicatorInfo
{
    public Define.EIndicator type = Define.EIndicator.RangeIndicator;
    public float maxLength = 5f;
    public float maxRadius = 5f;
}

public class Indicator : MonoBehaviour
{
    public IndicatorInfo info;

    public void SetInfo(Define.EIndicator type, float maxLength, float maxRadius)
    {
        info = new IndicatorInfo();

        info.type = type;
        info.maxLength = maxLength;
        info.maxRadius = maxRadius;

        switch (type)
        {
            case Define.EIndicator.ArrowIndicator:               
                    transform.localScale = new Vector3(1f, maxLength, 1f);           
                break;
            case Define.EIndicator.CircleIndicator:              
                    transform.localScale = new Vector3(maxLength, maxLength, maxLength);                
                break;
            case Define.EIndicator.RangeIndicator:               
                    transform.localScale = new Vector3(maxRadius, maxRadius, maxRadius);               
                break;
        }
    }

    public void InitRotate(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        transform.rotation = targetRotation * Quaternion.Euler(90, 0, 90);
    }

    public virtual void UpdatePosition(Vector3 inputPos) 
    {
        if (info.type != Define.EIndicator.CircleIndicator)
            return;
    }
    public virtual void UpdateRotate(Vector3 direction, float speed)
    {

    }
}
```
</details>

<details>
<summary> 화살 Indicator FullCode Open </summary>

```C#
public class ArrowIndicator : Indicator
{   
    public override void UpdatePosition(Vector3 inputPos)
    {
        base.UpdatePosition(inputPos);

        Vector3 playerPos = Managers.Game.GetPlayer().transform.position;
        Vector3 direction = (inputPos - Managers.Game.GetPlayer().transform.position).normalized;
        Vector3 length = direction * (info.maxLength / 2);
        transform.position = new Vector3(playerPos.x + length.x, 1f, playerPos.z + length.z);

        Quaternion rotate = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Euler(90f, rotate.eulerAngles.y, rotate.eulerAngles.z);     
    }
}

```    
</details>

<details>
<summary> 원 Indicator Fullcode Open </summary>

```C#
public class CircleIndicator : Indicator
{
    RangeIndicator rangeIndicator;

    public void SetRange(GameObject go) 
    {
        // RangeIndicator는 모양뿐인 클래스
        rangeIndicator = go.GetComponent<RangeIndicator>();
        rangeIndicator.SetInfo(Define.EIndicator.RangeIndicator, info.maxLength, info.maxRadius);

        Vector3 playerPos = Managers.Game.GetPlayer().transform.position;
        go.transform.position = new Vector3(playerPos.x, go.transform.position.y, playerPos.z);
        go.transform.parent = Managers.Game.GetPlayer().transform;
    }

    public override void UpdatePosition(Vector3 inputPos)
    {
        if (rangeIndicator == null)
            return;

        Vector3 playerPos = Managers.Game.GetPlayer().transform.position;
        float distance = Vector3.Distance(inputPos, playerPos);

        if(distance > (rangeIndicator.info.maxRadius / 2))
        {
            Vector3 direction = (inputPos - playerPos).normalized;
            Vector3 maxPos = direction * (info.maxRadius / 2);
            inputPos = new Vector3(playerPos.x + maxPos.x, transform.position.y, playerPos.z + maxPos.z);
        }

        transform.position = new Vector3(inputPos.x, transform.position.y, inputPos.z);
    }

    private void OnDestroy()
    {
        Managers.Resource.Destroy(rangeIndicator.gameObject);
    }
}

```
</details>

<details>
<summary> 몬스터 부채꼴 Indicator FullCode Open </summary>

```C#
public class EnemyArcIndicator : Indicator
{ 
    public override void UpdatePosition(Vector3 inputPos)
    {
        transform.position = new Vector3(inputPos.x, transform.position.y, inputPos.z);
    }

    public override void UpdateRotate(Vector3 direction, float speed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation * Quaternion.Euler(90, 0, 90), speed);
    }
}
```    
</details>

<details>
<summary> 몬스터 박스형 Indicator FullCode Open </summary>

```C#
public class EnemyBoxIndicator : Indicator
{
    public override void UpdatePosition(Vector3 inputPos)
    {
        transform.position = new Vector3(inputPos.x, transform.position.y, inputPos.z);
    }

    public override void UpdateRotate(Vector3 direction, float speed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized,Vector3.up);
        transform.rotation = targetRotation * Quaternion.Euler(90, 0, 90);
    }
}
```
</details>

### 2. 보스의 공격패턴들
보스는 2가지의 강한스킬과 기본공격을 하도록 구현했습니다.

<details Open>
<summary> 내려찍기 범위표시 및 공격 FullCode </summary>

```C#
public IEnumerator OnGroundAttack()
{
    OnSkill();
    Indicator indicator = IndicatorExecute(Define.EState.GroundAttack);
    indicator.InitRotate(lockTarget.transform.position - transform.position);
    indicator.transform.parent = this.gameObject.transform;

    float sec = 0f;
    while (sec <= 5f)
    {
        sec += Time.deltaTime;
        indicator.UpdatePosition(transform.position);
        float lerpX = Mathf.Lerp(0.1f, 3f, sec / 3f);
        indicator.transform.localScale = new Vector3(lerpX, lerpX * 1.5f, 1);

        if (sec <= 4)
            indicator.UpdateRotate(lockTarget.transform.position - transform.position, 20f);
        else if (sec >= 4f && EState != Define.EState.GroundAttack)
            EState = Define.EState.GroundAttack;

        yield return null;
    }
    
    Managers.Resource.Destroy(indicator.gameObject);
}

public void GroundAttack()
{
    Vector3 direction = lockTarget.transform.position - transform.position;        
    float distance = direction.magnitude;
    float angle = Vector3.Angle(transform.forward, direction.normalized);

    if(120f >= angle / 2 && distance <= 6f)
    {
        Stat targetStat = lockTarget.GetComponent<Stat>();
        targetStat.OnDamaged(stat);
    }
}
```    
</details>

<details Open>
<summary> 돌진 범위표시 및 공격 FullCode </summary>

```C#
// 공격 범위표시용 코루틴
public IEnumerator OnHardAttack()
{
    OnSkill();
    Indicator indicator = IndicatorExecute(Define.EState.HardAttack);
    indicator.transform.parent = this.gameObject.transform;

    float sec = 0f;
    while(sec <= 5f)
    {
        sec += Time.deltaTime;
        indicator.UpdatePosition(transform.position);
        indicator.UpdateRotate((lockTarget.transform.position - transform.position).normalized, 10f);

        float lerpX = Mathf.Lerp(0.1f, 5f, sec / 3f);
        indicator.transform.localScale = new Vector3(lerpX, 1, 1);

        Vector3 direction = (lockTarget.transform.position - transform.position).normalized;
        Quaternion targetRotate = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotate, 10f * Time.deltaTime);

        yield return null;          
    }
    Managers.Resource.Destroy(indicator.gameObject);

    StartCoroutine(HardAttack());
}

// 공격을 위한 이동목적 코루틴
private IEnumerator HardAttack()
{
    EState = Define.EState.HardAttack;
    Vector3 prevPos = transform.position;
    Vector3 prevForward = transform.forward;
    float speed = stat.Movespeed * 1.5f;
    float sec = 0f;

    while (true)
    {
        sec += Time.deltaTime;
        float moveLength = Vector3.Distance(prevPos, transform.position);
        if (AttackChecking(prevPos, 2f) == true)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnDamaged(stat);
            break;
        }
        else if(moveLength >= 40f || sec >= 5f)
            break;
                      
        transform.position += prevForward * Time.deltaTime * speed;
        yield return new WaitForFixedUpdate();
    }

    OffSkill();
}
```
</details>

### 3. 인벤토리
기본적인 아이템 추가, 삭제, 정렬기능을 구현했고 데이터와 UI를 분리하여 보다 업데이트를 사용하지않고 개발자가 원하는 타이밍에 데이터를 업데이트 하도록 구현하였으며

아이템 툴팁과 슬롯의 드래그이동, 클릭이벤트를 응용한 더블클릭 이벤트를 구현해 아이템 사용을 구현했습니다.

<details Open> 
<summary>인벤토리 매니저 Code</summary>

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
</details>

<details Open>
<summary> 아이템 슬롯 Code </summary>

```C#
public class UI_Inven_Slot : UIPopup
{
    enum GameObjects
    {
        SlotIcon,
        ItemStack
    }

    public Iteminfo item;
    private RectTransform rect;

    public int Index { get; private set; }

    public override void Init() {...}
    public void SetInfo(Iteminfo info) {...}
    public void ItemCounting(Iteminfo info) {...}
    public void SetIndex(int index) {...}
    private void EnterSlotEvent(PointerEventData eventData) {...}
    private void ExitSlotEvent(PointerEventData eventData) {...}
}
```
</details>

<details>
  <summary> 인벤토리 매니저 FullCode Open</summary>

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
<details>
<summary> 인벤토리UI FullCode Open</summary>
  
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

<details>
<summary> 드래그슬롯 클래스 FullCode Open </summary>

```C#
private void OffRayTarget(PointerEventData eventData)
{
    if (eventData.pointerDrag == null || !UI_SetDragSlot.instance.isDraging)
        return;

    icon.raycastTarget = false;
    UI_SetDragSlot.instance.hoverSlots.Add(icon);
}

private void OnRayTarget(PointerEventData eventData)
{
    if (eventData.pointerDrag == null || UI_SetDragSlot.instance.isDraging)
        return;
    
    icon.raycastTarget = true;
    UI_SetDragSlot.instance.hoverSlots.Remove(icon);
}

private void StartDragging(PointerEventData eventData)
{
    UI_SetDragSlot.instance.dragSlot = this;
    UI_SetDragSlot.instance.DragSetIcon(icon);
    UI_SetDragSlot.instance.transform.position = eventData.position;
    UI_SetDragSlot.instance.isDraging = true;
    UI_SetDragSlot.instance.icon.raycastTarget = false;
}

private void Dragging(PointerEventData eventData)
{
    UI_SetDragSlot.instance.SetColor(0.6f);
    UI_SetDragSlot.instance.transform.position = eventData.position;
}

private void EndDragging(PointerEventData eventData)
{
    UI_SetDragSlot.instance.SetColor(0);
    UI_SetDragSlot.instance.isDraging = false;
    UI_SetDragSlot.instance.icon.raycastTarget = true;
    UI_SetDragSlot.instance.gameObject.transform.position = new Vector3(99f, 99f, 99f);
    UI_SetDragSlot.instance.dragSlot = null;

    foreach (Image slot in UI_SetDragSlot.instance.hoverSlots)
        slot.raycastTarget = true;
}
```
</details>

<details>
<summary> 드랍슬롯 클래스 FullCode Open </summary>

```C#
private void OnHighlight(PointerEventData eventData) { icon.color = Color.red; }
private void OffHighlight(PointerEventData eventData) { icon.color = Color.white; }
private void OnDrop(PointerEventData eventData)
{
    if (eventData.pointerDrag.gameObject == this.gameObject)
        return;

    if (eventData.pointerDrag == null || eventData.pointerDrag.GetComponent<UI_DropSlot>() == true)
        return;

    int item1 = GetComponent<UI_Inven_Slot>().Index;
    int item2 = eventData.pointerDrag.transform.parent.GetComponent<UI_Inven_Slot>().Index;
    Managers.Inventory.InfoLink(item1, item2);
}
```
</details>

<details>
<summary> 아이템슬롯 FullCode Open </summary>

```C#
public class UI_Inven_Slot : UIPopup
{
    enum GameObjects
    {
        SlotIcon,
        ItemStack
    }

    public Iteminfo item;
    private RectTransform rect;


    public int Index { get; private set; }

    public override void Init()
    {
        rect = GetComponent<RectTransform>();
        BindObject(typeof(GameObjects));
        GetObject((int)GameObjects.ItemStack).SetActive(false);

        gameObject.BindEvent((evt) => { Managers.Inventory.SelectIndex = Index;});
        gameObject.BindEvent(EnterSlotEvent, Define.EUiEvent.PointerEnter);
        gameObject.BindEvent(ExitSlotEvent, Define.EUiEvent.PointerExit);
    }

    public void SetInfo(Iteminfo info)
    {
        item = info;
        info = info ?? Managers.Data.ItemDict[199];

        Image slot = GetObject((int)GameObjects.SlotIcon).GetComponent<Image>();
        Texture2D texture = Managers.Resource.Load<Texture2D>(info.uiInfo.icon);
        slot.sprite = Managers.UI.TextureToSprite(texture);

        ItemCounting(info);
    }

    public void ItemCounting(Iteminfo info)
    {
        if (info.isStack == true)
        {
            GetObject((int)GameObjects.ItemStack).SetActive(true);
            GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = string.Format($"{info.curStack}");
        }
        else
        {
            GetObject((int)GameObjects.ItemStack).SetActive(false);
            GetObject((int)GameObjects.ItemStack).GetComponent<Text>().text = "1";
        }
    }

    public void SetIndex(int index) { Index = index; }

    private void EnterSlotEvent(PointerEventData eventData)
    {
        if (item == null)
            return;

        UI_InvenTip.Instance.SetColor(1f);
        UI_InvenTip.Instance.SetToolTip(item);

        RectTransform tooltipRect = UI_InvenTip.Instance.GetComponent<RectTransform>();
        tooltipRect.pivot = new Vector2(0, 1);
        tooltipRect.position = rect.position;

        float x = Mathf.Clamp(tooltipRect.anchoredPosition.x, -UI_InvenTip.Instance.parentRect.position.x + tooltipRect.rect.size.x, UI_InvenTip.Instance.parentRect.position.x - tooltipRect.rect.size.x / 2);
        float y = Mathf.Clamp(tooltipRect.anchoredPosition.y, -UI_InvenTip.Instance.parentRect.position.y + tooltipRect.rect.size.y, UI_InvenTip.Instance.parentRect.position.y);

        tooltipRect.anchoredPosition = new Vector2(x, y);

        //Cursor.visible = false;
    }

    private void ExitSlotEvent(PointerEventData eventData)
    {
        UI_InvenTip.Instance.SetColor(0f);

        //Cursor.visible = true;
    }
}
```
</details>

<details>
<summary> 아이템슬롯 더블클릭 FullCode Open </summary>

```C#
public class UI_Inven_DoubleClick : UI_Slot
{
    private UI_Inven_Slot item;
    private bool isDoubleCheck = false;

    private void Start()
    {
        item = transform.parent.GetComponent<UI_Inven_Slot>(); 
        gameObject.BindEvent(DoubleClickEvent, Define.EUiEvent.Click);
    }

    void Update()
    {
        if(isDoubleCheck == true)
        {
            Managers.Inventory.InvenInfos[item.Index].Use(item.Index);
            isDoubleCheck = false;
        }
    }

    private void DoubleClickEvent(PointerEventData eventData)
    {
        if (item == null)
            return;

        if (eventData.clickCount >= 2)
            isDoubleCheck = true;
    }
}
```
</details>

### 4. 장비창 및 세부기능
인벤토리와 유사하게 슬롯UI와 아이템데이터를 나눠 관리했으며, 더블클릭으로 아이템 장착 및 해제시 스텟이 추가 및 감소되고 장비창UI의 텍스트를 업데이트하도록 구현했습니다.

<details Open>
<summary> 장비 매니저 Code </summary>

```C#
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager
{
    public Action OnStatusSet = null;

    public Texture2D[] emptyImage = new Texture2D[4];
    public Iteminfo[] equipInfos = new Iteminfo[4];
    public UI_EquipSlot[] slots = new UI_EquipSlot[4];

    public void Init() {...}
    public void OnEquip() {...}
    public void ItemEquip(Iteminfo item) {...}
    public void ItemStatPlus() {...}
    public void ItemUnEquip(int index) {...}
}
```
</details>

<details>
<summary> 장비 매니저 FullCode Open </summary>

```C#
public class EquipManager
{
    public Action OnStatusSet = null;

    public Texture2D[] emptyImage = new Texture2D[4];
    public Iteminfo[] equipInfos = new Iteminfo[4];
    public UI_EquipSlot[] slots = new UI_EquipSlot[4];

    public void Init()
    {
        emptyImage[0] = Managers.Resource.Load<Texture2D>("melee_background");
        emptyImage[1] = Managers.Resource.Load<Texture2D>("chest_background");
        emptyImage[2] = Managers.Resource.Load<Texture2D>("pants_background");
        emptyImage[3] = Managers.Resource.Load<Texture2D>("boots_background");
    }

    public void OnEquip()
    {
        if (Input.GetKeyDown(BindKey.Equipment))
            Managers.UI.OnGameUIPopup<UI_Equip>();

        if (Util.FindChild<UI_Equip>(Managers.UI.Root) == null)
            return;
        else
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (equipInfos[i] == null)
                    slots[i].SetBefore(emptyImage[i]);
                else
                    slots[i].SetInfo(equipInfos[i]);

                slots[i].index = i;
            }
        }  
    }

    public void ItemEquip(Iteminfo item)
    {
        switch (item.type)
        {
            case Define.ItemType.Weapon:
                equipInfos[0] = item;
                break;
            case Define.ItemType.Chest:
                equipInfos[1] = item; 
                break;
            case Define.ItemType.Pants:
                equipInfos[2] = item; 
                break;
            case Define.ItemType.Boots:
                equipInfos[3] = item; 
                break;
        }

        if(Util.FindChild(Managers.UI.Root) != null)
        {
            for (int i = 0; i < slots.Length; i++) 
            {
                if (equipInfos[i] == null || slots[i] == null)
                    continue;

                slots[i].SetInfo(equipInfos[i]);
                slots[i].index = i;
            }
        }

        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        playerStat.MaxHp += item.hp;
        playerStat.Attack += item.att;

        OnStatusSet?.Invoke();
    }

    public void ItemStatPlus()
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();

        for (int i = 0; i < equipInfos.Length; i++)
        {
            if (equipInfos[i] == null)
                continue;

            playerStat.MaxHp += equipInfos[i].hp;
            playerStat.Attack += equipInfos[i].att;
        }
    }

    public void ItemUnEquip(int index)
    {
        if (equipInfos[index] == null)
            return;

        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        playerStat.MaxHp -= equipInfos[index].hp;
        playerStat.Attack -= equipInfos[index].att;

        Managers.Inventory.AddItem(equipInfos[index]);
        equipInfos[index] = null;
        slots[index].SetBefore(emptyImage[index]);

        OnStatusSet?.Invoke();
    }
}
```
</details>

<details>
<summary> 장비창 UI FullCode Open </summary>

```C#
public class UI_Equip : UIPopup
{
    enum GameObjects
    {
        UI_Equip_Sword,
        UI_Equip_Armor,
        UI_Equip_Pant,
        UI_Equip_Boots,
        UI_Equip_ExitButton
    }

    enum Texts
    {
        UI_Equip_Hp_Text,
        UI_Equip_Mp_Text,
        UI_Equip_Att_Text,
        UI_Equip_Def_Text,
        UI_Equip_Move_Text
    }

    public override void Init()
    {
        base.Init();
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        GetObject((int)GameObjects.UI_Equip_ExitButton).BindEvent(evt => { Managers.UI.ClosePopupUI(); });
        SetSlots();

        Managers.Equip.OnStatusSet -= StatusSet;
        Managers.Equip.OnStatusSet += StatusSet;
        StatusSet();
    }e

    public void SetSlots()
    {
        for (int i = 0; i < Managers.Equip.slots.Length; i++)
        {
            GameObject go = GetObject((int)GameObjects.UI_Equip_Sword + i);
            UI_EquipSlot slot = go.GetComponent<UI_EquipSlot>();
            Managers.Equip.slots[i] = slot;
        }
    }

    public void StatusSet()
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();

        Text bindCheck = GetText((int)Texts.UI_Equip_Hp_Text);
        if (bindCheck == null)
            return;

        GetText((int)Texts.UI_Equip_Hp_Text).text = string.Format($"{playerStat.Hp} / {playerStat.MaxHp}");
        GetText((int)Texts.UI_Equip_Mp_Text).text = string.Format($"{playerStat.Mp} / {playerStat.MaxMp}");
        GetText((int)Texts.UI_Equip_Att_Text).text = string.Format($"{playerStat.Attack}");
        GetText((int)Texts.UI_Equip_Def_Text).text = string.Format($"{playerStat.Defense}");
        GetText((int)Texts.UI_Equip_Move_Text).text = string.Format($"{playerStat.Movespeed}");
    }
}
```
</details>

<details>
<summary> 장비창UI 슬롯 FullCode </summary>

```C#
public class UI_EquipSlot : UIPopup
{
    enum GameObjects
    {
        SlotIcon
    }

    public Iteminfo equipItem;
    public int index;

    public override void Init()
    {
        BindObject(typeof(GameObjects));
    }

    public void SetInfo(Iteminfo info)
    {       
        equipItem = info;
        GameObject go = GetObject((int)GameObjects.SlotIcon);
        if (go == null)
            return;

        Image slot = go.GetComponent<Image>();
        Texture2D texture = Managers.Resource.Load<Texture2D>(info.uiInfo.icon);
        slot.sprite = Managers.UI.TextureToSprite(texture);
    }

    public void SetBefore(Texture2D image)
    {
        equipItem = null;

        Image slot = GetObject((int)GameObjects.SlotIcon).GetComponent<Image>();
        slot.sprite = Managers.UI.TextureToSprite(image);
    }
}
```
</details>

<details>
<summary> 장비슬롯 더블클릭 FullCode Open </summary>

```C#
public class UI_Equip_DoubleClick : MonoBehaviour
{
    private UI_EquipSlot slot;
    private bool isDoubleCheck = false;

    private void Start()
    {
        slot = transform.parent.GetComponent<UI_EquipSlot>();
        gameObject.BindEvent(DoubleClickEvent, Define.EUiEvent.Click);
    }

    void Update()
    {
        if (isDoubleCheck == true)
        {
            Managers.Equip.ItemUnEquip(slot.index);
            isDoubleCheck = false;
        }
    }

    private void DoubleClickEvent(PointerEventData eventData)
    {
        if (slot == null)
            return;

        if (eventData.clickCount >= 2)
            isDoubleCheck = true;
    }
}
```
</details>

### 5. 스킬창 및 세부기능
스킬들의 세부정보와 현재 사용중인 스킬 및 파츠들의 정보를 제공하는 툴팁, 파츠의 사용을 결정하도록 UI를 구현했습니다.

<details Open> 
<summary> 스킬창 Code </summary>

```C#
public class UI_Skill : UIPopup
{
    enum GameObjects
    {
        ProjectileTab,
        ExplosionTab,
        Initialize_MotifyGround,
        Embodiment_MotifyGround,
        Movement_MotifyGround,
        MainSkill_Slot
    }

    private UI_MotifyGround[] grounds = new UI_MotifyGround[3];
    private int slotCount = 3;

    public override void Init() {...}
    private void SetSkill() {...}
    private void SetMotifys() {...}
    private void SetInitializeSlot() {...}
    private void SetEmbodimentSlot() {...}
    private void SetMovementSlot() {...}
    private void SetGroundInfo() {...}
}

```
</details>

<details Open>
<summary> 메인스킬 툴팁 Code </summary>

```C#
public class UI_SkillTip : MonoBehaviour
{
    private static UI_SkillTip instance;

    public CanvasGroup group;
    public RectTransform parentRect;
    public Image icon;
    public Text skillName;
    public Text skillFuction;
    public Text skillStat;
    public Text skillDescription;
    public Text initMotify;
    public Text embodiMotify;
    public Text moveMotify;

    private MotifyInfo initInfo;
    private MotifyInfo embodiInfo;
    private MotifyInfo moveInfo;

    public static UI_SkillTip Instance => instance;

    private void Awake() {...}
    public void SetToolTip(SkillInfo skillInfo) {...}
    public void SetColor(float alpha) {...}
}
```
</details>

<details Open>
<summary> 스킬파츠 툴팁 Code </summary>

```C#
public class UI_MotifyTip : MonoBehaviour
{
    static UI_MotifyTip instance;
    public CanvasGroup group;
    public RectTransform parentRect;
    public Image motifyIcon;
    public Text motifyName;
    public Text motifyFuction;
    public Text motifyStat;
    public Text motifyDescription;

    public static UI_MotifyTip Instance => instance;

    private void Start() {...}
    public void SetToolTip(MotifyInfo motifyInfo) {...}
    public void SetColor(float alpha) {...}
}
```
</details>

<details Open>
<summary> 스킬창 슬롯 Code </summary>

```C#
public class UI_SkillSlot : UI_Slot
{
    public static UI_SkillSlot instance;
    public SkillInfo skillInfo;
    RectTransform   rect;

    public override void Init() {...}
    public void SetInfo(SkillInfo info) {...}
    private void EnterSlotEvent(PointerEventData eventData) {...}
    private void ExitSlotEvent(PointerEventData eventData) {...}
}
```
</details>

<details Open>
<summary> 메인스킬 슬롯 Code </summary>

```C#
public class UI_SkillSlot : UI_Slot
{
    public static UI_SkillSlot instance;
    public SkillInfo skillInfo;
    RectTransform   rect;

    public override void Init() {...}
    public void SetInfo(SkillInfo info) {...}
    private void EnterSlotEvent(PointerEventData eventData) {...}
    private void ExitSlotEvent(PointerEventData eventData) {...}
}
```
</details>

<details Open>
<summary> 스킬파츠 슬롯 Code </summary>

```C#
public class UI_MotifySlot : UI_Slot
{
    public bool         isClick = false;
    public MotifyInfo   motifyInfo;
    RectTransform       rect;

    public override void Init() {...}
    public void SetInfo(MotifyInfo info) {...}
    public void SetColor() {...}
    private void OnClickEvent(PointerEventData eventData) {...}
    private void EnterSlotEvent(PointerEventData eventData) {...}
    private void ExitSlotEvent(PointerEventData eventData) {...}
    private void OnDestroy() {...}
}

```
</details>

<details>
<summary> 스킬창 FullCode Open </summary>

```C#
public class UI_Skill : UIPopup
{
    enum GameObjects
    {
        ProjectileTab,
        ExplosionTab,
        Initialize_MotifyGround,
        Embodiment_MotifyGround,
        Movement_MotifyGround,
        MainSkill_Slot
    }

    private UI_MotifyGround[] grounds = new UI_MotifyGround[3];
    private int slotCount = 3;

    public override void Init()
    {
        base.Init();

        BindObject(typeof(GameObjects));
        GetObject((int)GameObjects.ProjectileTab).BindEvent(evt => 
        {
            Managers.Skill.curMainSkill = Define.ESkill.Projectile;
            SetSkill();
        });
        GetObject((int)GameObjects.ExplosionTab).BindEvent(evt => 
        {
            Managers.Skill.curMainSkill = Define.ESkill.AreaOfEffect;
            SetSkill();
        });

        SetMotifys();
        SetSkill();     
    }

    private void SetSkill()
    {
        GameObject go = GetObject((int)GameObjects.MainSkill_Slot);
        UI_SkillSlot slot = go.GetComponent<UI_SkillSlot>();

        switch (Managers.Skill.curMainSkill)
        {
            case Define.ESkill.Projectile:
                slot.SetInfo(Managers.Data.SkillDict["ProjectileSkill"]);
                break;
            case Define.ESkill.AreaOfEffect:
                slot.SetInfo(Managers.Data.SkillDict["ExplosionSkill"]);
                break;
        }
        SetGroundInfo();
    }

    private void SetMotifys()
    {
        SetInitializeSlot();
        SetEmbodimentSlot();
        SetMovementSlot();
    }

    private void SetInitializeSlot()
    {
        Transform parent = GetObject((int)GameObjects.Initialize_MotifyGround).transform; 
        UI_MotifyGround ground = parent.GetComponent<UI_MotifyGround>();
        grounds[0] = ground;

        for (int i = 1; i <= slotCount; i++)
        {
            GameObject prefab = Managers.Resource.Instantiate("UI_MotifySlot");

            UI_MotifySlot slot = prefab.GetComponent<UI_MotifySlot>();
            slot.SetInfo(Managers.Data.MotifyDict[100 + i]); 
            prefab.transform.SetParent(parent);
        }

        ground.SetSlots();
    }

    private void SetEmbodimentSlot()
    {
        Transform parent = GetObject((int)GameObjects.Embodiment_MotifyGround).transform;
        UI_MotifyGround ground = parent.GetComponent<UI_MotifyGround>();
        grounds[1] = ground;

        for (int i = 1; i <= slotCount; i++)
        {
            GameObject prefab = Managers.Resource.Instantiate("UI_MotifySlot");

            UI_MotifySlot slot = prefab.GetComponent<UI_MotifySlot>();            
            slot.SetInfo(Managers.Data.MotifyDict[150 + i]); 
            prefab.transform.SetParent(parent);
        }

        ground.SetSlots();
    }
    
    private void SetMovementSlot()
    {
        Transform parent = GetObject((int)GameObjects.Movement_MotifyGround).transform;
        UI_MotifyGround ground = parent.GetComponent<UI_MotifyGround>();
        grounds[2] = ground;

        for (int i = 1; i <= slotCount; i++)
        {
            GameObject prefab = Managers.Resource.Instantiate("UI_MotifySlot");

            UI_MotifySlot slot = prefab.GetComponent<UI_MotifySlot>();
            slot.SetInfo(Managers.Data.MotifyDict[200 + i]);
            prefab.transform.SetParent(parent);
        }

        ground.SetSlots();
    }

    private void SetGroundInfo()
    {      
        SkillInventory skillInven = Managers.Game.GetPlayer().GetComponent<SkillInventory>();
        SkillInfo mainSkill = skillInven.skillMotifies.FirstOrDefault(x => x.Key.type == Managers.Skill.curMainSkill).Key;

        if (mainSkill == null) return;

        for(int i = 0; i < grounds.Length; i++)
        {
            if (skillInven.skillMotifies[mainSkill][i] == null)
            {
                grounds[i].DeSelect();
                continue;
            }

            for (int j = 0; j < slotCount; j++)
            {
                // MofityInfo의 Equals를 오버라이딩해서 사용
                if (skillInven.skillMotifies[mainSkill][i].Equals(grounds[i].slots[j].motifyInfo) == false)
                    continue;

                grounds[i].CheckSlots(grounds[i].slots[j]);
            }
        }
    }
}

```
</details>

<details>
<summary> 메인스킬 툴팁 FullCode Open </summary>

```C#
public class UI_SkillTip : MonoBehaviour
{
    private static UI_SkillTip instance;

    public CanvasGroup group;
    public RectTransform parentRect;
    public Image icon;
    public Text skillName;
    public Text skillFuction;
    public Text skillStat;
    public Text skillDescription;
    public Text initMotify;
    public Text embodiMotify;
    public Text moveMotify;

    private MotifyInfo initInfo;
    private MotifyInfo embodiInfo;
    private MotifyInfo moveInfo;

    public static UI_SkillTip Instance => instance;

    private void Awake()
    {
        instance = this;
        group = GetComponent<CanvasGroup>();
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    public void SetToolTip(SkillInfo skillInfo)
    {
        SkillInventory skillInventory = Managers.Game.GetPlayer().GetComponent<SkillInventory>();
        MotifyInfo[] motifys = skillInventory.skillMotifies[skillInfo];

        Texture2D texture = Managers.Resource.Load<Texture2D>(skillInfo.icon);
        icon.sprite = Managers.UI.TextureToSprite(texture);

        skillName.text = string.Format($"{skillInfo.name}");
        skillStat.text = string.Format($"마나 : {skillInfo.mana}");
        skillFuction.text = string.Format($"{skillInfo.function}");
        skillDescription.text = string.Format($"{skillInfo.description}");

        string initName = motifys[0] == null ? Managers.Data.MotifyDict[100].name : motifys[0].name;
        string embodiName = motifys[1] == null ? Managers.Data.MotifyDict[150].name : motifys[1].name;
        string moveName = motifys[2] == null ? Managers.Data.MotifyDict[200].name : motifys[2].name;

        initMotify.text = string.Format($"{initName}");
        embodiMotify.text = string.Format($"{embodiName}");
        moveMotify.text = string.Format($"{moveName}");
    }

    public void SetColor(float alpha) { group.alpha = alpha; }
}

```
</details>

<details>
<summary> 스킬파츠 툴팁 FullCode Open </summary>

```C#
public class UI_MotifyTip : MonoBehaviour
{
    static UI_MotifyTip instance;
    public CanvasGroup group;
    public RectTransform parentRect;
    public Image motifyIcon;
    public Text motifyName;
    public Text motifyFuction;
    public Text motifyStat;
    public Text motifyDescription;

    public static UI_MotifyTip Instance => instance;
    private void Start()
    {
        instance = this;
        parentRect = transform.parent.GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
    }

    public void SetToolTip(MotifyInfo motifyInfo)
    {
        Texture2D texture = Managers.Resource.Load<Texture2D>(motifyInfo.icon);
        motifyIcon.sprite = Managers.UI.TextureToSprite(texture);

        motifyName.text = string.Format($"{motifyInfo.name}");
        motifyStat.text = string.Format($"마나 : {motifyInfo.mana}");
        motifyFuction.text = string.Format($"{motifyInfo.function}");
        motifyDescription.text = string.Format($"{motifyInfo.description}");
    }

    public void SetColor(float alpha) { group.alpha = alpha; }
}
```
</details>

<details>
<summary> 메인스킬 슬롯 FullCode Open </summary>

```C#
public class UI_SkillSlot : UI_Slot
{
    public static UI_SkillSlot instance;
    public SkillInfo skillInfo;
    RectTransform   rect;

    public override void Init()
    {
        instance = this;
        rect = GetComponent<RectTransform>();
        gameObject.BindEvent(EnterSlotEvent, Define.EUiEvent.PointerEnter);
        gameObject.BindEvent(ExitSlotEvent, Define.EUiEvent.PointerExit);
    }

    public void SetInfo(SkillInfo info)
    {
        base.Init();

        skillInfo = info;

        Texture2D texture = Managers.Resource.Load<Texture2D>(skillInfo.icon);
        Image slotIcon = GetImage((int)Images.SlotIcon);
        slotIcon.sprite = Managers.UI.TextureToSprite(texture);
    }

    private void EnterSlotEvent(PointerEventData eventData)
    {
        UI_SkillTip.Instance.SetColor(1f);
        UI_SkillTip.Instance.SetToolTip(skillInfo);

        RectTransform tooltipRect = UI_SkillTip.Instance.GetComponent<RectTransform>();
        tooltipRect.pivot = new Vector2(0, 1);
        tooltipRect.position = rect.position;

        float y = Mathf.Clamp(tooltipRect.anchoredPosition.y, -UI_SkillTip.Instance.parentRect.position.y + tooltipRect.rect.size.y, UI_SkillTip.Instance.parentRect.position.y);

        tooltipRect.anchoredPosition = new Vector2(tooltipRect.anchoredPosition.x, y);

        Cursor.visible = false;
    }

    private void ExitSlotEvent(PointerEventData eventData)
    {
        UI_SkillTip.Instance.SetColor(0f);
        Cursor.visible = true;
    }
}
```
</details>

<details>
<summary> 스킬파츠 슬롯 FullCode Open </summary>

```C#
public class UI_MotifySlot : UI_Slot
{
    public bool isClick = false;
    public MotifyInfo motifyInfo;
    private RectTransform rect;

    public override void Init()
    {       
        icon = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        gameObject.BindEvent(OnClickEvent, Define.EUiEvent.Click);
        gameObject.BindEvent(EnterSlotEvent, Define.EUiEvent.PointerEnter);
        gameObject.BindEvent(ExitSlotEvent, Define.EUiEvent.PointerExit);
    }

    public void SetInfo(MotifyInfo info) 
    {
        base.Init();

        motifyInfo = info;

        Texture2D texture = Managers.Resource.Load<Texture2D>(motifyInfo.icon);
        Image slotIcon = GetImage((int)Images.SlotIcon);
        slotIcon.sprite = Managers.UI.TextureToSprite(texture);
    }

    public void SetColor()
    {
        icon.color = isClick == true ? Color.red : Color.white;
    }
    
    private void OnClickEvent(PointerEventData eventData)
    {
        UI_MotifyGround parent = transform.parent.GetComponent<UI_MotifyGround>();
        if (isClick == true)
            parent.DeSelect();
        else
            parent.CheckSlots(this);

        Managers.Skill.playerInventory.AddMotify(motifyInfo);
    }

    private void EnterSlotEvent(PointerEventData eventData)
    {
        UI_MotifyTip.Instance.SetToolTip(motifyInfo);
        UI_MotifyTip.Instance.SetColor(1f);

        RectTransform tooltipRect = UI_MotifyTip.Instance.GetComponent<RectTransform>();
        tooltipRect.pivot = new Vector2(0, 1);
        tooltipRect.position = rect.position;

        float y = Mathf.Clamp(tooltipRect.anchoredPosition.y, -UI_MotifyTip.Instance.parentRect.position.y + tooltipRect.rect.size.y, UI_MotifyTip.Instance.parentRect.position.y);

        tooltipRect.anchoredPosition = new Vector2(tooltipRect.anchoredPosition.x, y);

        Cursor.visible = false;
    }

    private void ExitSlotEvent(PointerEventData eventData)
    {
        UI_MotifyTip.Instance.SetColor(0f);

        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}
```
</details>

### 6. 퀘스트 관리
Action을 이용하여 퀘스트의 상태추적, 조건추적을 해도록 구현했습니다.

<details open>
<summary>퀘스트 매니저 Code Open</summary>

```C#
public class QuestManager
{
    private List<Quest> activeQuests = new List<Quest>();
    public IReadOnlyList<Quest> ActiveQuests => activeQuests;

    // 퀘스트추적 액션들
    public Action<QuestInfo> OnStartQuest = null;
    public Action<Quest> OnCompletedQuest = null;
    public Action<Quest> OnRewardsQuest = null;

    // 조건추적 액션들
    public Action<string> OnKillQuestAction = null;
    public Action<string, int> OnGetQuestAction = null;
    public Action<int> OnLevelQuestAction = null;

    public Action<int> OnCurrentUpdate = null;

    public void Init() {...}

    // 추가 삭제 완료 메서드들
    public void AddQuest(QuestInfo questInfo) {...}
    public void RemoveQuest(Quest quest) {...}
    public void CompleteQuest(Quest quest) {...}

    // 조건업데이트 메서드들
    public void UpdateKill(string target) {...}
    public void UpdateGet(string target, int count) {...}
    public void UpdateLevel(int curLevel) {...}
}
```
</details>

<details Open>
<summary> 퀘스트 UI Code </summary>

```C#
public class UI_Quest : UIPopup
{
    enum GameObjects
    {
        QuestList,
        UI_Quest_Popup
    }

    private GameObject list;
    private GameObject popup;
    private int questId = int.MaxValue;

    public override void Init() {...}
    private void QuestListInit() {...}
    public void OnQuestPopup(int id) {...}
    private void Update() {...}
}

```
</details>

<details>
<summary> 퀘스트 매니저 FullCode Open </summary>

```C#
public class QuestManager
{
    private List<Quest> activeQuests = new List<Quest>();
    public IReadOnlyList<Quest> ActiveQuests => activeQuests;
    
    public Action<QuestInfo> OnStartQuest = null;
    public Action<Quest> OnCompletedQuest = null;
    public Action<Quest> OnRewardsQuest = null;

    public Action<string> OnKillQuestAction = null;
    public Action<string, int> OnGetQuestAction = null;
    public Action<int> OnLevelQuestAction = null;

    public Action<int> OnCurrentUpdate = null;

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
<summary> 퀘스트 UI FullCode Open</summary>

```C#
public class UI_Quest : UIPopup
{
    enum GameObjects
    {
        QuestList,
        UI_Quest_Popup
    }

    private GameObject list;
    private GameObject popup;
    private int questId = int.MaxValue;

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

### 퀘스트 NPC
퀘스트 매니저의 기능들을 가져와 시각적으로 플레이어에게 제공하도록 구현했습니다.

<details open>
<summary> 퀘스트NPC Code </summary>

```C#
public class QuestGiver : NpcController
{
    private List<QuestInfo> quests = new List<QuestInfo>();
    public List<QuestInfo> Quests => quests;

    public override void Init() {...}
    public void Search() {...} // 가능한 퀘스트들 리스트업 후 저장
    public void OnTypeUI() {...} // 퀘스트리스트 UI 오픈
}
```
</details>

<details Open>
<summary> 퀘스트NPC UI Code </summary>

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
<summary> 퀘스트NPC FullCode Open</summary>

```C#
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
<summary> 퀘스트NPC UI FullCode Open</summary>

```C#
public class UI_Giver : UIPopup
{
    enum GiverObject
    {
        PossibleList,
        GiverQuest_Popup,
        UI_Giver_ExitButton
    }

    private List<QuestInfo> ownerQuests = new List<QuestInfo>();
    private GameObject popup;
    private GameObject list;

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
아이템리스트를 띄워주는 UI 발사대로 UI에 파싱된 데이터를 가져와 유저에게 제공하고 Buy, Sell기능도 UI에서 제공하도록 합니다.    

<details Open>
<summary> 상인NPC Code </summary>
    
```C#
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

<details Open>
<summary> 상점 아이템슬롯 Code </summary>

```C#
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
<summary> 상점UI FullCode Open </summary>
    
```C#
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


<details>
<summary> 상점 아이템슬롯 FullCode Open </summary>

```C#
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
