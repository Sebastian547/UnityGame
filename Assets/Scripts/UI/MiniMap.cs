using System.Collections;
using UnityEngine;
using UnityEngine.Device;

public class MiniMap : MonoBehaviour
{
    
    int orginalSize;
    Vector3 orginalPosition;
    Camera cam;
   
    void Start()
    {
        
        cam = gameObject.GetComponent<Camera>();

        
        if (PlayerPrefs.HasKey("MapSize"))
        {
            int mapSize = PlayerPrefs.GetInt("MapSize");

            switch (mapSize)
            {
                case 0: //Small

                    orginalSize = 50;
                    orginalPosition = new Vector3(50, 50, -11);

                    //Debug.Log(PlayerPrefs.GetInt("MapSize"));
                    break;
                case 1: //Medium
                    orginalSize = 100;
                    orginalPosition = new Vector3(100, 100, -11);

                    //Debug.Log(PlayerPrefs.GetInt("MapSize"));
                    break;
                case 2: //Big
                    orginalSize = 150;
                    orginalPosition = new Vector3(150, 150, -11);

                    //Debug.Log(PlayerPrefs.GetInt("MapSize"));
                    break;
            }

            gameObject.transform.position = orginalPosition;
            cam.orthographicSize = orginalSize;
        }

        //StartCoroutine(Freeze());
    }


    /*
    IEnumerator Freeze()
    {
        while (true)
        {
            cam.enabled = false;
            yield return new WaitForSecondsRealtime(2);
            Texture2D tex = new Texture2D (cam.targetTexture.width, cam.targetTexture.height, TextureFormat.ARGB32, false);
            cam.enabled = true;
        }
    }
    */
}
