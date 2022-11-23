using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneChargeController : MonoBehaviour
{
    public GameObject BatteryIcon, ChargingIcon, ProgressBar,ChargingText;
    public SpriteRenderer AppleLogo;
    public Sprite[] BatteryChargingSprites;
    public Sprite FullBattery;

     float animateTime = 0.5f;    // the duration after which the battery animation will change
     int animateCycle = 3;        // how many times to play the animation before full battery

    public void StartCharging()
    {
        ChargingText.SetActive(false);
        BatteryIcon.SetActive(true);
        ChargingIcon.SetActive(true);
        GetComponent<AudioSource>().Play();
        StartCoroutine(BatteryChargingAnimation());
    }

    IEnumerator BatteryChargingAnimation()
    {
        
        int spriteCount = 0;
        int cycleCount =0;
        while(cycleCount < animateCycle)
        {
            yield return new WaitForSeconds(animateTime);
            if (spriteCount >=BatteryChargingSprites.Length)
            {
                // we cycle through the battery sprites to show battery animation. After each complete cycle we increase the cycle count
                spriteCount = 0;
                cycleCount++;
            }
            BatteryIcon.GetComponent<SpriteRenderer>().sprite = BatteryChargingSprites[spriteCount++];
        }

        BatteryIcon.GetComponent<SpriteRenderer>().sprite = FullBattery;
        yield return new WaitForSeconds(1f);
        BatteryIcon.SetActive(false);
        ChargingIcon.SetActive(false);
        AppleLogo.gameObject.SetActive(true);

        // fade in apple logo. We change the alpha value of the logo to have fade in effect. 
        Color logoColor = AppleLogo.color;
        float alphaValue = logoColor.a;
        while(AppleLogo.color.a<1)
        {
            alphaValue += 0.01f;
            logoColor.a = alphaValue;
            AppleLogo.color = logoColor;
            yield return new WaitForSeconds(0.01f);
        }
      
    }
}
