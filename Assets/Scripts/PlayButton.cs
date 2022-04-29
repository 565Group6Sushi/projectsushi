using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private float loadDelay = 1f;
    public void PlayGame ()
    {
        FindObjectOfType<AudioManager>().Play("ClickButton");
        Invoke("NextScene", loadDelay);
    }

    private void NextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
