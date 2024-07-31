using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SonMenu : MonoBehaviour
{
    
    public void OyunuBastanBaslat()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("Level", 1);
    }
}
