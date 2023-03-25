using TMPro;
using UnityEngine;

public class DefeatScreen : ScreenView
{
    public DefeatScreen(RectTransform rectTransform) : base(rectTransform)
    {
    }

    public override void Show(bool showAnimation = true)
    {
        if (Active)
            return;
        var animTime = showAnimation ? 0.5f : 0f;
        rectTransform.gameObject.SetActive(true);
        var title = GuiUtils.FindGameObject("Title", rectTransform.gameObject);
        title.GetComponent<TMP_Text>().text = $"Failed. Lets try again!";
        var retryButton = GuiUtils.FindGameObject("RetryButton", rectTransform.gameObject);
        GuiUtils.DoExtendAnimation(title, animTime);
        GuiUtils.DoExtendAnimation(retryButton, animTime);
        base.Show(showAnimation);
    }

    public override void Close(bool showAnimation = true)
    {
        if (!Active)
            return;
        var animTime = showAnimation ? 0.5f : 0f;
        rectTransform.gameObject.SetActive(true);
        var title = GuiUtils.FindGameObject("Title", rectTransform.gameObject);
        var retryButton = GuiUtils.FindGameObject("RetryButton", rectTransform.gameObject);
        GuiUtils.DoCloseAnimation(title, animTime);
        GuiUtils.DoCloseAnimation(retryButton, animTime);
        base.Close(showAnimation);
    }
}
