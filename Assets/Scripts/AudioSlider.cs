using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AMPStudios
{
  public class AudioSlider : MonoBehaviour //Manages basic audio volume, as well as play/pause and next/previous
  {
    [SerializeField]bool isPlaying = true; // holds the sate of the audio source
    public float songPosition; //songs position when paused
    int currentSong = 0;
    Slider slider; //slider to control the volume
    public AudioSource audioSource; //reference to the audiosource for the music

    [Space(10)]
    [Header("Song Details")]
    public AudioClip[] mainSongs; //array for all the possible songs
    public int songNumber = 0; //which song to play

    private void Awake()
    {
      slider = GetComponent<Slider>(); //assigns the slider variable to the Slider component

      if (!PlayerPrefs.HasKey("AudioLevel")) //checks if the user has already saved an audio level
      {
        PlayerPrefs.SetFloat("AudioLevel", slider.value); //if not, it creates one with the default value (0)
      }
      else
      {
        slider.value = PlayerPrefs.GetFloat("AudioLevel"); //and if yes, gets the saved value and assigns to the slider
      }

      audioSource.volume = slider.value; //sets the volume of the audio to the value of the slider

      PlayerPrefs.Save(); //saves all modifications
    }

    public void ChangeVolume() //is called everytime the slider changes value
    {
      audioSource.volume = slider.value/10; //sets the volume of the audio to the value of the slider

      PlayerPrefs.SetFloat("AudioLevel", slider.value); //saves the selected audio level to player prefs
      PlayerPrefs.Save(); //saves all modifications
    }

    public void PlaySong(int songNumber) //method to play and pause current song
    {
      //plays the current song
      songNumber = currentSong;

      //assigns the current song from the array
      audioSource.clip = mainSongs[songNumber];

      // play/pause logic
      if (!isPlaying)
      {
        audioSource.time = songPosition; // set song position
        audioSource.Play(); // plays the song
        Debug.Log("Song unpaused! " + songPosition);
      }
      else if (isPlaying)
      {
        songPosition = audioSource.time; // save song position
        Debug.Log("Song paused! " + songPosition);
        audioSource.Stop(); // stops the song
      }
      isPlaying = !isPlaying; // inverses the state of the audio source
    }

    public void NextSong() //method to go to next song
    {
      if (currentSong < mainSongs.Length-1)
      {
        // go forward in the song list and plays it
        currentSong += 1;
        audioSource.clip = mainSongs[currentSong];
        audioSource.Play();
      }else
      {
        // if reached end of songs, then restarts it
        currentSong = 0;
        audioSource.clip = mainSongs[currentSong];
        audioSource.Play();
      }
    }

    public void PreviousSong() //method to go to previous song
    {
      if (currentSong > 0)
      {
        // go back in the song list and plays it
        currentSong -= 1;
        audioSource.clip = mainSongs[currentSong];
        audioSource.Play();
      }
      else
      {
        // if reached end of songs, then restarts it
        currentSong = mainSongs.Length - 1;
        audioSource.clip = mainSongs[currentSong];
        audioSource.Play();
      }
    }
  }
}