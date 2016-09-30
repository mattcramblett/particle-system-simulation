using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleGenerator : MonoBehaviour {
	public int particleDensity = 5;
	public float yStartPosition = 5f;
	public ArrayList Particles;



	public class Drop {
		public GameObject body;
		public float velocity;
		public int life;

		public Drop(){
			body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			velocity = .75f;
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
			aSphere.body.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); //drop size
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
			Vector3 planePoint = new Vector3(0,0,0);
			Vector3 normal = ground.transform.up;
			float dot = -Vector3.Dot (ground.transform.up, planePoint);
			
			//print (Vector3.Dot (p.body.transform.position - planePoint, normal));
			//float angle = Mathf.Abs(Vector3.Angle(normal, ground.transform.forward));
			//print(angle);

			//if particle is above ground:
			if (Vector3.Dot (p.body.transform.position - planePoint, normal) > .4) {
				//drop falling physics:
				float accel = 1f; //only force is gravity, using an equal ratio with mass
				float endv = p.velocity + accel * Time.deltaTime; // v + a * deltaTime
				p.body.transform.position -= Vector3.up * ((endv + p.velocity)/2) * Time.deltaTime; //update y position
				p.velocity = endv; //update particle velocity
			}
			
			//if particle is on ground:
			if (Vector3.Dot (p.body.transform.position - planePoint, normal) <= .4) {
				//TODO: add diagonal force down. plane is currently tilted 20 on x axis
				//can add rotation to ball too
				float accel = .05f;
				float endv = p.velocity + accel * Time.deltaTime;
				p.body.transform.position -= new Vector3(0,0,-1) * ((endv + p.velocity) / 2) * Time.deltaTime;

				p.velocity = endv;
			}

			if(p.body.transform.position.z > 5.5f){
				float accel = .05f;
				float endv = p.velocity + accel * Time.deltaTime;
				p.body.transform.position -= new Vector3(0,1,0) * ((endv + p.velocity) / 2) * Time.deltaTime;
				p.velocity = endv;
			}

			if(p.body.transform.position.y < -5f){
				toRemove.Add(p);
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
