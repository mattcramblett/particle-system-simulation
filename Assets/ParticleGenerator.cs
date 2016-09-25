using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleGenerator : MonoBehaviour {
	public int startingNumberOfParticles = 25;
	public float yStartPosition = 5f;
	public ArrayList Particles;
	public float rate = -0.1f;

	//creates sphere game objects:
	void generateParticles(){
		for(int i = 0; i < startingNumberOfParticles; i++){
     		GameObject aSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
     		aSphere.transform.parent = transform;
     		aSphere.name = "sphere" + i.ToString();
     		aSphere.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), yStartPosition, Random.Range(-4.0f, 4.0f)); //drop start pos
			aSphere.transform.localScale = new Vector3(0.04f, 0.1f, 0.04f); //drop size
			Particles.Add(aSphere);
		}
	}


	// Use this for initialization
	void Start () {
		Particles = new ArrayList();
		generateParticles();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject p in Particles){
			//add up forces acting on p
			p.transform.position += Vector3.up * rate; //cheaply make the rain fall
		}
		generateParticles();
	}
}
