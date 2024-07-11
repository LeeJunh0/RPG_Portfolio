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

    private void Awake()
    {
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

    public void Loading(Define.Scene type)
    {
        string label = "";
        Time.timeScale = 1.0f;

        switch (type)
        {
            case Define.Scene.Title:
                label = "Title";
                break;
            case Define.Scene.Game:
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

    IEnumerator SceneAsyncLoad(Define.Scene type)
    {
        yield return new WaitForSeconds(1.5f); // 디버그용 하드코딩
        Managers.Scene.LoadScene(type);
        Managers.Pool.Push(gameObject.GetOrAddComponent<Poolable>());
    }
}
