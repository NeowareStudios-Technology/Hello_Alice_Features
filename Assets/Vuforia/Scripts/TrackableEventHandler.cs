//*============================================================================== 
 /* Copyright(c) 2012-2015 Qualcomm Connected Experiences, Inc.All Rights Reserved.

/* ============================================================================== */
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine.UI;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class TrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PRIVATE_MEMBERS
    private TrackableBehaviour mTrackableBehaviour;
    private bool mHasBeenFound = false;
    private bool mLostTracking;
    private float mSecondsSinceLost;

    public Button phoneButton;
    public Button emailButton;

    private bool capture;
    private bool newCapture;
    private Animator phone;
    private Animator email;

    public Button ph;
    public Button em;



    #endregion // PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        OnTrackingLost();
    }

    void Update()
    {
        // Pause the video if tracking is lost for more than two seconds
        if (mHasBeenFound && mLostTracking)
        {
            if (mSecondsSinceLost > 0.5f)
            {
                VideoPlaybackBehaviour video = GetComponentInChildren<VideoPlaybackBehaviour>();
                if (video != null &&
                    video.CurrentState == VideoPlayerHelper.MediaState.PLAYING)
                {
                    video.VideoPlayer.Pause();
                }

                mLostTracking = false;
            }

            mSecondsSinceLost += Time.deltaTime;
        }
    }

    #endregion //MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }
    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS
    private void OnTrackingFound()
    {
        /* screen caputre value
        //capture = GameObject.FindObjectOfType<screenShotSharing>().noAnimation;
       // newCapture = GameObject.FindObjectOfType<screenShotSharing>().noPhoneEmailButtons;
       // GameObject.FindObjectOfType<screenShotSharing>().currentPhone = phoneButton;
       // GameObject.FindObjectOfType<screenShotSharing>().currentEmail = emailButton;*/

        print("this is the noPhoneEmailButtons" + newCapture);

        // if there is phone button get the animator components and rest the animation
        if (phoneButton != null)
        {
            ph = phoneButton;
            phone = phoneButton.gameObject.GetComponent<Animator>();
            phone.Play("phoneAnimation", -1, 0f);
        }
        // if there is email button get the animator components and rest the animation
        if (emailButton != null)
        {
            em = emailButton;
            email = emailButton.gameObject.GetComponent<Animator>();
            email.Play("emailAnimation", -1, 0f);
        }

        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
        Collider[] colliderComponents = GetComponentsInChildren<Collider>();
        Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable Canvas
        foreach (Canvas component in canvasComponents)
        {
            component.enabled = true;
        }

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

        // Optionally play the video automatically when the target is found
        VideoPlaybackBehaviour video = GetComponentInChildren<VideoPlaybackBehaviour>();
        if (video != null && video.AutoPlay)
        {
            if (video.VideoPlayer.IsPlayableOnTexture())
            {
                VideoPlayerHelper.MediaState state = video.VideoPlayer.GetStatus();
                if (state == VideoPlayerHelper.MediaState.PAUSED ||
                    state == VideoPlayerHelper.MediaState.READY ||
                    state == VideoPlayerHelper.MediaState.STOPPED)
                {
                    // Pause other videos before playing this one
                    PauseOtherVideos(video);

                    // Play this video on texture where it left off
                    video.VideoPlayer.Play(false, video.VideoPlayer.GetCurrentPosition());
                }
                else if (state == VideoPlayerHelper.MediaState.REACHED_END)
                {
                    // Pause other videos before playing this one
                    PauseOtherVideos(video);

                    // Play this video from the beginning
                    video.VideoPlayer.Play(false, 0);
                }
            }
        }

        // if the screen is not been capture
        if (capture == false)
        {
            // active the button and play animation
            if (phoneButton != null)
            {
                phone.gameObject.SetActive(true);
                phone.Play("phoneAnimation");
            }
            if (emailButton != null)
            {
                email.gameObject.SetActive(true);
                email.Play("emailAnimation");
            }
            // if the screen is been captured diactive button
        }
        else if (capture == true)
        {
            if (phoneButton != null)
            {
                phone.gameObject.SetActive(false);
            }
            if (emailButton != null)
            {
                email.gameObject.SetActive(false);
            }
        }

        mHasBeenFound = true;
        mLostTracking = false;
    }

    private void OnTrackingLost()
    {

        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
        Collider[] colliderComponents = GetComponentsInChildren<Collider>();
        Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable Canvas
        foreach (Canvas component in canvasComponents)
        {
            component.enabled = false;
        }

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        mLostTracking = true;
        mSecondsSinceLost = 0;
    }

    // Pause all videos except this one
    private void PauseOtherVideos(VideoPlaybackBehaviour currentVideo)
    {
        VideoPlaybackBehaviour[] videos = (VideoPlaybackBehaviour[])
                FindObjectsOfType(typeof(VideoPlaybackBehaviour));

        foreach (VideoPlaybackBehaviour video in videos)
        {
            if (video != currentVideo)
            {
                if (video.CurrentState == VideoPlayerHelper.MediaState.PLAYING)
                {
                    video.VideoPlayer.Pause();
                }
            }
        }
    }
    #endregion //PRIVATE_METHODS


}