using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour, ISelectHandler

{
    public AudioClip click;

    private Button button { get { return GetComponent<Button>(); } }

    private AudioSource source { get { return GetComponent<AudioSource>(); } }


    // Use this for initialization
    void Start()

    {
        gameObject.AddComponent<AudioSource>();
        source.clip = click;
        source.playOnAwake = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (this.gameObject == button)
        {
            PlaySound();
        }
    }

    // Update is called once per frame
    void PlaySound()

    {
        source.PlayOneShot(click);
    }

}
