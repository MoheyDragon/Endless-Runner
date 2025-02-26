using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class DestroyOnAudioEnd : MonoBehaviour
{
    IEnumerator Start()
    {
        if (GetComponent<AudioSource>().loop)
        {
            Destroy(this);
            yield break;
        }
        yield return new WaitForSeconds( GetComponent<AudioSource>().clip.length);
        SoundsManager.Singleton.OnClipFinished(GetComponent<AudioSource>());
        Destroy(gameObject);
    }
}
