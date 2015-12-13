using UnityEngine;
using System.Collections;

public class simpleMovement : MonoBehaviour {

    public Rigidbody mRigidBody;

    public float speeds = 5f;

	// Use this for initialization
	void Start () {
        if (this.mRigidBody == null)
            this.mRigidBody = this.GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
            this.mRigidBody.velocity += new Vector3(1 + speeds, 0) * Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.A))
            this.mRigidBody.velocity -= new Vector3(1 + speeds, 0) * Time.deltaTime;

        this.mRigidBody.velocity = Vector2.zero;

    }
}
