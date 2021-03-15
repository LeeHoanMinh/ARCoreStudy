using UnityEngine;
using System.Collections;

public class InteractionScript : MonoBehaviour {

	public GenerateDebrisOnDestroy DestructionScript;

	public JumpgateController JumpgateScript;
	public JumpgateController JumpgateTwoScript;
	public JumpgateController JumpgateThreeScript;

	public StardockController StardockScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.D)) {
			if (DestructionScript != null) {
				DestructionScript.SelfDestruct = true;
			}
		}
		if (Input.GetKeyUp (KeyCode.J)) {
			if (JumpgateScript != null) {
				JumpgateScript.TestShipIndex = Random.Range (0, 4);
				JumpgateScript.TestSpawnStarship = true;
			}
			if (JumpgateTwoScript != null) {
				JumpgateTwoScript.TestShipIndex = Random.Range (0, 4);
				JumpgateTwoScript.TestSpawnStarship = true;
			}
			if (JumpgateThreeScript != null) {
				JumpgateThreeScript.TestShipIndex = Random.Range (0, 4);
				JumpgateThreeScript.TestSpawnStarship = true;
			}
		}
		if (Input.GetKeyUp (KeyCode.B)) {
			if (StardockScript != null) {
				StardockScript.TestBuildStarship = true;
			}
		}
	}
}
