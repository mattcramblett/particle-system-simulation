using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	Vector3 position; //updates frame to frame
	Vector3 velocity; //updates frame to fram
	Vector3 force; //reset and recomputed each frame
	GameObject geom;
	
	// Update is called once per frame
	public void Update (){
	}
	
	public void ApplyForce(Vector3 f){
		force += f*5;
	}

	public void ResetForce(){
		force = Vector3.zero;
	}
	
}
