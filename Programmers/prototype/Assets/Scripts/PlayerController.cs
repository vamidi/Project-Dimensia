using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    // movement variables
    public float mMaxSpeed = 5f;

    private Rigidbody2D mRigidbody2d;
    private Animator mAnimator;
    private bool mFacingRight;
    private float horizontal;

	// Use this for initialization
	void Start () {
        this.mRigidbody2d = GetComponent<Rigidbody2D>();
        this.mAnimator = GetComponent<Animator>();
        this.mFacingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
        horizontal = Input.GetAxis("Horizontal");
        if(mAnimator) {
            mAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        }
        if (mRigidbody2d) {
            mRigidbody2d.velocity = new Vector2(horizontal * mMaxSpeed, mRigidbody2d.velocity.y);
        }

        if(horizontal > 0 && !mFacingRight) {
            Flip();
        }else if(horizontal < 0 && mFacingRight) {
            Flip();
        }
	}

    private void Flip() {
        mFacingRight = !mFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
