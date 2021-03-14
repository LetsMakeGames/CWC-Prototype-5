using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> bgm;
    public List<AudioClip> sfx;

    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(sfx[index]);
    }

    public void PlayMusic(int index)
    {

    }
}
