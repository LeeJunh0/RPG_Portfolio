using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestMark : UIBase
{
    enum MarkIcon
    {
        QuestMarkIcon
    }

    public override void Init()
    {
        Bind<Image>(typeof(MarkIcon));

    }
}
