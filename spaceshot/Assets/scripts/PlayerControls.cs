using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float controlSpeed= 35f; 
    [SerializeField] float Xrange = 25f; // ekran sýnýr
    [SerializeField] float Yrange = 12f; // ekran sýnýr

    [SerializeField] float positionPitchFactor= -1.5f; // X ekseni 
    [SerializeField] float controlPitchFactor = -1.5f; // 
    [SerializeField] float positionYawFactor = 1f; //  Y ekseni
    [SerializeField] float controlRollFactor = 20f; //  Z ekseni
    float yThrow;
    float xThrow;

    void Update()
    {
        processTransition();
        processRotation();
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
}
