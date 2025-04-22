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

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
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
}
