using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    /// <summary>
    /// The bigger this value the faster the player can move.
    /// </summary>
    public float mSpeedMultiplier = 5f;

    /// <summary>
    /// ground LayerMask to see if the player is on the ground.
    /// </summary>
    public LayerMask mGroundLayer;
    
    /// <summary>
    /// 
    /// </summary>
    public Transform mGroundCheck;

    /// <summary>
    /// 
    /// </summary>
    public float mJumpMultiplier;

    /// <summary>
    /// Spawn point for where the bullet will come out.
    /// </summary>
    public Transform mSpawnPoint;

    /// <summary>
    /// GameObject reference to the bullet.
    /// </summary>
    public GameObject mBulletPrefab;

    /// <summary>
    /// Rigidbody to move the player around and let it takes forces.
    /// </summary>
    private Rigidbody2D mRigidbody2d;

    /// <summary>
    /// The Animator of the player to animate it.
    /// </summary>
    private Animator mAnimator;

    /// <summary>
    /// To see if the player is facing a direction. (needed to flip the localScale).
    /// </summary>
    private bool mFacingRight;

    /// <summary>
    /// float value for the input horizontal.
    /// </summary>
    private float mHorizontal;
    
    /// <summary>
    /// To see if the player is grounded
    /// </summary>
    private bool isGrounded = false;
    private float mGroundRadius = 0.2f;

    /// <summary>
    /// Fire rate
    /// </summary>
    private float mFireRate = .5f;

    /// <summary>
    /// For how fast the player can shoot.
    /// </summary>
    private float mNextFire = 0f;
	// Use this for initialization
	void Start () {
        
        // Reference to the rigidbody
        this.mRigidbody2d = GetComponent<Rigidbody2D>();
        // Reference to the Animator
        this.mAnimator = GetComponent<Animator>();
        // set facing right to true to see in the right direction.
        this.mFacingRight = true;
	}
	
	// Update is called once per frame
    protected void Update() {
        // Jump mechanic.
        if (isGrounded && Input.GetAxis("Jump") > 0) {
            isGrounded = false;
            mAnimator.SetBool("isGrounded", isGrounded);
            mRigidbody2d.AddForce(new Vector2(0, mJumpMultiplier));   
        }

        // Fire machenic
        if (Input.GetAxisRaw("Fire1") > 0) { Shoot(); }
    }

	protected void FixedUpdate () {

        isGrounded = Physics2D.OverlapCircle(mGroundCheck.position, mGroundRadius, mGroundLayer);
        mAnimator.SetBool("isGrounded", isGrounded);

        mAnimator.SetFloat("verticalSpeed", mRigidbody2d.velocity.y);

        mHorizontal = Input.GetAxis("Horizontal");
        if(mAnimator) {
            mAnimator.SetFloat("speed", Mathf.Abs(mHorizontal));

        }
        if (mRigidbody2d) {
            mRigidbody2d.velocity = new Vector2(mHorizontal * mSpeedMultiplier, mRigidbody2d.velocity.y);
        }

        if(mHorizontal > 0 && !mFacingRight) {
            Flip();
        }else if(mHorizontal < 0 && mFacingRight) {
            Flip();
        }
	}

    /// <summary>
    /// Handy flip mechanic to flip the 2d sprite.
    /// (Maybe should make this static).
    /// </summary>
    private void Flip() {
        mFacingRight = !mFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    /// <summary>
    /// Shooting mechanic for the player.
    /// Will not come in the really game this was just for the tutorial
    /// </summary>
    private void Shoot() {
        if(Time.time > mNextFire) {
            mNextFire = Time.time + mFireRate;

            if (mFacingRight) {
                Instantiate(mBulletPrefab, mSpawnPoint.transform.position, Quaternion.Euler(new Vector3(0,0,0)));
            } else if(!mFacingRight) {
                Instantiate(mBulletPrefab, mSpawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
        }
    }
}
