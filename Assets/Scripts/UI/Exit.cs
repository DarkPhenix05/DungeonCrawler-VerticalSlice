using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public GameObject hud;
    public GameObject GCUI;

    [Header("Fade")]
    public GameObject image;
    public Image imageCom;
    public float time;

    private void Awake()
    {
        if (image == null)
            imageCom = UIManager.instance.GetTransitionImage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            imageCom.DOFade(0.85f, 0.15f).SetUpdate(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            hud.SetActive(false);
            GCUI.SetActive(true);
        }
    }
}
