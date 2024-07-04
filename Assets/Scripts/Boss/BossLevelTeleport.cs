using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelTeleport : MonoBehaviour
{
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(2); // Loads scene with index 2
        }
    }
}
