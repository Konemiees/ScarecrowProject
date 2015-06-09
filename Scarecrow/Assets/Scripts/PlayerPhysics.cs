using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {

	public float gravity = 0;





	// Use this for initialization
	void Start () {
	
	}

	public void move (Vector3 movement) {


		transform.Translate (new Vector3(0, 0, movement.z));
		transform.Rotate(new Vector3(0, movement.x, 0));

	}




}
