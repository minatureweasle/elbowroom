using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {

    //public enum JumpState {RISING, FALLING, AIR, LANDED};
    //public JumpState jumpState = JumpState.LANDED;

    //public float threshold = 1f;
	
	void Update () {

        /*if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) <= threshold)
        {
            jumpState = JumpState.LANDED;
           // Debug.Log("landed");
        }
        else if (GetComponent<Rigidbody>().velocity.y < -threshold)
        {
            jumpState = JumpState.FALLING;
            //Debug.Log("falling");
        }
        else if (GetComponent<Rigidbody>().velocity.y > threshold)
        {
            jumpState = JumpState.RISING;
           // Debug.Log("rising");
        }*/

        //Debug.Log(jumpState.ToString());
        
	}

    public bool IsOnGround()
    {
        Ray rayDown = new Ray(transform.position + Vector3.up, Vector3.down);
        RaycastHit hit;

        //Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + Vector3.down * 1.6f, Color.red, 0.5f);

        if (Physics.Raycast(rayDown, out hit, 1.2f))
        {
            //Debug.Log("hit: " + hit.collider.name);
            return true;

        }
        else
        {
            //Debug.Log("hit NOTHING");
            return false;
        }
    }
}
