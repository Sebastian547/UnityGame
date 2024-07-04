
using UnityEngine;


public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;

    [SerializeField]
    GameObject options;

    [SerializeField]
    GameObject worldOptions;



    public void StartGame()
    {
        if (!GameObject.FindGameObjectWithTag("WorldOptions"))
        {
            Instantiate(worldOptions);
        }
    }

    
    public void Options()
    {
        if (!GameObject.FindGameObjectWithTag("Options"))
        {
            Instantiate(options);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void GoBack()
    {
        GameObject.Destroy(canvas);
    }

}
