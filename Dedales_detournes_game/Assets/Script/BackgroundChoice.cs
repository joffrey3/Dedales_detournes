using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackgroundChoice : MonoBehaviour
{
    public Sprite[] images = new Sprite[4];
    Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = images[Random.Range(0, images.Length)];
    }
}
