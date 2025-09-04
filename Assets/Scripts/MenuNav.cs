using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuNav : MonoBehaviour
{
    public Button loadGameButton;

    [Header("Volume")]
    [SerializeField] private Slider slider;
    public AudioMixer mixer;

    private GameManager gameMan;

    private void Start()
    {
        if (!SaveSystem.SaveFileExists())
        {
            loadGameButton.interactable = false;
        }
        
        gameMan = FindObjectOfType<GameManager>();
    }

    public void StartGame(bool isNewGame)
    {
        if (!isNewGame)
        {
            gameMan.LoadGame();
        }
        else
        {
            SaveSystem.DeleteSaveFile();
            gameMan.ResetData();
        }

        SceneManager.LoadScene((int)SceneIndexes.COMBAT);
    }

    public void SaveGame()
    {
        gameMan.SaveGame();

        QuitGame();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }
}
