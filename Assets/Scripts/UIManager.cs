using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    [SerializeField] Transform childImage;
    [SerializeField] RectTransform winPanel;
    [SerializeField] RectTransform defeatPanel;
    [SerializeField] RectTransform mainMenuPanel;
    WinScreen winScreen;
    DefeatScreen defeatScreen;
    MainMenuScreen mainMenuScreen;
    private readonly float minCircleScale = 0.2f;
    private void Start()
    {
        GameController.Instance.OnLevelUpdated += UpdateCircle;
        winScreen = new WinScreen(winPanel);
        defeatScreen = new DefeatScreen(defeatPanel);
        mainMenuScreen = new MainMenuScreen(mainMenuPanel);
        GameController.DelayActionForOneFrame(() => { 
            mainMenuScreen.Show();
        });
    }

    public void UpdateCircle(int level)
    {
        var scale = (level / (float)GameController.Instance.MaxPlayerLevel) * (1 - minCircleScale) + minCircleScale;
        childImage.localScale = Vector3.one * scale;
    }

    public void ShowWinScreen()
    {
        winScreen.Show();
    }

    public void ShowLoseScreen()
    {
        defeatScreen.Show();
    }

    public void NextLevelPressed()
    {
        winScreen.Close();
        GameController.DelayAction(() => {
            GameController.Instance.CurrentLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }, 500);
    }

    public void RetryPressed()
    {
        defeatScreen.Close();
        GameController.DelayAction(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }, 500);
    }

    public bool DefeatScreenActive => defeatScreen.Active;
    public bool WinScreenActive => winScreen.Active;

    public void PlayButtonPressed()
    {
        mainMenuScreen.Close();
    }

    private void OnDisable()
    {
        GameController.Instance.OnLevelUpdated -= UpdateCircle;
    }
}
