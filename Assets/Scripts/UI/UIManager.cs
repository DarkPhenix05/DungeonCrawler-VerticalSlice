using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("SCENE")]
    [SerializeField] private int currentScene = 0;

    [Header("FADE")]
    public Image fadeImage;
    public float fadeTime;

    [Header("HUD")]
    public GameObject goHUD;
    public GameObject goDS;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);

        GetRefs();
        goHUD.SetActive(false);
    }

    public void ChangeSceneTo(int scene, Image image, float time)
    {
        FadeOut(image, time);
        currentScene = scene;
        FadeIn(image, time);
    }

    public void FadeIn(Image image, float time)
    {
        fadeImage = image;
        fadeTime = time;

        fadeImage.DOFade(1, fadeTime).OnComplete(SceneChange);
    }

    public void FadeOut(Image image, float time)
    {
        fadeImage = image;
        fadeTime = time;

        fadeImage.DOFade(1, fadeTime);
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(1);
        GetTransitionImage();
    }

    public Image GetTransitionImage()
    {
        fadeImage = GameObject.FindGameObjectWithTag("Transition").GetComponent<Image>();
        return fadeImage;
    }

    private void GetRefs()
    {
        fadeImage = GetTransitionImage();
        goHUD = GameObject.FindGameObjectWithTag("HUD");
        goHUD = GameObject.FindGameObjectWithTag("GOUI");
    }

    public void DeathScreen()
    {
        GetRefs();
        fadeImage.DOFade(0.85f, 0.15f).SetUpdate(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        goHUD.gameObject.SetActive(false);
        goDS.gameObject.SetActive(true);
    }
}
