
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    Transform player;


    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public Color backgroundColor = Color.black;


    private void Start()
    {
        Camera mainCamera = GetComponent<Camera>();

        if (mainCamera != null)
        {
            mainCamera.backgroundColor = backgroundColor;
        }
    }
    private void LateUpdate()
    {
        if(player != null) 
        { 
            transform.position = player.position + offset;
        }
    }
}
