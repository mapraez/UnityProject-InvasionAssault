using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast the Ship moves left/right based upon player input")] [SerializeField] float xControlSpeed = 30;
    [Tooltip("How fast the Ship moves up/down based upon player input")] [SerializeField] float yControlSpeed = 30;
    [Header("Movement Range")]
    [Tooltip("How far the Player can move left/right")] [SerializeField] float xRange = 15f;
    [Tooltip("How far the Player can move up/down")] [SerializeField] float yRange = 10f;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = 0.5f;
    [SerializeField] float positionYawFactor = 1.5f;

    [Header("Player control based tuning")]
    [SerializeField] float controlPitchFactor = -25f;
    [SerializeField] float controlRollFactor = -50f;

    [Header("Attached Weapons")]
    [Tooltip("Add all Player Weapons here")] [SerializeField] GameObject[] lasers;

    [Header("Attributes")]
    [SerializeField] GameObject laserPowerNotYetImplemented;


    float xThrow;
    float yThrow;

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        float xOffset = xThrow * Time.deltaTime * xControlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = Input.GetAxis("Vertical");
        float yOffset = yThrow * Time.deltaTime * yControlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }

    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
