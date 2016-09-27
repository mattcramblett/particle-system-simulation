using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleGenerator : MonoBehaviour {
	public int startingNumberOfParticles = 25;
	public float yStartPosition = 5f;
	public ArrayList Particles;
	public ArrayList SmallParticles;
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

	void generateSmallParticles(GameObject p){
		for (int i = 0; i < 5; i++) {
			GameObject smallSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			smallSphere.transform.parent = transform;
			smallSphere.name = "smallsphere" + i.ToString();
			//this should be a position relative to the argument to create splatter
			smallSphere.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), yStartPosition, Random.Range(-4.0f, 4.0f)); //drop start pos
			//smaller than original raindrop
			smallSphere.transform.localScale = new Vector3(0.02f, 0.05f, 0.02f); //drop size
			SmallParticles.Add(smallSphere);
		}
	}


	// Use this for initialization
	void Start () {
		Particles = new ArrayList();
		generateParticles();
	}
	
	// Update is called once per frame
	void Update () {
		List<GameObject> toRemove = new List<GameObject> ();
		List<GameObject> toRemoveSmall = new List<GameObject> ();
		foreach (GameObject p in Particles){
			//add up forces acting on p
			p.transform.position += Vector3.up * rate; //cheaply make the rain fall
			GameObject ground = GameObject.FindGameObjectWithTag("ground");
			
			if (p.transform.position.y <= ground.transform.position.y) {
				toRemove.Add (p);
			}
		}
		foreach (GameObject p in toRemove) {
			//generateSmallParticles (p);
			Particles.Remove (p);
		}
		/*foreach (GameObject p in smallParticles) {
			p.transform.position += Vector3.up * rate;
			if (p.transform.position.y <= .2) {
				toRemoveSmall.Add (p);
			}
		}
		foreach (GameObject p in toRemoveSmall) {
			smallParticles.Remove (p);
		}*/

		/*if (GameObject.transform.position.y < 0) {
			print ("destroy");
			GameObject.Destroy ();
		}*/
		generateParticles();
	}
}
