using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneDragDrop : MonoBehaviour
{
    bool ObjectHeld = false;
    Vector3 Mousepos;
    Vector3 Mouseposw;
    bool onCharger;
    Vector3 initialPos;

    public PhoneChargeController phoneChargeController;
    void Start()
    {
        onCharger = false;
        initialPos = gameObject.transform.localPosition;
    }

    void Update()
    {
        if (ObjectHeld == true)
        {
            // if we are clicking on the object, in this case the iPhone then allow it to move with mouse.

            Mousepos = Input.mousePosition;
            Mouseposw = Camera.main.ScreenToWorldPoint(Mousepos);
            this.gameObject.transform.localPosition = new Vector3(Mouseposw.x, Mouseposw.y, 0);
        }
    }

    void OnMouseDown()
    {
        // Called when mouse is pressed over an element
        if (Input.GetMouseButtonDown(0))
        {
            ObjectHeld = true;  // checks for left mouse click on the object 
        }
    }

    private void OnMouseUp()
    {
        ObjectHeld = false;
        if (onCharger)
        {
            // if the phone is on top of the charger we change it's position so it rests on the charger. The position of charger will not change so it is safe to hard code this value. 
            gameObject.transform.localPosition = new Vector3(3f, 0f, 0);

            phoneChargeController.StartCharging();
        }
        else
        {
            transform.localPosition = initialPos;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.tag == "Charger")
        {
            onCharger = true; // checks if the phone is on top of the charger
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onCharger = false;
    }
}
