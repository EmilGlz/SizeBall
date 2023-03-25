using TMPro;
using UnityEngine;

public class MainMenuScreen : ScreenView
{
    private readonly string defaulttitleText = $"Play level {GameController.Instance.CurrentLevel}";
    private readonly string firstLeveltitleText = $"Welcome to the game. \nYou are level {GameController.Instance.CurrentLevel}\nLets play!";
    public MainMenuScreen(RectTransform rectTransform) : base(rectTransform, true)
    {
        InitTitle();
    }

    public override void Show(bool showAnimation = true)
    {
        if (Active)
            return;
        var animTime = showAnimation ? AnimTime: 0f;
        rectTransform.gameObject.SetActive(true);
        var title = GuiUtils.FindGameObject("Title", rectTransform.gameObject);
        var playButton = GuiUtils.FindGameObject("PlayButton", rectTransform.gameObject);
        GuiUtils.DoExtendAnimation(title, animTime);
        GuiUtils.DoExtendAnimation(playButton, animTime);
        base.Show(showAnimation);
    }

    private void InitTitle()
    {
        var title = GuiUtils.FindGameObject("Title", rectTransform.gameObject);
        title.GetComponent<TMP_Text>().text = GameController.Instance.CurrentLevel == 0 ? firstLeveltitleText : defaulttitleText;
    }

    public override void Close(bool showAnimation = true)
    {
        if (!Active)
            return;
        var animTime = showAnimation ? AnimTime : 0f;
        rectTransform.gameObject.SetActive(true);
        var title = GuiUtils.FindGameObject("Title", rectTransform.gameObject);
        var playButton = GuiUtils.FindGameObject("PlayButton", rectTransform.gameObject);
        GuiUtils.DoCloseAnimation(title, animTime);
        GuiUtils.DoCloseAnimation(playButton, animTime);
        base.Close(showAnimation);
    }
}
