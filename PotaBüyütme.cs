using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotaBüyütme : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] int suresi;
    [SerializeField] GameManager manager;

    private IEnumerator Start()
    {
        textMesh.text = suresi.ToString();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            suresi--;
            textMesh.text = suresi.ToString();

            if(suresi == 0)
            {
                gameObject.SetActive(false);
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Top"))
        {
            manager.PotaBuyut();
            ParticleSystem efekt = manager.efektler[1];
            efekt.transform.position = gameObject.transform.position;
            if (!efekt.isPlaying)
            {
                efekt.gameObject.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
