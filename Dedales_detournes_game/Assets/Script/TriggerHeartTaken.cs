using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHeartTaken : MonoBehaviour
{
    public GameObject player;
    public AudioSource audiosource;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            audiosource.Play();
            PlayerPV.HeartTaken();
            this.gameObject.SetActive(false);
        }
    }
}
