using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public Transform target;            // The position that that camera will be following.
	public float smoothing = 5f;        // The speed with which the camera will be following.
	
	Vector3 offset;                     // The initial offset from the target.

	public float distance = 5.0f; 
	public float xSpeed = 125.0f; 
	public float ySpeed = 50.0f;
	private float x = 0.0f; 
	private float y = 0.0f;

	public float cameraLowLimit = 0;
	public float cameraUpAngleLimit = 58;
	public float cameraCloseLimit = 1;
	private bool cameraFrontLock;

	//@script AddComponentMenu("Camera-Control/Mouse Orbit");
	
	void Start ()
	{
		cameraFrontLock = false;
		x = transform.eulerAngles.y; 
		y = transform.eulerAngles.x;
	
		// Calculate the initial offset.
		offset = transform.position - target.position;
	}
	
	void FixedUpdate ()
	{
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = target.position + offset;
		

		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);

		 
		 if (target) { 
			x += Input.GetAxis("Mouse X") * xSpeed * distance; //0.02f; 
			y -= Input.GetAxis("Mouse Y") * ySpeed; //0.02f; 


			if (y > cameraUpAngleLimit){
				y = cameraUpAngleLimit;
			}

			Quaternion rotation = Quaternion.Euler(y, x, 0); 
			Vector3 position = rotation * new Vector3(0.0f, 2.0f, -distance) + target.position; 

			float dist = Mathf.Sqrt(Mathf.Abs(target.position.z-position.z)*Mathf.Abs(target.position.z-position.z)+(Mathf.Sqrt(Mathf.Abs(target.position.x-position.x)*Mathf.Abs(target.position.x-position.x)+Mathf.Abs(target.position.y-position.y)*Mathf.Abs(target.position.y-position.y))));

			print(dist);

			if(dist <= cameraCloseLimit && y > 0){
				print("täsä");
				rotation = Quaternion.Euler(0, x, 0);
				position = rotation * new Vector3(0.0f, 2.0f, -distance) + target.position;
			}

			if (position.y < cameraLowLimit) {
				position.y = cameraLowLimit;
				rotation = Quaternion.Euler(0, x, 0); 
			} 
			transform.position = position;
			transform.rotation = rotation;
		}

	}






}