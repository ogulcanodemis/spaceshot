using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("GENERAL SETUP SETTINGS")]
    [SerializeField] float controlSpeed= 35f;
    [Tooltip("how far player moves horizontally")] [SerializeField] float Xrange = 25f; // ekran sýnýr
    [Tooltip("how far player moves vertically")] [SerializeField] float Yrange = 12f; // ekran sýnýr
    
    [Header("SCREEN POSITION")]
    [SerializeField] float positionPitchFactor= -1.5f; // X ekseni 
    [SerializeField] float positionYawFactor = 1f; //  Y ekseni
    
    [Header("PLAYER INPUT POSITION")]
    [SerializeField] float controlPitchFactor = -1.5f; //  
    [SerializeField] float controlRollFactor = 20f; // 
    
    [Header("LASER GUN ARRAY")]
    [Tooltip("add lasers here")] [SerializeField] GameObject[] lasers ; // sað sol mouse için dizi ateþleme


    float yThrow;
    float xThrow;

    void Update()
    {
        processTransition();
        processRotation();
        processFiring();

    }

    void processRotation() 
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor; //yukarý aþaðý rotation Y
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToControlThrow  *pitchDueToPosition ; // x ekseni 

        float yaw = transform.localPosition.x * positionYawFactor; // sað sol rotation X
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw , roll); // açý berlirleme  X Y Z 
    }

    void processTransition()
    {
         xThrow = Input.GetAxis("Horizontal");
         yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXpos = transform.localPosition.x + xOffset;
        float clampedXpos = Mathf.Clamp(rawXpos, -Xrange, Xrange); // ekran

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYpos = transform.localPosition.y + yOffset;
        float clampedYpos = Mathf.Clamp(newYpos, -Yrange, Yrange); // ekran

        transform.localPosition = new Vector3(clampedXpos, clampedYpos, transform.localPosition.z); // ekrandan çýkmama
    }

    void processFiring() 
    {
        if (Input.GetButton("Fire1")) // tuþa basýnca ateþleme
        {
            SetActiveLaser(true);     
        }
        else
        {
            SetActiveLaser(false);
        }
    
    }

     void SetActiveLaser( bool isActive)
    {
        foreach (var laser in lasers)  // particle sistemde emissionu açma kapatma
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission; 
            emissionModule.enabled = isActive;
        }   
    }

    
}
