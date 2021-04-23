using System;
using UnityEngine;

public static class Extension
{
   public static void PlaySound(this AudioSource _audio,SimpleSound simpleSound)
   {
      _audio.volume = simpleSound.volumeSound;
      _audio.clip = simpleSound.audioClip;
      _audio.Play();
   }
   public static void PlayOneShot(this AudioSource _audio,SimpleSound simpleSound)
   {
      _audio.PlayOneShot(simpleSound.audioClip,simpleSound.volumeSound);
   }
}
[Serializable]
public struct SimpleSound
{
   public AudioClip audioClip; 
   [Range(0, 1)] public float volumeSound;
}
