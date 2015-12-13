using UnityEngine;
using System.Collections;

/**
*   
*   Camera2D.cs 
*   
*/

public class Camera2D : MonoBehaviour {

    //
    public Transform player;

    public Vector2 mMargin, mSmoothing;

    public BoxCollider2D mBounds;

    public Vector3 mMin, mMax;

    public bool isFollowing { get; set; }

	// Use this for initialization
	void Start () {
        mMin = mBounds.bounds.min;
        mMax = mBounds.bounds.max;
        isFollowing = true;
	}
	
	// Update is called once per frame
	void Update () {
        var x = transform.position.x;
        var y = transform.position.y;

        if (isFollowing) {
            if(Mathf.Abs(x - player.transform.position.x) > mMargin.x)
            {
                x = Mathf.Lerp(x, transform.position.x, mSmoothing.x * Time.deltaTime);
            }

            if(Mathf.Abs(y - player.transform.position.y) > mMargin.y)
            {
                y = Mathf.Lerp(y, transform.position.y, mSmoothing.y * Time.deltaTime);
            }

            var cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
            x = Mathf.Clamp(x, mMin.x + cameraHalfWidth, mMax.x - cameraHalfWidth);
            y = Mathf.Clamp(y, mMin.y + Camera.main.orthographicSize, mMax.y - cameraHalfWidth);

            transform.position = new Vector3(x, y, transform.position.z);

        }
    }
}
