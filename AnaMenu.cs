using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenu : MonoBehaviour
{
    [SerializeField] Slider slider;

    IEnumerator Start()
    {
        slider.maxValue = 2f;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
        if (PlayerPrefs.HasKey("Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            /*SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));*/
        }

        if (!PlayerPrefs.HasKey("Zorluk"))
        {
            PlayerPrefs.SetFloat("Zorluk", 1f);
        }
        if (!PlayerPrefs.HasKey("ArkaPlanlar"))
        {
            PlayerPrefs.SetInt("ArkaPlanlar", 0);
        }

    }

    private void Update()
    {
        slider.value = Time.time;
    }
}
