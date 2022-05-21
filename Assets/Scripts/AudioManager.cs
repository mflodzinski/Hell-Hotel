using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	public AudioClip[] audioClips;
	AudioClip previousAudioClip;
	AudioSource audioSource;
	float fadeDuration = 10f;
	float timeBeetweenClips = 5f;
	bool CR_running;


	private void Start()
    {
		audioSource = GetComponent<AudioSource>();

    }
    public void PlayMusic()
	{	
		if (previousAudioClip == null)
        {
			previousAudioClip = GetNextAudioClip();
		}
		AudioClip currentAudioClip = GetNextAudioClip();
		audioSource.clip = currentAudioClip;
		StartCoroutine("AnimateMusic");
    }

    private void Update()
    {
        if (!CR_running)
        {
			PlayMusic();
        }
    }


    AudioClip GetNextAudioClip()
    {
		int size = audioClips.Length;
		int index = Random.Range(0, size);
		if (audioClips[index] != previousAudioClip)
        {
			previousAudioClip = audioClips[index];
			return audioClips[index];
		}
		else
        {
			return GetNextAudioClip();
        }
    }

	IEnumerator AnimateMusic()
    {
		CR_running = true;
		audioSource.Play();
		float percent = 0;
		while (percent < 1)
		{
			percent += Time.deltaTime * 1 / fadeDuration;
			audioSource.volume = percent;
			yield return null;
		}
		yield return new WaitForSeconds(audioSource.clip.length - 2 * fadeDuration);

		percent = 0;
		while (percent < 1)
		{
			percent += Time.deltaTime * 1 / fadeDuration;
			audioSource.volume = 1 - percent;
			yield return null;
		}
		audioSource.Stop();
		yield return new WaitForSeconds(timeBeetweenClips);
		CR_running = false;
	}
}