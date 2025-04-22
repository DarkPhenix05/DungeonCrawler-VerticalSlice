using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    private Player player;

    public bool GameIsPaused = false;
    public bool canPause = true;

    public GameObject PauseMenuUI;
    public GameObject HUD;

    [Header("Fade")]
    public GameObject image;
    public Image imageCom;
    public float time;

    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<Player>().GetComponent<Player>(); ;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (image == null)
            imageCom = UIManager.instance.GetTransitionImage();
    }

    void Update()
    {
        if (canPause == true && !player._isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                    Resume();
                else
                    Pause();
            }
        }
    }

    public void Resume()
    {
        imageCom.DOFade(0, 0.15f).SetUpdate(true);
        PauseMenuUI.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        imageCom.DOFade(0.85f, 0.15f).SetUpdate(true);
        PauseMenuUI.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void GoToMainMenu()
    {
        GameIsPaused = false;
        canPause = true;
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    public void PlayAgain()
    {
        image.SetActive(true);
        imageCom = image.GetComponent<Image>();
        UIManager.instance.ChangeSceneTo(1, imageCom, time);
    }
}
