using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] AudioSource ses;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket"))
        {
            if(!manager.sesler[0].isPlaying | !manager.sesler[1].isPlaying)
            {
                manager.sesler[3].Play();
            }
            ParticleSystem efekt = manager.efektler[0];
            efekt.transform.position = gameObject.transform.position;
            if (!efekt.isPlaying)
            {
                efekt.gameObject.SetActive(true);
            }
            manager.fileAlti.SetActive(false);
            Invoke("TekrarAcmaFileAlti", 0.3f);
            manager.BasketSayisiArtirma();
        }
       
        if (other.CompareTag("FileAlti"))
        {
            manager.basketController.SetActive(false);
            manager.kontrol.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Kontrol"))
        {
            manager.basketController.SetActive(true);
            manager.fileAlti.SetActive(true);
            manager.kontrol.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AltCubuk"))
        {
            manager.Kaybetme();
        }
        ses.Play();
        
    }

    void TekrarAcmaFileAlti()
    {
        manager.fileAlti.SetActive(true);
    }
    void TekrarAcmaBasket()
    {
        manager.basketController.SetActive(true);
        
    }
}
