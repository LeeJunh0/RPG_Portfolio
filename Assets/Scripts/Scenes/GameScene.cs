using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.EScene.Game;
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.EWorldObject.Player, "Player");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        CameraController minimap = Managers.Resource.Instantiate("Minimap_Camera").GetOrAddComponent<CameraController>();
        minimap.SetPlayer(player);

        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);

        Managers.Resource.Instantiate("UI_Game");
        Managers.Skill.GetSkillInventory();
        Managers.Equip.Init();
    }

    public override void Clear()
    {

    }
}
