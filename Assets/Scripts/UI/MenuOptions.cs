
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField]
    Slider volumeSlider;

    [SerializeField]
    Dropdown dropDown;

    [SerializeField]
    Toggle lights;


    [SerializeField]
    Button brightadd;

    [SerializeField]
    Button brighttake;

    [SerializeField]
    Button quitToMenu;
    private void Start()
    {
        
        if (PlayerPrefs.HasKey("Light"))
        {
            if(PlayerPrefs.GetInt("Light")==0)
            {
                lights.isOn = false;
            }
            else
            {
                lights.isOn = true;
            }
        }


        volumeSlider.value = AudioListener.volume;

        volumeSlider.onValueChanged.AddListener(GameVolume);

        dropDown.onValueChanged.AddListener(ResolutionChange);

        lights.onValueChanged.AddListener(LightsControl);

        brightadd.onClick.AddListener(BrightAdd);

        brighttake.onClick.AddListener(BrightTake);

        quitToMenu.onClick.AddListener(QuitToMenu);
    }

    void GameVolume(float volume)
    {
        AudioListener.volume = volume;
    }


    private void ResolutionChange(int value)
    {


        switch (value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 1:
                Screen.SetResolution(2560, 1440, true);
                break;
            case 2:
                Screen.SetResolution(1280, 720, true);
                break;
            case 3:

                Screen.SetResolution(1920, 1080, false);
                break;
        }
        Debug.Log(Screen.resolutions);

    }

    private void LightsControl(bool onoff)
    {
        if(onoff)
        {
            PlayerPrefs.SetInt("Light", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Light", 0);
        }

        Light2D[] lights = FindObjectsOfType<Light2D>();

            foreach (Light2D light in lights)
            {
                light.enabled = onoff;
            }
        
    }


    /// Experimental for the rzutnik
    private void BrightAdd()
    {
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color *= new Color (1.25f, 1.25f, 1.25f);
        }
    }

    private void BrightTake()
    {
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            
            sprite.color *= new Color(0.75f, 0.75f, 0.75f);
        }
    }

    void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
