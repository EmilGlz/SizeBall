using UnityEngine;

public class ScreenView
{
    protected readonly RectTransform rectTransform;
    protected readonly float AnimTime = 0.5f;
    public bool Active { get; private set; }

    public ScreenView(RectTransform rectTransform, bool active = false)
    {
        this.rectTransform = rectTransform;
        rectTransform.gameObject.SetActive(active);
        Active = active;
    }
    public virtual void Show(bool showAnimation = true)
    {
        Active = true;
    }

    public virtual void Close(bool showAnimation = true)
    {
        Active = false;
        GameController.DelayAction(() => {
            rectTransform.gameObject.SetActive(false);
        }, (int)(AnimTime * 1000));
    }
}
