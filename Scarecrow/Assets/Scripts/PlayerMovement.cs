using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	/*public float speed = 6f;            
	public float acceleration = 6f; 
	public float gravity = 10;
	public float jumpHeight = 8;**/

	public float idleWaitTime = 10;

	float currentSpeed;
	float currentSpeedGrav;
	Quaternion newRotation;
	
	//Vector3 movement;                   
	Animator anim;                      
	Rigidbody playerRigidbody;          
	//int floorMask;                      
	//float camRayLength = 100f; 

	float h;
	float v;

	private bool moving;
	private bool jumped;
	private bool floored;
	private bool died;
	private bool fighting;
	private int nextAttack;
	private float timeIdle;

	
	void Awake ()
	{
		//floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
		jumped = false;
		died = false;


		currentSpeed = 0;
		floored = true;
		currentSpeedGrav = 0;
		timeIdle = 0;

		newRotation = transform.rotation;
	}
	
	
	void FixedUpdate ()
	{
		h = Input.GetAxisRaw ("Horizontal");;
		v = Input.GetAxisRaw ("Vertical");
		jumped = Input.GetButtonDown ("Jump");

		if (h != 0 || v != 0) {
			Move (h, v);
		}


		//Turning ();


		Animating (h, v);
		
	}
	
	void Move (float h, float v)
	{

		float turn = 0;

		if (h != 0 || v != 0) {
			if (v == 1) {
				if (h == 1) {
					turn = 45;
					h = 0;
				}
				if (h == -1) {
					turn = -45;
					h = 0;
				}
			} else if (v == 0) {
				if (h == 1) {
					turn = 90;
					v = 1;
					h = 0;
				}
				if (h == -1) {
					turn = -90;
					v = 1;
					h = 0;
				}
			} else {
				if (h == 0) {
					turn = 180;
					v = 1;
				}
				if (h == 1) {
					turn = 135;
					h = 0;
					v = 1;
				}
				if (h == -1) {
					turn = -135;
					h = 0;
					v = 1;
				}
			}
		}



		//h = IncrementTowards (currentSpeed, speed * Time.deltaTime * h, acceleration);
		//v = IncrementTowards (currentSpeed, speed * Time.deltaTime * v, acceleration);

		/*float g = IncrementTowards (currentSpeedGrav, maxFallSpeed * Time.deltaTime * -1, gravity);

		currentSpeedGrav -= gravity*Time.deltaTime;

		if(jumped){
			currentSpeedGrav = jumpHeight;
		}**/

		//movement.Set (h, 0, v);

		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, turn + Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z); 

		//transform.Translate (movement);


	}

	
	void Animating (float h, float v)
	{
		moving = h != 0 || v != 0;

		anim.SetBool ("Moving", moving);



		if (moving) {
			if(currentSpeed != 1 && !Input.GetButton("Walk") && !Input.GetButton("Run")){
				float s = Mathf.Sign(currentSpeed - 1)*-1;
				currentSpeed += Time.deltaTime*s;
				if(Mathf.Sign(currentSpeed -1) == s)
					currentSpeed = 1;
			} if(Input.GetButton("Walk") && currentSpeed != 0){
				currentSpeed -= Time.deltaTime;
				if(currentSpeed < 0)
					currentSpeed = 0;
			} if(Input.GetButton("Run") && currentSpeed != 2){
				currentSpeed += Time.deltaTime;
				if(currentSpeed >2)
					currentSpeed =2;
			}

			anim.SetFloat("MoveSpeed", currentSpeed);
		} 

		if (!moving && !fighting && floored) {
			timeIdle += Time.deltaTime/idleWaitTime;
			if (timeIdle / idleWaitTime >= 1)
				timeIdle = 1;
			anim.SetFloat ("IdleWaitTime", timeIdle);
		} else {
			timeIdle = 0;
		}

	}



	// Halutaan käyttää, koska muuten liikkeet töksähtelevät.

	/*private float IncrementTowards(float n, float target, float a){
		if(n == target){
			return n;
		}
		else{
			float dir = Mathf.Sign(target-n);
			n+=a*Time.deltaTime*dir;
			return(dir == Mathf.Sign(target-n))? n: target;
		}
	}**/
}