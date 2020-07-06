using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriar : MonoBehaviour
{
    public AudioClip hitAudio;
    // Start is called before the first frame update

    public void PlayAudio() {

        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
    }
}
