using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleGenerator : MonoBehaviour {
	public int particleDensity = 25;
	public float yStartPosition = 5f;
	public ArrayList Particles;
	public float rate = -0.1f;


	public class Drop {
		public GameObject body;
		public float velocity;
		public int life;

		public Drop(){
			body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			velocity = 1f;
			life = 0;
		}
	}


	//creates sphere game objects:
	void generateParticles(){
		for(int i = 0; i < particleDensity; i++){
     		//GameObject aSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
     		Drop aSphere = new Drop();
     		aSphere.body.transform.parent = transform;
     		aSphere.body.name = "sphere" + i.ToString();
     		aSphere.body.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), yStartPosition, Random.Range(-4.0f, 4.0f)); //drop start pos
			aSphere.body.transform.localScale = new Vector3(0.04f, 0.1f, 0.04f); //drop size
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
		ArrayList toRemove = new ArrayList();
		foreach (Drop p in Particles){
			
			p.life++; //each frame is one point to life
			GameObject ground = GameObject.FindGameObjectWithTag("ground");

			//plane equation
			Vector3 planePoint = new Vector3(ground.transform.up.x,ground.transform.up.y,ground.transform.up.y);
			Vector3 normal = ground.transform.up;
			float dot = -Vector3.Dot (ground.transform.up, planePoint);
			//print (dot);



			//if particle is above ground:
			if (p.body.transform.position.y > ground.transform.position.y) {
				//drop falling physics:
				float accel = 1f; //only force is gravity, using an equal ratio with mass
				float endv = p.velocity + accel * Time.deltaTime; // v + a * deltaTime
				p.body.transform.position -= Vector3.up * ((endv + p.velocity)/2) * Time.deltaTime; //update y position
				p.velocity = endv; //update particle velocity
			}
			
			//if particle is above ground:
			if (p.body.transform.position.y <= ground.transform.position.y) {
				Vector3 shape = p.body.transform.localScale;
				Vector3 position = p.body.transform.position;
				position.y = 0;
				p.body.transform.position = position;
				if (shape.y > 0) {
					shape.y -= .01f;
					shape.x += .01f;
					shape.z += .01f;
				} else {
					toRemove.Add (p);
				}
				p.body.transform.localScale = shape;
			}

			//check each drops y position and assign a color:
			if(p.body.transform.position.y < 1.25){
				Material material = new Material(Shader.Find("Standard"));
				material.color = Color.red;
				p.body.GetComponent<Renderer>().material = material;
			}else if(p.body.transform.position.y < 2.5){
				Material material = new Material(Shader.Find("Standard"));
				material.color = Color.green;
				p.body.GetComponent<Renderer>().material = material;
			}else if(p.body.transform.position.y < 3.75){
				Material material = new Material(Shader.Find("Standard"));
				material.color = Color.yellow;
				p.body.GetComponent<Renderer>().material = material;
			}else{
				Material material = new Material(Shader.Find("Standard"));
				material.color = Color.blue;
				p.body.GetComponent<Renderer>().material = material;
			}


		}
		foreach (Drop p in toRemove) {
			Particles.Remove (p);
			Destroy (p.body);
		}

		//Key up and down to change density of drops:
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if(particleDensity < 50){
				particleDensity++;
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (particleDensity > 0) {
				particleDensity--;
			}
		}
		generateParticles ();
	}
}
