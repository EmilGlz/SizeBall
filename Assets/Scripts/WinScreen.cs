using UnityEngine;

public class WinScreen : ScreenView
{
    public WinScreen(RectTransform rectTransform) : base(rectTransform)
    {
    }
    
    public override void Show(bool showAnimation = true)
    {
        if (Active)
            return;
        var animTime = showAnimation ? 0.5f : 0f;
        rectTransform.gameObject.SetActive(true);
        var title = GuiUtils.FindGameObject("Title", rectTransform.gameObject);
        var nextButton = GuiUtils.FindGameObject("NextButton", rectTransform.gameObject);
        GuiUtils.DoExtendAnimation(title, animTime);
        GuiUtils.DoExtendAnimation(nextButton, animTime);
        base.Show(showAnimation);
    }

    public override void Close(bool showAnimation = true)
    {
        if (!Active)
            return;
        var animTime = showAnimation ? 0.5f : 0f;
        rectTransform.gameObject.SetActive(true);
        var title = GuiUtils.FindGameObject("Title", rectTransform.gameObject);
        var nextButton = GuiUtils.FindGameObject("NextButton", rectTransform.gameObject);
        GuiUtils.DoCloseAnimation(title, animTime);
        GuiUtils.DoCloseAnimation(nextButton, animTime);
        base.Close(showAnimation);
    }
}
