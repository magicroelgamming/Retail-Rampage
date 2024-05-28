using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] _items;

    // Start is called before the first frame update
    public void PlaySound(int item)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(_items[item]);
        }
        
    }
}
