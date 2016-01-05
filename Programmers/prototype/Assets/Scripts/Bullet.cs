using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    /// <summary>
    /// How fast the bullet can travel
    /// </summary>
    public float mBulletMultiplier = 15f;

    /// <summary>
    /// How long this gameobject lives.
    /// </summary>
    public float mLifeTime = 5f;

    /// <summary>
    /// 
    /// </summary>
    private Rigidbody2D mRigidbody2d;

	// Use this for initialization
	void Awake () {
        
        // Reference to the rigidbody.
        this.mRigidbody2d = GetComponent<Rigidbody2D>();
        
        //Apply constant force.
        if(transform.localRotation.z > 0)
           mRigidbody2d.AddForce(new Vector2(-1, 0) * mBulletMultiplier , ForceMode2D.Impulse);
        else mRigidbody2d.AddForce(new Vector2(1, 0) * mBulletMultiplier, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update () {

        // Destroy the gameobect after the lifetime has been passed.
        Destroy(this.gameObject, mLifeTime);
	}
}
