using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]

public class PlayerControl : MonoBehaviour {

	public float maxSpeed = 1;
	public float acceleration = 8;
	public float turnSpeed = 10;
	public float turnAcceleration = 10;

	private float currentSpeed;
	private float currentTurn;

	private PlayerPhysics playerPhysics;


	// Use this for initialization
	void Start () {
		currentSpeed = 0;
		playerPhysics = GetComponent<PlayerPhysics> ();
	}
	
	// Update is called once per frame
	void Update () {
		var coordX = Input.GetAxis ("Horizontal");
		var coordZ = Input.GetAxis ("Vertical");

		currentTurn = IncrementTowards (currentTurn, turnSpeed * Time.deltaTime * coordX, turnAcceleration);
		currentSpeed = IncrementTowards (currentSpeed, maxSpeed * Time.deltaTime * coordZ, acceleration);


		playerPhysics.move (new Vector3(currentTurn, 0, currentSpeed));

	}

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
