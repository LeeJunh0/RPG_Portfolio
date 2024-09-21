using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRealization : SkillMaker
{
    Define.ESkill type = Define.ESkill.Normal;
    int count = 1;

    public Define.ESkill EType { get { return type; } set { type = value; } }
    public int Count { get { return count; } set { count = value; } }

    // 마법의 생성결정
    public override void SkillInstantiate()
    {
        for (int i = 0; i < count; i++)
        {
            var magic = Managers.Resource.Instantiate(Skill.skillInstance.prefabMagic.name);
            Skill.skillInstance.balls.Add(magic.GetComponent<MagicBall>());
        }

        Skill skill = Skill.skillInstance;
        Transform player = Managers.Game.GetPlayer().transform;
        switch (type)
        {
            case Define.ESkill.Normal:
                foreach(var magic in skill.balls)
                {
                    magic.transform.position = new Vector3(player.position.x, 1f, player.position.z);
                    magic.transform.forward = player.forward;
                }
                break;
            case Define.ESkill.Horizontal:
                {
                    int half = count / 2;
                    int index = 0;

                    for (int i = -half; i <= half; i++)
                    {
                        Vector3 spawnPos =  new Vector3(player.position.x, 
                                            skill.balls[index].transform.position.y,
                                            player.position.z) + player.right * (i * 4f);

                        skill.balls[index].transform.position = spawnPos;
                        skill.balls[index].transform.forward = player.forward;
                        index++;
                    }
                }
                break;
            case Define.ESkill.Circle:
                {

                }
                break;
        }
    }
}
