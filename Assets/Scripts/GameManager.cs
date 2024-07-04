using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject winText;
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().playerDead += GameEnd;
        boss.GetComponent<Boss>().gameWin += GameWin;
    }



    void GameEnd()
    {
        SceneManager.LoadScene(0);
    }

    void GameWin()
    {
        StartCoroutine(WaitForCredits());
    }

    IEnumerator WaitForCredits()
    {
        Instantiate(winText);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);

    }
}
