using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour 
{

	private int maxWalkSpeed;
	private int maxSprintSpeed;
	private int acceleration;
	
	public bool sprint = false; 
	public float curSpeed;

    void Start () 
	{
		RefreshStats ();
		curSpeed = maxWalkSpeed;
	}

	void RefreshStats ()
	{
		acceleration = gameObject.GetComponent<playerData>().acceleration;
		maxWalkSpeed = gameObject.GetComponent<playerData>().maxWalkSpeed;
		maxSprintSpeed = gameObject.GetComponent<playerData>().maxSprintSpeed;
	}

    void Update()
    {
        sprint = Input.GetButton("Sprint") && Input.GetButton("Forward");

        if (Input.GetButton("Forward"))
        {
            if (!sprint)
            {
                transform.Translate(Vector3.forward * maxWalkSpeed * Time.deltaTime); //Walk Forward
            }
            else
            {
                if (curSpeed <= maxSprintSpeed)
                {
                    if (curSpeed + (acceleration * Time.deltaTime) < maxSprintSpeed)
                    {
                        curSpeed += acceleration * Time.deltaTime;
                        transform.Translate(Vector3.forward * curSpeed * Time.deltaTime); //Accelerate Forward
                    }
                    else
                    {
                        curSpeed = maxSprintSpeed;
                        transform.Translate(Vector3.forward * maxSprintSpeed * Time.deltaTime); //Accelerate Forward
                    }
                }
                else if (curSpeed > maxSprintSpeed)
                {
                    transform.Translate(Vector3.forward * maxSprintSpeed * Time.deltaTime); //Sprint Forward
                }
            }
        }

        if (Input.GetButton("Back"))
        {
            transform.Translate(Vector3.back * maxWalkSpeed * Time.deltaTime); //Walk Back
        }

        if (Input.GetButton("Right"))
        {
            transform.Translate(Vector3.right * maxWalkSpeed * Time.deltaTime); //Strafe Right
        }

        if (Input.GetButton("Left"))
        {
            transform.Translate(Vector3.left * maxWalkSpeed * Time.deltaTime); //Strafe Forward
        }

        if (curSpeed > maxWalkSpeed && !sprint)
        {
            curSpeed -= acceleration * Time.deltaTime;
        }
        else if (curSpeed < maxWalkSpeed)
        {
            curSpeed = maxWalkSpeed;
        }
    }
}
