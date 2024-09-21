using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public static Skill             skillInstance;

    public Transform                player;
    public SkillInitialize          initialize;
    public SkillRealization         realization;
    public SkillMovement            movement;
    public List<MagicBall>          balls;
    public GameObject               prefabMagic;
    public bool                     isActive;

    int     count;
    float   speed; 
    float   radius;
    float   curAngle;
    float   rotateSpeed;
    float   skillColldown;

    public int SkillCount { get; set; }
    public float SkillSpeed { get; set; }
    private void Awake()
    {
        skillInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        isActive = true;
        skillColldown = 1.5f;
    }

    public void tempMethod(SkillInitialize temp) { initialize = temp; }

    public void OnSkill()
    {
        player = Managers.Game.GetPlayer().transform;

        StartCoroutine(SkillCoolDown());
        NullProperty();
        initialize.SkillInit();
        realization.SkillInstantiate();
    }

    public void NullProperty()
    {
        if (initialize == null)         { initialize = Managers.Skill.NullInitialize(); }
        if (realization == null)        { realization = Managers.Skill.NullRealization(); }
        if (movement == null)           { movement = Managers.Skill.NullMoveMent(); }
    }

    private void FixedUpdate()
    {
        NullProperty();

        switch (movement.EType)
        {
            case Define.ESkill.Circle:
                {
                    curAngle += Time.deltaTime * rotateSpeed;
                    float perAngle = 360f / realization.Count;
                    //각도를 라디안으로 바꿔줌 라디안은 컴퓨터가 알아먹을수 있는 각도다.
                    for(int i = 0; i < realization.Count; i++)
                    {
                        // 삼각함수 Cos세타 * 길이 = x축지점, Sin세타 * 길이 = y축지점
                        float radianAngle = curAngle * Mathf.Deg2Rad + perAngle * i;

                        float x = MathF.Cos(radianAngle) * radius;
                        float z = Mathf.Sin(radianAngle) * radius;

                        balls[i].transform.position = new Vector3(player.position.x + x, balls[i].transform.position.y, player.position.z + z);
                    }
                }
                break;
            default:
                {
                    foreach (MagicBall magic in balls)
                    {
                        Rigidbody rigid = magic.GetComponent<Rigidbody>();
                        rigid.AddForce(magic.transform.forward * movement.Speed, ForceMode.Impulse);
                    }
                }
                break;
        }
    }
    IEnumerator SkillCoolDown()
    {
        isActive = false;

        yield return new WaitForSeconds(skillColldown);

        isActive = true;
    }
}
