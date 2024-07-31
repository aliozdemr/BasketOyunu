using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKuvvet : MonoBehaviour
{
    [SerializeField] float aci;
    [SerializeField] float kuvvet;
    [SerializeField] GameManager manager;


    private void OnCollisionEnter(Collision collision)
    {
        
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(aci, 90, 0) * kuvvet/10000 * (PlayerPrefs.GetFloat("Zorluk")), ForceMode.Force);
    }
}
