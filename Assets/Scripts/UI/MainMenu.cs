using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject image;
    public Image imageCom;
    public float time;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void PlayGame()
    {
        Debug.Log("Play");
        image.SetActive(true);
        imageCom = image.GetComponent<Image>();
        UIManager.instance.ChangeSceneTo(1, imageCom, time);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
