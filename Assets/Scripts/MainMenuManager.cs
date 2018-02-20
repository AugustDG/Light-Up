using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace AMPStudios
{
  public class MainMenuManager : MonoBehaviour //Manages basic menu interactions and scene loading
  {
    Animator Animator; //reference to the menu animator
    public CinemachineVirtualCamera virtualCamera; //reference to the global cinemachine camera
    SwipeControls swipeControls; //reference to the touch controller

    public bool isIN; //manages the states of animation of the credits and options menu
    public bool isQUIT = true; //manages the states of animation of the quit menu
    public bool backLeaves = true; //decides if the back button quits the application

    bool isLoaded; //holds the state of the next loaded scene
    bool isMENU = true; //holds the state of the main menu
    [SerializeField] bool isOptions; //holds the state of the options menu
    [SerializeField] bool isCredits; //holds the state of the credits menu
    [SerializeField] bool isQuitOpen; //holds the state of the quit menu

    void Awake()
    {
      Input.backButtonLeavesApp = false; //decides if the back button quits the application
      Animator = GetComponent<Animator>(); //assigns the animator reference to the animator
      swipeControls = GetComponent<SwipeControls>(); //assigns the the touch controller reference to the touch controller
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape)) //checks if the back button has been pressed: closes the menu and switches its state
      {
        PlayMenu();
        isMENU = true;
      }

      //enables correct swipe controls for the main menu
      #region Swipe Controls Main menu
      if (!isOptions && !isCredits) 
      {
        if (swipeControls.swipeLeft)
        {
          OptionsMenu();
        }
        if (swipeControls.swipeRight)
        {
          CreditsMenu();
        }
      }
      else if (isOptions && !isCredits)
      {
        if (swipeControls.swipeRight)
        {
          OptionsMenu();
        }
      }
      else if (!isOptions && isCredits)
      {
        if (swipeControls.swipeLeft)
        {
          CreditsMenu();
        }
      }

      if (!isOptions && !isCredits)
      {
        if (!isQuitOpen)
        {
          if (swipeControls.swipeUp)
          {
            ButtonQuit();
          }
        }
        else if (isQuitOpen)
        {
          if (swipeControls.swipeDown)
          {
            ButtonQuit();
          }
        }
      }
      #endregion 
    }

    public void PlayMenu()
    {
      isIN = !isIN; //switches the state of the play menu

      Animator.SetBool("isMENU", isIN); //switches the state of the main menu
      Animator.SetBool("isNMENU", !isIN);

      if (!isLoaded && isMENU) //checks if the menu has already been loaded and if the command has already been issued, then loads the next scene
      {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        isLoaded = true;
        isMENU = false;
      }
      else if (isLoaded && !isMENU) //checks if the menu has already been loaded and if the command has already been issued, then loads the next scene
      {
        SceneManager.UnloadSceneAsync(1);
        isLoaded = false;
        isMENU = true;
      }

      if (SceneManager.sceneCount == 2) //disables and enables the virtual camera according to the current playing scene
      {
        virtualCamera.gameObject.SetActive(false);
        Camera.main.orthographicSize = 5;
      }else
      {
        virtualCamera.gameObject.SetActive(true);
        Camera.main.orthographicSize = 10;
      }
    }

    public void OptionsMenu()
    {     
      isIN = !isIN; //switches the state of the options menu

      Animator.SetBool("OisIN", isIN); //changes the according animator parameters to enable or disable the menu by animation
      Animator.SetBool("OisOUT", !isIN);

      isOptions = !isOptions; // inverses the state of the options menu
    }

    public void CreditsMenu()
    {
      isIN = !isIN; //switches the state of the credits menu

      Animator.SetBool("CisIN", isIN); //changes the according animator parameters to enable or disable the menu by animation
      Animator.SetBool("CisOUT", !isIN);
      isCredits = !isCredits; // inverses the state of the credits menu
    }

    //takes care of quitting the app
    #region Quit Mechanics
    public void ButtonQuit()
    {
      isQuitOpen = !isQuitOpen; // inverses the state of the credits menu
      isQUIT = !isQUIT; //switches the state of the quit menu

      Animator.SetBool("isQUIT", isQUIT); //changes the according animator parameters to enable or disable the menu by animation
      Animator.SetBool("isNQUIT", !isQUIT);
    }

    public void YesQuit()
    {
      Application.Quit(); //quits the app, if the user has confirmed his choice
    }

    public void NoQuit()
    {
      isQuitOpen = !isQuitOpen; // inverses the state of the credits menu
      isQUIT = !isQUIT; //switches the state of the quit menu

      Animator.SetBool("isQUIT", isQUIT); //changes the according animator parameters to enable or disable the menu by animation
      Animator.SetBool("isNQUIT", !isQUIT);
    }
    #endregion
  }
}