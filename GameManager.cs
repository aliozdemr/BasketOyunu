using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject platform;
    [SerializeField] Image[] bosHedefGörselleri;
    [SerializeField] Sprite basketAtildiGörseli;
    [SerializeField] int hedefSayisi;
    public int basketSayisi;
    public GameObject fileAlti;
    public GameObject basketController;
    public GameObject kontrol;
    public GameObject pota;
    public GameObject potaBuyume;
    public GameObject[] ozellikNoktalari;
    public AudioSource[] sesler;
    public ParticleSystem[] efektler;
    [SerializeField] GameObject[] paneller;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] GameObject arkaPlan;
    [SerializeField] Sprite[] arkaPlanlar;
    public TMP_Dropdown dropDown;
    float ParmakPozX;

    void Start()
    {
        arkaPlan.GetComponent<SpriteRenderer>().sprite = arkaPlanlar[PlayerPrefs.GetInt("ArkaPlanlar")];
        level.text = (SceneManager.GetActiveScene().buildIndex).ToString();
        for (int i = 0; i < hedefSayisi; i++)
        {
            bosHedefGörselleri[i].gameObject.SetActive(true);
        }
        Invoke("OzellikOlusturma", 15f);
    }

    void Update()
    {
        if (basketSayisi == hedefSayisi)
        {
            Invoke("Kazanma", 0.15f);

            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex+1);
            
        }
        if(Time.timeScale != 0)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.x, 10));

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        ParmakPozX = touchPosition.x - platform.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if(touchPosition.x - ParmakPozX > -1.45f && touchPosition.x - ParmakPozX < 1.45f)
                        {
                            platform.transform.position = Vector3.Lerp(platform.transform.position, new Vector3(touchPosition.x - ParmakPozX,
                            platform.transform.position.y, platform.transform.position.z), 1f);
                        }
                        break;
                }
            }



            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (platform.transform.position.x > -1.45f)
                    platform.transform.position = Vector3.Lerp(platform.transform.position, new Vector3(platform.transform.position.x - 3f,
                        platform.transform.position.y, platform.transform.position.z), 0.05f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (platform.transform.position.x < 1.45f)
                    platform.transform.position = Vector3.Lerp(platform.transform.position, new Vector3(platform.transform.position.x + 3f,
                    platform.transform.position.y, platform.transform.position.z), 0.05f);
            }
        }
        
    }
    public void SpriteDegistir(int deger)
    {
        switch (deger)
        {
            case 0:
                arkaPlan.GetComponent<SpriteRenderer>().sprite = arkaPlanlar[0];
                PlayerPrefs.SetInt("ArkaPlanlar", 0);
                AyarlarKapama();
                break;
            case 1:
                arkaPlan.GetComponent<SpriteRenderer>().sprite = arkaPlanlar[1];
                PlayerPrefs.SetInt("ArkaPlanlar", 1);
                AyarlarKapama();
                break;
            case 2:
                arkaPlan.GetComponent<SpriteRenderer>().sprite = arkaPlanlar[2];
                PlayerPrefs.SetInt("ArkaPlanlar", 2);
                AyarlarKapama();
                break;
        }
        
        
    }
    public void SeviyeBelirleme(string buttonTag)
    {
        if ( buttonTag == "Seviye1")
        {
            PlayerPrefs.SetFloat("Zorluk", 1f);
        }
        else if (buttonTag == "Seviye2")
        {
            PlayerPrefs.SetFloat("Zorluk", 1.5f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void AyarlarKapama()
    {
        paneller[0].SetActive(true);
        paneller[1].SetActive(true);
        paneller[5].SetActive(false);
    }
    public void AyarlarAcma()
    {
        paneller[0].SetActive(false);
        paneller[1].SetActive(false);
        paneller[5].SetActive(true);
    }
    public void Cikis()
    {
        Application.Quit();
    }
    public void SiradakiLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        PlayerPrefs.SetInt("Level", (SceneManager.GetActiveScene().buildIndex + 1));
        Time.timeScale = 1;
    }
    public void TekrarBaslatma()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void DurdurmaPanelAcma()
    {
        paneller[0].SetActive(false);
        paneller[1].SetActive(false);
        paneller[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void DevamEtmeButonu()
    {
        for (int i = 0; i < paneller.Length; i++)
        {
            paneller[i].SetActive(false);
        }
        paneller[0].SetActive(true);
        paneller[1].SetActive(true);
        Time.timeScale = 1;
    }
    public void OzellikOlusturma()
    {
        int sayi = Random.Range(0, ozellikNoktalari.Length - 1);
        potaBuyume.transform.position = ozellikNoktalari[sayi].transform.position;
        potaBuyume.gameObject.SetActive(true);
    }
    

    public void Kaybetme()
    {
        paneller[0].SetActive(false);
        paneller[1].SetActive(false);
        paneller[4].SetActive(true);

        sesler[1].Play();
        sesler[4].Stop();
        Time.timeScale = 0;
    }
    void Kazanma()
    {
        paneller[0].SetActive(false);
        paneller[1].SetActive(false);
        paneller[3].SetActive(true);
        if (!sesler[0].isPlaying)
        {
            sesler[0].Play();
            sesler[4].Stop();
            Time.timeScale = 0;
        }
    }
    

    public void BasketSayisiArtirma()
    {
        basketSayisi++;
        bosHedefGörselleri[basketSayisi - 1].sprite = basketAtildiGörseli;
    }

    public void PotaBuyut()
    {
        sesler[2].Play();
        pota.transform.localScale = new Vector3(55f, 55f, 55f);
    }
}
