using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : UIPopup
{
    Slider slider;
    Text tipText;

    [SerializeField]
    string[] tipArr;

    public override void Init()
    {
        base.Init(); 

        slider = Util.FindChild<Slider>(gameObject, "LoadingBar");
        tipText = Util.FindChild<Text>(gameObject, "LoadingTip");
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        slider.value = 0.0f;
        int random = Random.Range(0, tipArr.Length);
        tipText.text = tipArr[random];
    }

    public void Loading(Define.EScene type)
    {
        string label = "";
        Time.timeScale = 1.0f;

        switch (type)
        {
            case Define.EScene.Title:
                label = "Title";
                break;
            case Define.EScene.Game:
                label = "Game";
                break;
        }

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
    }

    IEnumerator SceneAsyncLoad(Define.EScene type)
    {
        yield return new WaitForSeconds(1.5f); // ����׿� �ϵ��ڵ�
        Managers.EScene.LoadScene(type);
        Managers.Pool.Push(gameObject.GetOrAddComponent<Poolable>());
    }
}
