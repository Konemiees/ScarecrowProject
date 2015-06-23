using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;            
	public float acceleration = 6f;     

	float currentSpeed;
	Quaternion newRotation;
	
	Vector3 movement;                   
	Animator anim;                      
	Rigidbody playerRigidbody;          
	int floorMask;                      
	float camRayLength = 100f;          

	
	void Awake ()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();

		currentSpeed = 0;

		newRotation = transform.rotation;
	}
	
	
	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);

		Turning ();

		Animating (h, v);

		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z); 
	}
	
	void Move (float h, float v)
	{

		h = IncrementTowards (currentSpeed, speed * Time.deltaTime * h, acceleration);
		v = IncrementTowards (currentSpeed, speed * Time.deltaTime * v, acceleration);

		movement.Set (h, 0f, v);
		


		transform.Translate (movement);


	}
	
	void Turning ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;
		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;
			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			newRotation = Quaternion.LookRotation (playerToMouse);
			




		}
	}
	
	void Animating (float h, float v)
	{
		int walking = (int) Mathf.Sign(Mathf.Abs(h)+Mathf.Abs(v));

		anim.SetInteger ("IsWalking", walking);
	}



	// Halutaan käyttää, koska muuten liikkeet töksähtelevät.

	private float IncrementTowards(float n, float target, float a){
		if(n == target){
			return n;
		}
		else{
			float dir = Mathf.Sign(target-n);
			n+=a*Time.deltaTime*dir;
			return(dir == Mathf.Sign(target-n))? n: target;
		}
	}
}