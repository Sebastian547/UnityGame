using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using Unity.VisualScripting;

public class WorldOptionsMenu : MonoBehaviour
{
    [SerializeField]
    Dropdown dropDownDifficulty;
    
    [SerializeField]
    Dropdown dropDownMapSize;

    [SerializeField]
    InputField inputFieldSeed;
    void Start()
    {
        
        dropDownDifficulty.onValueChanged.AddListener(DifficultyChange);

        dropDownMapSize.onValueChanged.AddListener(MapSizeChange);

        inputFieldSeed.onValueChanged.AddListener(MapSeedChange);
    }

    

    public void CreateWorld()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    void MapSizeChange(int value)
    {
        switch (value)
        {
            case 0: //Small
                PlayerPrefs.SetInt("MapSize", 0);
                break;

            case 1: //Medium
                PlayerPrefs.SetInt("MapSize", 1);
                break;

            case 2: //Big
                PlayerPrefs.SetInt("MapSize", 2);
                break;
        }
    }   
    void DifficultyChange(int value)
    {
        switch (value)
        {
            case 0: //Easy
                PlayerPrefs.SetInt("Difficulty", 0);
                break;

            case 1: //Medium
                PlayerPrefs.SetInt("Difficulty", 1);
                break;

            case 2: //Hard
                PlayerPrefs.SetInt("Difficulty", 2);
                break;
        }
    }

    void MapSeedChange(string seed)
    {
        

        StringBuilder sb = new StringBuilder();


        for (int i = 0; i < seed.Length; i++) 
        {
            
            if (char.IsDigit(seed[i]))
            {
                sb.Append(seed[i]);
            }   
            else
            { 
            sb.Append( ((int)seed[i])%10 ); /// Modulo for same lenght of string, it will be allways 1 digit
            }; 
        }

        PlayerPrefs.SetInt("MapSeed", int.Parse(sb.ToString()));
    }
}
