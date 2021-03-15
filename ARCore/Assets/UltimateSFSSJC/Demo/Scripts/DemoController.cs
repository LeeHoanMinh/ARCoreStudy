using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum ColorSchemeTypes {
	Red,
	Gray,
	Blue,
	Green,
	Purple,
	Teal
}

public enum StarshipTypes {
	Carrier,
	Large,
	Medium,
	Small
}

public enum DemoModes {
	PremadeStarshipViewer,
	FleetViewer,
	MainMenu,
	ComponentViewer,
	JumpgateViewer,
	StardockViewer
}

public class DemoController : MonoBehaviour {

	public DemoModes CurrentMode = DemoModes.PremadeStarshipViewer;

	public bool AutoSpawnFleet = false;

	public ColorSchemeTypes CurrentColorScheme = ColorSchemeTypes.Red;
	public StarshipTypes CurrentStarshipType = StarshipTypes.Small;
	public int CurrentStarshipIndex = 0;

	public GameObject PremadeControlsGO;
	public Text CurrentModeText;

	[Space(12)]

	public bool StarshipSelected = false;
	public ShipMouseSelection CurrentShipSelected;
	public StarshipTypes SelectedType = StarshipTypes.Small;
	public int SelectedIndex = 0;

	[Space(12)]

	public Transform DirLightTransform;
	public ColorSchemeTypes FleetColorScheme = ColorSchemeTypes.Red;
	public GameObject FleetControlsPanelGO;

	// Fleet SpawnLocations
	public Transform[] CarrierLocations;
	public Transform[] LargeStarshipLocations;
	public Transform[] MediumStarshipLocations;
	public Transform[] SmallStarshipLocations;

	[Space(12)]

	// Color Scheme = Red
	public GameObject[] CarrierPrefabs_Red;
	public GameObject[] LargePrefabs_Red;
	public GameObject[] MediumPrefabs_Red;
	public GameObject[] SmallPrefabs_Red;

	// Color Scheme = Gray
	public GameObject[] CarrierPrefabs_Gray;
	public GameObject[] LargePrefabs_Gray;
	public GameObject[] MediumPrefabs_Gray;
	public GameObject[] SmallPrefabs_Gray;

	// Color Scheme = Blue
	public GameObject[] CarrierPrefabs_Blue;
	public GameObject[] LargePrefabs_Blue;
	public GameObject[] MediumPrefabs_Blue;
	public GameObject[] SmallPrefabs_Blue;

	// Color Scheme = Green
	public GameObject[] CarrierPrefabs_Green;
	public GameObject[] LargePrefabs_Green;
	public GameObject[] MediumPrefabs_Green;
	public GameObject[] SmallPrefabs_Green;

	// Color Scheme = Purple
	public GameObject[] CarrierPrefabs_Purple;
	public GameObject[] LargePrefabs_Purple;
	public GameObject[] MediumPrefabs_Purple;
	public GameObject[] SmallPrefabs_Purple;

	// Color Scheme = Teal
	public GameObject[] CarrierPrefabs_Teal;
	public GameObject[] LargePrefabs_Teal;
	public GameObject[] MediumPrefabs_Teal;
	public GameObject[] SmallPrefabs_Teal;

	[Space(12)]

	// Color Scheme = Red
	public Sprite[] CarrierIcons_Red;
	public Sprite[] LargeIcons_Red;
	public Sprite[] MediumIcons_Red;
	public Sprite[] SmallIcons_Red;

	// Color Scheme = Gray
	public Sprite[] CarrierIcons_Gray;
	public Sprite[] LargeIcons_Gray;
	public Sprite[] MediumIcons_Gray;
	public Sprite[] SmallIcons_Gray;

	// Color Scheme = Blue
	public Sprite[] CarrierIcons_Blue;
	public Sprite[] LargeIcons_Blue;
	public Sprite[] MediumIcons_Blue;
	public Sprite[] SmallIcons_Blue;

	// Color Scheme = Green
	public Sprite[] CarrierIcons_Green;
	public Sprite[] LargeIcons_Green;
	public Sprite[] MediumIcons_Green;
	public Sprite[] SmallIcons_Green;

	// Color Scheme = Purple
	public Sprite[] CarrierIcons_Purple;
	public Sprite[] LargeIcons_Purple;
	public Sprite[] MediumIcons_Purple;
	public Sprite[] SmallIcons_Purple;

	// Color Scheme = Teal
	public Sprite[] CarrierIcons_Teal;
	public Sprite[] LargeIcons_Teal;
	public Sprite[] MediumIcons_Teal;
	public Sprite[] SmallIcons_Teal;

	[Space(12)]

	public GameObject CurrentJumpgate;
	public JumpgateController CurrentJumpgateScript;

	public GameObject CurrentStardock;
	public StardockController CurrentStardockScript;

	public GameObject CurrentStarship;
	public GenerateDebrisOnDestroy CurrentDebrisScript;
	public GameObject OldStarship;
	public float DistanceToExit = 0;
	private Transform currentStarshipTransform;
	private float spawnStarshipDelayTimer = 0;
	private float spawnStarshipDelayTimerFreq = 16.0f;

	private List<GameObject> SpawnedFleetList;

	public Transform SpawnLocationTransform;
	public Transform FinalPositionTransform;
	public Transform ExitPositionTransform;

	public float MoveToFinalSpeed = 10;

	// GUI Elements
	public Sprite EmptyIconSprite;
	public Image FleetStarshipIcon;
	public Image StarshipIcon;
	public Image StarshipIconTwo;
	public Text SizeText;
	public Text StarshipTypeText;
	public Text ColorSchemeText;
	public Image ShipSelectionImage;
	public RectTransform ShipSelectionRect;
	public GameObject SelectedStarshipControlsPanelGO;

	public SpinObject CameraSpinScript;
	public float CurrentSpinSpeed = 15;
	public float SpinSpeedMIN = 5;
	public float SpinSpeedMAX = 30;
	public void ResetCameraRotation() {
		CameraTargetTransform.rotation = Quaternion.Euler(new Vector3(0, 40, 0));		
	}
	public void ToggleSpin() {
		if (CameraSpinScript != null) {
			CameraSpinScript.DoSpin = !CameraSpinScript.DoSpin;
		}
	}
	public void IncreaseSpinSpeed() {
		if (CurrentSpinSpeed < SpinSpeedMAX)
			CurrentSpinSpeed += 5;
		else
			CurrentSpinSpeed = SpinSpeedMAX;

		CameraSpinScript.SpinSpeed = CurrentSpinSpeed;
	}
	public void DecreaseSpinSpeed() {
		if (CurrentSpinSpeed > SpinSpeedMIN)
			CurrentSpinSpeed -= 5;
		else
			CurrentSpinSpeed = SpinSpeedMIN;

		CameraSpinScript.SpinSpeed = CurrentSpinSpeed;
	}

	public void NavigateToMainMenu() {
		SceneManager.LoadScene ("bloodFleetPackDemo");
	}

	public void NavigateToPremadeDemo() {
		SceneManager.LoadScene ("premadeDemoScene");
	}

	public void NavigateToFleetDemo() {
		SceneManager.LoadScene ("fleetDemoScene");
	}

	public void NavigateToComponentDemo() {
		SceneManager.LoadScene ("componentDemoScene");
	}

	public void NavigateToJumpgateDemo() {
		SceneManager.LoadScene ("jumpgateDemoScene");
	}

	public void NavigateToStardockDemo() {
		SceneManager.LoadScene ("stardockDemoScene");
	}

	// Use this for initialization
	void Start () {
		// Choose Current Scene
		if (SceneManager.GetActiveScene ().name == "bloodFleetPackDemo") {
			Debug.Log ("Main Menu Active...");
			CurrentMode = DemoModes.MainMenu;
			CurrentModeText.text = "MAIN MENU";

			UpdateModeGUI ();
		}
		else if (SceneManager.GetActiveScene ().name == "jumpgateDemoScene") {
			Debug.Log ("Jumpgate Demo Scene Active...");
			CurrentMode = DemoModes.JumpgateViewer;
			CurrentModeText.text = "JUMPGATE VIEWER";

			UpdateModeGUI ();

			UpdateGUIElements ();
		}
		else if (SceneManager.GetActiveScene ().name == "stardockDemoScene") {
			Debug.Log ("Stardock Demo Scene Active...");
			CurrentMode = DemoModes.StardockViewer;
			CurrentModeText.text = "STARDOCK VIEWER";

			UpdateModeGUI ();

			UpdateGUIElements ();
		}
		else if (SceneManager.GetActiveScene ().name == "premadeDemoScene") {
			Debug.Log ("Premade Starship Demo Active...");
			CurrentMode = DemoModes.PremadeStarshipViewer;
			CurrentModeText.text = "PREMADE STARSHIP VIEWER";

			CurrentColorScheme = ColorSchemeTypes.Red;
			CurrentStarshipType = StarshipTypes.Small;
			SetupShipTypeCamera (CurrentStarshipType);
			if (CurrentStarship == null)
				SpawnStarship ();
			UpdateModeGUI ();

			StarshipSelectionPanelGO.SetActive (false);
			FleetControlsPanelGO.SetActive (false);

			if (!PremadeControlsGO.activeSelf)
				PremadeControlsGO.SetActive (true);

			CurrentModeText.text = "PREMADE STARSHIP VIEWER";

			DirLightTransform.rotation = Quaternion.Euler(new Vector3(45, 235, 0));

			UpdateModeGUI ();

			ClearCurrentFleet ();
			ClearCurrentStarship ();
		}
		else if (SceneManager.GetActiveScene ().name == "fleetDemoScene") {
			Debug.Log ("Fleet Demo Active...");
			CurrentMode = DemoModes.FleetViewer;
			CurrentModeText.text = "COMPLETE PREMADE FLEET VIEWER";

			SpawnedFleetList = new List<GameObject> ();

			if (PremadeControlsGO.activeSelf)
				PremadeControlsGO.SetActive (false);

			// Setup Fleet Camera
			if (CameraTargetTransform != null & CameraTransform != null) {
				// Setup Camera for Small Starships
				CameraTargetTransform.position = new Vector3(-11f, -9.29f, 0f);
				CameraTargetTransform.rotation = Quaternion.Euler(new Vector3(0, 311.6f, 0));
				CameraTransform.localPosition = new Vector3 (0, 34.1f, 60);
			}

			DirLightTransform.rotation = Quaternion.Euler(new Vector3(45, 135, 0));

			ShipSelectionImage.enabled = false;
			UpdateModeGUI ();

			ClearCurrentStarship();
			SpawnCurrentFleet ();
		}
		else if (SceneManager.GetActiveScene ().name == "componentDemoScene") {
			Debug.Log ("Component Demo Active...");
			CurrentMode = DemoModes.ComponentViewer;
			CurrentModeText.text = "COMPONENT VIEWER";

			UpdateModeGUI ();
		}

		if (AutoSpawnFleet) {
			Debug.Log ("Fleet Demo Active...");
			CurrentMode = DemoModes.FleetViewer;
			CurrentModeText.text = "COMPLETE PREMADE FLEET VIEWER";

			SpawnedFleetList = new List<GameObject> ();

			if (PremadeControlsGO.activeSelf)
				PremadeControlsGO.SetActive (false);

			// Setup Fleet Camera
			if (CameraTargetTransform != null & CameraTransform != null) {
				// Setup Camera for Small Starships
				CameraTargetTransform.position = new Vector3(-11f, -9.29f, 0f);
				CameraTargetTransform.rotation = Quaternion.Euler(new Vector3(0, 311.6f, 0));
				CameraTransform.localPosition = new Vector3 (0, 34.1f, 60);
			}

			DirLightTransform.rotation = Quaternion.Euler(new Vector3(45, 135, 0));

			ShipSelectionImage.enabled = false;
			UpdateModeGUI ();

			ClearCurrentStarship();
			SpawnCurrentFleet ();
		}

		ShipSelectionRect = ShipSelectionImage.gameObject.GetComponent<RectTransform> ();
		ShipSelectionRect.anchoredPosition = new Vector2 (640, 240);
		spawnStarshipDelayTimer = spawnStarshipDelayTimerFreq;

		SetupShipTypeCamera (CurrentStarshipType);
	}

	// Icon Sprite Generator
	public bool CreateIconSprites = false;
	public GameObject IconSpritePrefab;
	private float iconSpawnTimer = 0;
	private float iconSpawnTimerFreq = 0.75f;
	private int spriteIndex = 0;
	private StarshipTypes currentIconType = StarshipTypes.Small;
	private void GenerateIconSprites() {
		if (iconSpawnTimer < iconSpawnTimerFreq) {
			iconSpawnTimer += Time.deltaTime;
		} else {
			if (currentIconType == StarshipTypes.Small) {
				SpawnIconSprite (SmallIcons_Red [spriteIndex], SmallIcons_Gray [spriteIndex], SmallIcons_Green [spriteIndex], SmallIcons_Blue [spriteIndex], SmallIcons_Purple [spriteIndex], SmallIcons_Teal [spriteIndex]);
				if (spriteIndex < 4)
					spriteIndex++;
				else {
					currentIconType = StarshipTypes.Medium;
					spriteIndex = 0;
				}
			}
			else if (currentIconType == StarshipTypes.Medium) {
				SpawnIconSprite (MediumIcons_Red [spriteIndex], MediumIcons_Gray [spriteIndex], MediumIcons_Green [spriteIndex], MediumIcons_Blue [spriteIndex], MediumIcons_Purple [spriteIndex], MediumIcons_Teal [spriteIndex]);
				if (spriteIndex < 3)
					spriteIndex++;
				else {
					currentIconType = StarshipTypes.Large;
					spriteIndex = 0;
				}
			}
			else if (currentIconType == StarshipTypes.Large) {
				SpawnIconSprite (LargeIcons_Red [spriteIndex], LargeIcons_Gray [spriteIndex], LargeIcons_Green [spriteIndex], LargeIcons_Blue [spriteIndex], LargeIcons_Purple [spriteIndex], LargeIcons_Teal [spriteIndex]);
				if (spriteIndex < 3)
					spriteIndex++;
				else {
					currentIconType = StarshipTypes.Carrier;
					spriteIndex = 0;
				}
			}
			else if (currentIconType == StarshipTypes.Carrier) {
				SpawnIconSprite (CarrierIcons_Red [spriteIndex], CarrierIcons_Gray [spriteIndex], CarrierIcons_Green [spriteIndex], CarrierIcons_Blue [spriteIndex], CarrierIcons_Purple [spriteIndex], CarrierIcons_Teal [spriteIndex]);
				if (spriteIndex < 3)
					spriteIndex++;
				else {
					CreateIconSprites = false;
				}
			}
			iconSpawnTimer = 0;
		}
	}
	private void SpawnIconSprite(Sprite sprite1ToUse, Sprite sprite2ToUse, Sprite sprite3ToUse, Sprite sprite4ToUse, Sprite sprite5ToUse, Sprite sprite6ToUse) {
		float yPosition = 6;
		float flyDownSpeed = 1.75f;
		float sidewaysSpeed = 0.5f;

		// Sprite 1
		Vector3 spawnPosition = new Vector3 (3.75f, yPosition, 0);
		GameObject newIcon = GameObject.Instantiate (IconSpritePrefab, spawnPosition, Quaternion.identity) as GameObject;
		SpriteRenderer iconSpriteScript = newIcon.GetComponent<SpriteRenderer> ();
		iconSpriteScript.sprite = sprite1ToUse;
		StarshipMovement moveScript = newIcon.AddComponent<StarshipMovement> ();
		moveScript.FlyDown = true;
		moveScript.Speed = flyDownSpeed;
		moveScript.FlyRight = true;
		moveScript.SidewaysSpeed = sidewaysSpeed;
		// Sprite 2
		spawnPosition = new Vector3 (2.25f, yPosition, 0);
		GameObject new2Icon = GameObject.Instantiate (IconSpritePrefab, spawnPosition, Quaternion.identity) as GameObject;
		iconSpriteScript = new2Icon.GetComponent<SpriteRenderer> ();
		iconSpriteScript.sprite = sprite2ToUse;
		moveScript = new2Icon.AddComponent<StarshipMovement> ();
		moveScript.FlyDown = true;
		moveScript.Speed = flyDownSpeed;
		moveScript.FlyRight = true;
		moveScript.SidewaysSpeed = sidewaysSpeed;
		// Sprite 3
		spawnPosition = new Vector3 (0.75f, yPosition, 0);
		GameObject new3Icon = GameObject.Instantiate (IconSpritePrefab, spawnPosition, Quaternion.identity) as GameObject;
		iconSpriteScript = new3Icon.GetComponent<SpriteRenderer> ();
		iconSpriteScript.sprite = sprite3ToUse;
		moveScript = new3Icon.AddComponent<StarshipMovement> ();
		moveScript.FlyDown = true;
		moveScript.Speed = flyDownSpeed;
		moveScript.FlyRight = true;
		moveScript.SidewaysSpeed = sidewaysSpeed;
		// Sprite 4
		spawnPosition = new Vector3 (-0.75f, yPosition, 0);
		GameObject new4Icon = GameObject.Instantiate (IconSpritePrefab, spawnPosition, Quaternion.identity) as GameObject;
		iconSpriteScript = new4Icon.GetComponent<SpriteRenderer> ();
		iconSpriteScript.sprite = sprite4ToUse;
		moveScript = new4Icon.AddComponent<StarshipMovement> ();
		moveScript.FlyDown = true;
		moveScript.Speed = flyDownSpeed;
		moveScript.FlyLeft = true;
		moveScript.SidewaysSpeed = sidewaysSpeed;
		// Sprite 5
		spawnPosition = new Vector3 (-2.25f, yPosition, 0);
		GameObject new5Icon = GameObject.Instantiate (IconSpritePrefab, spawnPosition, Quaternion.identity) as GameObject;
		iconSpriteScript = new5Icon.GetComponent<SpriteRenderer> ();
		iconSpriteScript.sprite = sprite5ToUse;
		moveScript = new5Icon.AddComponent<StarshipMovement> ();
		moveScript.FlyDown = true;
		moveScript.Speed = flyDownSpeed;
		moveScript.FlyLeft = true;
		moveScript.SidewaysSpeed = sidewaysSpeed;
		// Sprite 6
		spawnPosition = new Vector3 (-3.75f, yPosition, 0);
		GameObject new6Icon = GameObject.Instantiate (IconSpritePrefab, spawnPosition, Quaternion.identity) as GameObject;
		iconSpriteScript = new6Icon.GetComponent<SpriteRenderer> ();
		iconSpriteScript.sprite = sprite6ToUse;
		moveScript = new6Icon.AddComponent<StarshipMovement> ();
		moveScript.FlyDown = true;
		moveScript.Speed = flyDownSpeed;
		moveScript.FlyLeft = true;
		moveScript.SidewaysSpeed = sidewaysSpeed;
	}

	// Update is called once per frame
	void Update () {
		if (CreateIconSprites) {
			GenerateIconSprites ();
		}

		// Demo Modes
		if (CurrentMode == DemoModes.FleetViewer) {

			if (Input.GetMouseButtonUp (0)) {
				DoStarshipMouseSelection ();
			}

			if (Input.GetKeyUp (KeyCode.Space)) {
				// Fire Rocket At Selected Starship
				if (DemoWeaponLauncher.GlobalAccess != null) {
					if (CurrentShipSelected != null) {
						DemoWeaponLauncher.GlobalAccess.FireRocket (CurrentShipSelected.gameObject.transform);
					}
				}
			}

			UpdateCarrierControls ();

			if (CurrentShipSelected == null) {
				if (SelectedStarshipControlsPanelGO.activeSelf)
					SelectedStarshipControlsPanelGO.SetActive (false);

				if (FleetStarshipIcon.sprite != EmptyIconSprite)
					FleetStarshipIcon.sprite = EmptyIconSprite;
				if (SizeText.enabled)
					SizeText.enabled = false;
				if (StarshipTypeText.enabled)
					StarshipTypeText.enabled = false;
				if (ColorSchemeText.enabled)
					ColorSchemeText.enabled = false;

				if (ShipSelectionImage.enabled)
					ShipSelectionImage.enabled = false;
			} else {
				if (!SelectedStarshipControlsPanelGO.activeSelf)
					SelectedStarshipControlsPanelGO.SetActive (true);

				if (!SizeText.enabled)
					SizeText.enabled = true;
				if (!StarshipTypeText.enabled)
					StarshipTypeText.enabled = true;
				if (!ColorSchemeText.enabled)
					ColorSchemeText.enabled = true;

				if (!ShipSelectionImage.enabled)
					ShipSelectionImage.enabled = true;
			}

		} else if (CurrentMode == DemoModes.JumpgateViewer) {

			if (CurrentJumpgate == null) {
				if (spawnStarshipDelayTimer < spawnStarshipDelayTimerFreq) {
					spawnStarshipDelayTimer += Time.deltaTime;
				} else {
					SpawnJumpgate ();
				}
			}

			if (Input.GetKeyUp (KeyCode.T)) {
				// Cycle Starship Type
				ChangeStarshipType ();
			}
			if (Input.GetKeyUp (KeyCode.N)) {
				// Cycle Starship Number
				ChangeStarshipNumber ();
			}
			if (Input.GetKeyUp (KeyCode.C)) {
				// Cycle Starship Color Scheme
				ChangeStarshipColoring ();
			}

			UpdateCarrierControls ();

			if (CurrentStarship != null) {
				if (currentStarshipTransform == null)
					currentStarshipTransform = CurrentStarship.transform;

				// Move To Position
				currentStarshipTransform.position = Vector3.Lerp (currentStarshipTransform.position, FinalPositionTransform.position, MoveToFinalSpeed * Time.deltaTime);
			}
					
		} else if (CurrentMode == DemoModes.StardockViewer) {

			if (CurrentStardock == null) {
				if (spawnStarshipDelayTimer < spawnStarshipDelayTimerFreq) {
					spawnStarshipDelayTimer += Time.deltaTime;
				} else {
					SpawnStardock ();
				}
			}

			if (Input.GetKeyUp (KeyCode.T)) {
				// Cycle Starship Type
				ChangeStarshipType ();
			}
			if (Input.GetKeyUp (KeyCode.N)) {
				// Cycle Starship Number
				ChangeStarshipNumber ();
			}
			if (Input.GetKeyUp (KeyCode.C)) {
				// Cycle Starship Color Scheme
				ChangeStarshipColoring ();
			}

			UpdateCarrierControls ();

			if (CurrentStarship != null) {
				if (currentStarshipTransform == null)
					currentStarshipTransform = CurrentStarship.transform;

				// Move To Position
				currentStarshipTransform.position = Vector3.Lerp (currentStarshipTransform.position, FinalPositionTransform.position, MoveToFinalSpeed * Time.deltaTime);
			}

		} else if (CurrentMode == DemoModes.PremadeStarshipViewer) {

			if (CurrentStarship == null) {
				if (spawnStarshipDelayTimer < spawnStarshipDelayTimerFreq) {
					spawnStarshipDelayTimer += Time.deltaTime;
				} else {
					SpawnStarship ();
				}
			}

			if (Input.GetKeyUp (KeyCode.T)) {
				// Cycle Starship Type
				ChangeStarshipType ();
			}
			if (Input.GetKeyUp (KeyCode.N)) {
				// Cycle Starship Number
				ChangeStarshipNumber ();
			}
			if (Input.GetKeyUp (KeyCode.C)) {
				// Cycle Starship Color Scheme
				ChangeStarshipColoring ();
			}

			UpdateCarrierControls ();

			if (CurrentStarship != null) {
				if (currentStarshipTransform == null)
					currentStarshipTransform = CurrentStarship.transform;

				// Move To Position
				currentStarshipTransform.position = Vector3.Lerp (currentStarshipTransform.position, FinalPositionTransform.position, MoveToFinalSpeed * Time.deltaTime);
			}

		}
	}

	public void DestroySelectedFleetShip() {
		GenerateDebrisOnDestroy debrisScript = CurrentShipSelected.gameObject.GetComponent<GenerateDebrisOnDestroy> ();
		if (debrisScript != null) {
			debrisScript.DestroyStarship ();
		} else {
			GenerateDebrisOnDestroy addedDebrisScript = CurrentShipSelected.gameObject.AddComponent<GenerateDebrisOnDestroy> ();
			addedDebrisScript.StarshipType = CurrentShipSelected.Type;
			addedDebrisScript.StarshipColoring = (StarshipDebrisColors)CurrentShipSelected.Coloring;
			addedDebrisScript.DestroyStarship ();
		}
	}

	public void ChangeToFleetViewer() {
		ChangeDemoMode (DemoModes.FleetViewer);
	}

	public void ChangeToPremadeViewer() {
		ChangeDemoMode (DemoModes.PremadeStarshipViewer);
	}

	public void ChangeDemoMode(DemoModes modeToChangeTo) {
		if (modeToChangeTo == DemoModes.FleetViewer) {
			if (PremadeControlsGO.activeSelf)
				PremadeControlsGO.SetActive (false);

			// Setup Fleet Camera
			if (CameraTargetTransform != null & CameraTransform != null) {
				// Setup Camera for Small Starships
				CameraTargetTransform.position = new Vector3(-11f, -9.29f, 0f);
				CameraTargetTransform.rotation = Quaternion.Euler(new Vector3(0, 311.6f, 0));
				CameraTransform.localPosition = new Vector3 (0, 34.1f, 60);
			}

			DirLightTransform.rotation = Quaternion.Euler(new Vector3(45, 135, 0));

			ShipSelectionImage.enabled = false;
			UpdateModeGUI ();

			ClearCurrentStarship();
			SpawnCurrentFleet ();
		}
		else if (modeToChangeTo == DemoModes.PremadeStarshipViewer) {
			if (!PremadeControlsGO.activeSelf)
				PremadeControlsGO.SetActive (true);

			CurrentModeText.text = "PREMADE STARSHIP VIEWER";

			DirLightTransform.rotation = Quaternion.Euler(new Vector3(45, 235, 0));

			UpdateModeGUI ();

			ClearCurrentFleet ();
			ClearCurrentStarship ();
//			SpawnStarship ();
		}
		CurrentMode = modeToChangeTo;
	}

	public Transform CameraTargetTransform;
	public Transform CameraTransform;

	public GameObject StarshipSelectionPanelGO;

	private void UpdateModeGUI() {
		if (CurrentMode == DemoModes.FleetViewer) {
			
		} else if (CurrentMode == DemoModes.PremadeStarshipViewer) {
			
		} else if (CurrentMode == DemoModes.MainMenu) {
			
		} else if (CurrentMode == DemoModes.ComponentViewer) {
			
		}
	}

	private void UpdateGUIElements() {
		
		SizeText.text = CurrentStarshipType.ToString();
		if (CurrentStarship != null && CurrentMode == DemoModes.PremadeStarshipViewer)
			StarshipTypeText.text = CurrentStarship.name;
		if (CurrentMode == DemoModes.JumpgateViewer)
			StarshipTypeText.text = StarshipPrefabManager.GlobalAccess.GetStarshipPrefab (CurrentStarshipType, CurrentColorScheme, CurrentStarshipIndex).name;
		if (CurrentMode == DemoModes.FleetViewer) {
			StarshipTypeText.text = CurrentShipSelected.gameObject.transform.root.gameObject.name;
		}			
		ColorSchemeText.text = CurrentColorScheme.ToString();

		// Update Starship Icon
		if (CurrentColorScheme == ColorSchemeTypes.Gray) {
			// Update Starship Icon - Gray
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				StarshipIcon.sprite = CarrierIcons_Gray [CurrentStarshipIndex];
				StarshipIconTwo.sprite = CarrierIcons_Gray [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				StarshipIcon.sprite = LargeIcons_Gray [CurrentStarshipIndex];
				StarshipIconTwo.sprite = LargeIcons_Gray [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				StarshipIcon.sprite = MediumIcons_Gray [CurrentStarshipIndex];
				StarshipIconTwo.sprite = MediumIcons_Gray [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				StarshipIcon.sprite = SmallIcons_Gray [CurrentStarshipIndex];
				StarshipIconTwo.sprite = SmallIcons_Gray [CurrentStarshipIndex];
			}
		}
		else if (CurrentColorScheme == ColorSchemeTypes.Blue) {
			// Update Starship Icon - Blue
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				StarshipIcon.sprite = CarrierIcons_Blue [CurrentStarshipIndex];
				StarshipIconTwo.sprite = CarrierIcons_Blue [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				StarshipIcon.sprite = LargeIcons_Blue [CurrentStarshipIndex];
				StarshipIconTwo.sprite = LargeIcons_Blue [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				StarshipIcon.sprite = MediumIcons_Blue [CurrentStarshipIndex];
				StarshipIconTwo.sprite = MediumIcons_Blue [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				StarshipIcon.sprite = SmallIcons_Blue [CurrentStarshipIndex];
				StarshipIconTwo.sprite = SmallIcons_Blue [CurrentStarshipIndex];
			}
		}
		else if (CurrentColorScheme == ColorSchemeTypes.Green) {
			// Update Starship Icon - Green
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				StarshipIcon.sprite = CarrierIcons_Green [CurrentStarshipIndex];
				StarshipIconTwo.sprite = CarrierIcons_Green [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				StarshipIcon.sprite = LargeIcons_Green [CurrentStarshipIndex];
				StarshipIconTwo.sprite = LargeIcons_Green [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				StarshipIcon.sprite = MediumIcons_Green [CurrentStarshipIndex];
				StarshipIconTwo.sprite = MediumIcons_Green [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				StarshipIcon.sprite = SmallIcons_Green [CurrentStarshipIndex];
				StarshipIconTwo.sprite = SmallIcons_Green [CurrentStarshipIndex];
			}
		}
		else if (CurrentColorScheme == ColorSchemeTypes.Purple) {
			// Update Starship Icon - Purple
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				StarshipIcon.sprite = CarrierIcons_Purple [CurrentStarshipIndex];
				StarshipIconTwo.sprite = CarrierIcons_Purple [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				StarshipIcon.sprite = LargeIcons_Purple [CurrentStarshipIndex];
				StarshipIconTwo.sprite = LargeIcons_Purple [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				StarshipIcon.sprite = MediumIcons_Purple [CurrentStarshipIndex];
				StarshipIconTwo.sprite = MediumIcons_Purple [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				StarshipIcon.sprite = SmallIcons_Purple [CurrentStarshipIndex];
				StarshipIconTwo.sprite = SmallIcons_Purple [CurrentStarshipIndex];
			}
		}
		else if (CurrentColorScheme == ColorSchemeTypes.Red) {
			// Update Starship Icon - Red
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				StarshipIcon.sprite = CarrierIcons_Red [CurrentStarshipIndex];
				StarshipIconTwo.sprite = CarrierIcons_Red [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				StarshipIcon.sprite = LargeIcons_Red [CurrentStarshipIndex];
				StarshipIconTwo.sprite = LargeIcons_Red [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				StarshipIcon.sprite = MediumIcons_Red [CurrentStarshipIndex];
				StarshipIconTwo.sprite = MediumIcons_Red [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				StarshipIcon.sprite = SmallIcons_Red [CurrentStarshipIndex];
				StarshipIconTwo.sprite = SmallIcons_Red [CurrentStarshipIndex];
			}
		}
		else if (CurrentColorScheme == ColorSchemeTypes.Teal) {
			// Update Starship Icon - Teal
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				StarshipIcon.sprite = CarrierIcons_Teal [CurrentStarshipIndex];
				StarshipIconTwo.sprite = CarrierIcons_Teal [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				StarshipIcon.sprite = LargeIcons_Teal [CurrentStarshipIndex];
				StarshipIconTwo.sprite = LargeIcons_Teal [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				StarshipIcon.sprite = MediumIcons_Teal [CurrentStarshipIndex];
				StarshipIconTwo.sprite = MediumIcons_Teal [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				StarshipIcon.sprite = SmallIcons_Teal [CurrentStarshipIndex];
				StarshipIconTwo.sprite = SmallIcons_Teal [CurrentStarshipIndex];
			}
		}
	}

	private void SetupShipTypeCamera(StarshipTypes typeToSetupFor) {
		if (CurrentMode == DemoModes.PremadeStarshipViewer) {
			if (typeToSetupFor == StarshipTypes.Small) {
				if (CameraTargetTransform != null & CameraTransform != null) {
					// Setup Camera for Small Starships
					CameraTargetTransform.position = new Vector3 (0, 0.25f, 0);
					CameraTargetTransform.rotation = Quaternion.Euler (new Vector3 (0, 40, 0));
					CameraTransform.localPosition = new Vector3 (0, 1, 4);
				}
			} else if (typeToSetupFor == StarshipTypes.Medium) {
				if (CameraTargetTransform != null & CameraTransform != null) {
					// Setup Camera for Small Starships
					CameraTargetTransform.position = new Vector3 (0, 0.25f, 0);
					CameraTargetTransform.rotation = Quaternion.Euler (new Vector3 (0, 40, 0));
					CameraTransform.localPosition = new Vector3 (0, 6, 25);
				}
			} else if (typeToSetupFor == StarshipTypes.Large) {
				if (CameraTargetTransform != null & CameraTransform != null) {
					// Setup Camera for Small Starships
					CameraTargetTransform.position = new Vector3 (0, 0.5f, 0);
					CameraTargetTransform.rotation = Quaternion.Euler (new Vector3 (0, 40, 0));
					CameraTransform.localPosition = new Vector3 (0, 16, 50);
				}
			} else if (typeToSetupFor == StarshipTypes.Carrier) {
				if (CameraTargetTransform != null & CameraTransform != null) {
					// Setup Camera for Small Starships
					CameraTargetTransform.position = new Vector3 (0, -1.5f, 0);
					CameraTargetTransform.rotation = Quaternion.Euler (new Vector3 (0, 40, 0));
					CameraTransform.localPosition = new Vector3 (0, 12, 28);
				}
			}
		} else if (CurrentMode == DemoModes.JumpgateViewer) {
			if (typeToSetupFor == StarshipTypes.Small) {
				if (CameraTargetTransform != null & CameraTransform != null) {
					// Setup Camera for Small Starships
					CameraTargetTransform.position = new Vector3 (0, 0.25f, 10);
					CameraTargetTransform.rotation = Quaternion.Euler (new Vector3 (0, 25, 0));
					CameraTransform.localPosition = new Vector3 (0, 5, 26);
				}
			} else if (typeToSetupFor == StarshipTypes.Medium) {
				if (CameraTargetTransform != null & CameraTransform != null) {
					// Setup Camera for Small Starships
					CameraTargetTransform.position = new Vector3 (0, 0.25f, 20);
					CameraTargetTransform.rotation = Quaternion.Euler (new Vector3 (0, 25, 0));
					CameraTransform.localPosition = new Vector3 (0, 20, 65);
				}
			} else if (typeToSetupFor == StarshipTypes.Large) {
				if (CameraTargetTransform != null & CameraTransform != null) {
					// Setup Camera for Small Starships
					CameraTargetTransform.position = new Vector3 (0, 0.5f, 30);
					CameraTargetTransform.rotation = Quaternion.Euler (new Vector3 (0, 25, 0));
					CameraTransform.localPosition = new Vector3 (0, 30, 120);
				}
			}
		} else if (CurrentMode == DemoModes.StardockViewer) {
			if (typeToSetupFor == StarshipTypes.Small) {
				if (CameraTargetTransform != null & CameraTransform != null) {
					// Setup Camera for Small Starships
					CameraTargetTransform.position = new Vector3 (0, -0.25f, 0);
					CameraTargetTransform.rotation = Quaternion.Euler (new Vector3 (0, 15, 0));
					CameraTransform.localPosition = new Vector3 (0, 2, 8);
				}
			} else if (typeToSetupFor == StarshipTypes.Medium) {
				if (CameraTargetTransform != null & CameraTransform != null) {
					// Setup Camera for Small Starships
					CameraTargetTransform.position = new Vector3 (0, 0.25f, 0);
					CameraTargetTransform.rotation = Quaternion.Euler (new Vector3 (0, 18, 0));
					CameraTransform.localPosition = new Vector3 (0, 6, 28);
				}
			} else if (typeToSetupFor == StarshipTypes.Large) {
				if (CameraTargetTransform != null & CameraTransform != null) {
					// Setup Camera for Small Starships
					CameraTargetTransform.position = new Vector3 (0, -0.5f, 0);
					CameraTargetTransform.rotation = Quaternion.Euler (new Vector3 (0, 16, 0));
					CameraTransform.localPosition = new Vector3 (0, 12, 65);
				}
			}
		}
	}

	public void ChangeStarshipColoring() {
		if (CurrentColorScheme == ColorSchemeTypes.Red)
			CurrentColorScheme = ColorSchemeTypes.Gray;
		else if (CurrentColorScheme == ColorSchemeTypes.Gray)
			CurrentColorScheme = ColorSchemeTypes.Blue;
		else if (CurrentColorScheme == ColorSchemeTypes.Blue)
			CurrentColorScheme = ColorSchemeTypes.Green;
		else if (CurrentColorScheme == ColorSchemeTypes.Green)
			CurrentColorScheme = ColorSchemeTypes.Purple;
		else if (CurrentColorScheme == ColorSchemeTypes.Purple)
			CurrentColorScheme = ColorSchemeTypes.Teal;
		else if (CurrentColorScheme == ColorSchemeTypes.Teal)
			CurrentColorScheme = ColorSchemeTypes.Red;

		// Update and Smawn New Starship Prefab
		if (CurrentMode == DemoModes.PremadeStarshipViewer)
			SpawnStarship ();
		else if (CurrentMode == DemoModes.JumpgateViewer) {
			SpawnJumpgate ();
			UpdateGUIElements ();
		}
		else if (CurrentMode == DemoModes.StardockViewer) {
			SpawnStardock ();
			UpdateGUIElements ();
		}
	}

	public void ChangeStarshipType() {
		if (CurrentStarshipType == StarshipTypes.Small)
			CurrentStarshipType = StarshipTypes.Medium;
		else if (CurrentStarshipType == StarshipTypes.Medium)
			CurrentStarshipType = StarshipTypes.Large;
		else if (CurrentStarshipType == StarshipTypes.Large)
			CurrentStarshipType = StarshipTypes.Carrier;
		else if (CurrentStarshipType == StarshipTypes.Carrier)
			CurrentStarshipType = StarshipTypes.Small;

		if (CurrentMode == DemoModes.JumpgateViewer) {
			// No Carrier Types
			if (CurrentStarshipType == StarshipTypes.Carrier)
				CurrentStarshipType = StarshipTypes.Small;
		}

		CurrentStarshipIndex = 0;

		// Update and Smawn New Starship Prefab
		if (CurrentMode == DemoModes.PremadeStarshipViewer)
			SpawnStarship ();
		else if (CurrentMode == DemoModes.JumpgateViewer) {
			SpawnJumpgate ();
			UpdateGUIElements ();
		}
		else if (CurrentMode == DemoModes.StardockViewer) {
			SpawnStardock ();
			UpdateGUIElements ();
		}
		SetupShipTypeCamera (CurrentStarshipType);
	}

	public void ChangeStarshipNumber() {
		if (CurrentStarshipType == StarshipTypes.Small) {

			if (CurrentStarshipIndex < SmallPrefabs_Red.Length - 1) {
				CurrentStarshipIndex++;
			} else {
				CurrentStarshipIndex = 0;
			}

		} else if (CurrentStarshipType == StarshipTypes.Medium) {

			if (CurrentStarshipIndex < MediumPrefabs_Red.Length - 1) {
				CurrentStarshipIndex++;
			} else {
				CurrentStarshipIndex = 0;
			}

		} else if (CurrentStarshipType == StarshipTypes.Large) {

			if (CurrentStarshipIndex < LargePrefabs_Red.Length - 1) {
				CurrentStarshipIndex++;
			} else {
				CurrentStarshipIndex = 0;
			}

		} else if (CurrentStarshipType == StarshipTypes.Carrier) {

			if (CurrentStarshipIndex < CarrierPrefabs_Red.Length - 1) {
				CurrentStarshipIndex++;
			} else {
				CurrentStarshipIndex = 0;
			}

		}

		// Update and Smawn New Starship Prefab
		if (CurrentMode == DemoModes.PremadeStarshipViewer)
			SpawnStarship ();
		else if (CurrentMode == DemoModes.JumpgateViewer) {
			UpdateGUIElements ();
		}
		else if (CurrentMode == DemoModes.StardockViewer) {
			UpdateGUIElements ();
		}
	}

	private void UpdateSelectedFleetStarshipIcon() {
		// Update Starship Icon
		if (FleetColorScheme == ColorSchemeTypes.Gray) {
			// Update Starship Icon - Gray
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				FleetStarshipIcon.sprite = CarrierIcons_Gray [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				FleetStarshipIcon.sprite = LargeIcons_Gray [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				FleetStarshipIcon.sprite = MediumIcons_Gray [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				FleetStarshipIcon.sprite = SmallIcons_Gray [CurrentStarshipIndex];
			}
		}
		else if (FleetColorScheme == ColorSchemeTypes.Blue) {
			// Update Starship Icon - Blue
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				FleetStarshipIcon.sprite = CarrierIcons_Blue [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				FleetStarshipIcon.sprite = LargeIcons_Blue [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				FleetStarshipIcon.sprite = MediumIcons_Blue [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				FleetStarshipIcon.sprite = SmallIcons_Blue [CurrentStarshipIndex];
			}
		}
		else if (FleetColorScheme == ColorSchemeTypes.Green) {
			// Update Starship Icon - Green
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				FleetStarshipIcon.sprite = CarrierIcons_Green [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				FleetStarshipIcon.sprite = LargeIcons_Green [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				FleetStarshipIcon.sprite = MediumIcons_Green [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				FleetStarshipIcon.sprite = SmallIcons_Green [CurrentStarshipIndex];
			}
		}
		else if (FleetColorScheme == ColorSchemeTypes.Purple) {
			// Update Starship Icon - Purple
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				FleetStarshipIcon.sprite = CarrierIcons_Purple [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				FleetStarshipIcon.sprite = LargeIcons_Purple [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				FleetStarshipIcon.sprite = MediumIcons_Purple [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				FleetStarshipIcon.sprite = SmallIcons_Purple [CurrentStarshipIndex];
			}
		}
		else if (FleetColorScheme == ColorSchemeTypes.Red) {
			// Update Starship Icon - Red
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				FleetStarshipIcon.sprite = CarrierIcons_Red [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				FleetStarshipIcon.sprite = LargeIcons_Red [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				FleetStarshipIcon.sprite = MediumIcons_Red [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				FleetStarshipIcon.sprite = SmallIcons_Red [CurrentStarshipIndex];
			}
		}
		else if (FleetColorScheme == ColorSchemeTypes.Teal) {
			// Update Starship Icon - Teal
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				FleetStarshipIcon.sprite = CarrierIcons_Teal [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Large) {
				FleetStarshipIcon.sprite = LargeIcons_Teal [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Medium) {
				FleetStarshipIcon.sprite = MediumIcons_Teal [CurrentStarshipIndex];
			} else if (CurrentStarshipType == StarshipTypes.Small) {
				FleetStarshipIcon.sprite = SmallIcons_Teal [CurrentStarshipIndex];
			}
		}
	}

	private Ray ray;
	private RaycastHit rayHit;
	private void DoStarshipMouseSelection() {
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out rayHit, 500)) {
			if (rayHit.collider.gameObject.name != null) {
				CurrentShipSelected = rayHit.collider.transform.root.gameObject.GetComponentInChildren<ShipMouseSelection> ();
				if (CurrentShipSelected != null) {
					Debug.Log ("Selected: " + CurrentShipSelected.gameObject.name + " Type: " + CurrentShipSelected.Type.ToString () + " Index: " + CurrentShipSelected.ShipIndex.ToString ());

					CurrentStarshipIndex = CurrentShipSelected.ShipIndex;
					CurrentStarshipType = CurrentShipSelected.Type;
					StarshipTypeText.text = CurrentShipSelected.gameObject.name;
					UpdateGUIElements ();
					if (CurrentShipSelected != null) {
						ShipSelectionImage.enabled = true;
						Vector3 selectionImagePos = Camera.main.WorldToScreenPoint (CurrentShipSelected.transform.position);
						ShipSelectionRect.anchoredPosition = new Vector2 (selectionImagePos.x, selectionImagePos.y);
						Debug.Log ("Ship Selection Pos: " + " x:" + selectionImagePos.x.ToString () + " y:" + selectionImagePos.y.ToString ());
					}

					UpdateSelectedFleetStarshipIcon ();
				}
			}
		} else {
			ShipSelectionImage.enabled = false;
			CurrentShipSelected = null;
		}
	}

	public void ChangeCurrentFleetColorScheme() {

		if (FleetColorScheme == ColorSchemeTypes.Red)
			FleetColorScheme = ColorSchemeTypes.Gray;
		else if (FleetColorScheme == ColorSchemeTypes.Gray)
			FleetColorScheme = ColorSchemeTypes.Blue;
		else if (FleetColorScheme == ColorSchemeTypes.Blue)
			FleetColorScheme = ColorSchemeTypes.Green;
		else if (FleetColorScheme == ColorSchemeTypes.Green)
			FleetColorScheme = ColorSchemeTypes.Purple;
		else if (FleetColorScheme == ColorSchemeTypes.Purple)
			FleetColorScheme = ColorSchemeTypes.Teal;
		else if (FleetColorScheme == ColorSchemeTypes.Teal)
			FleetColorScheme = ColorSchemeTypes.Red;
		
		CurrentShipSelected = null;
		SpawnCurrentFleet ();
	}

	private void SpawnCurrentFleet() {
		ClearCurrentFleet ();
		if (FleetColorScheme == ColorSchemeTypes.Red) {
			SpawnFleetShip (CarrierPrefabs_Red[0], CarrierLocations [0], StarshipTypes.Carrier, 0, ColorSchemeTypes.Red);
			SpawnFleetShip (CarrierPrefabs_Red[1], CarrierLocations [1], StarshipTypes.Carrier, 1, ColorSchemeTypes.Red);
			SpawnFleetShip (CarrierPrefabs_Red[2], CarrierLocations [2], StarshipTypes.Carrier, 2, ColorSchemeTypes.Red);
			SpawnFleetShip (CarrierPrefabs_Red[3], CarrierLocations [3], StarshipTypes.Carrier, 3, ColorSchemeTypes.Red);
			SpawnFleetShip (LargePrefabs_Red[0], LargeStarshipLocations [0], StarshipTypes.Large, 0, ColorSchemeTypes.Red);
			SpawnFleetShip (LargePrefabs_Red[1], LargeStarshipLocations [1], StarshipTypes.Large, 1, ColorSchemeTypes.Red);
			SpawnFleetShip (LargePrefabs_Red[2], LargeStarshipLocations [2], StarshipTypes.Large, 2, ColorSchemeTypes.Red);
			SpawnFleetShip (LargePrefabs_Red[3], LargeStarshipLocations [3], StarshipTypes.Large, 3, ColorSchemeTypes.Red);
			SpawnFleetShip (MediumPrefabs_Red[0], MediumStarshipLocations [0], StarshipTypes.Medium, 0, ColorSchemeTypes.Red);
			SpawnFleetShip (MediumPrefabs_Red[1], MediumStarshipLocations [1], StarshipTypes.Medium, 1, ColorSchemeTypes.Red);
			SpawnFleetShip (MediumPrefabs_Red[2], MediumStarshipLocations [2], StarshipTypes.Medium, 2, ColorSchemeTypes.Red);
			SpawnFleetShip (MediumPrefabs_Red[3], MediumStarshipLocations [3], StarshipTypes.Medium, 3, ColorSchemeTypes.Red);
			SpawnFleetShip (SmallPrefabs_Red[0], SmallStarshipLocations [0], StarshipTypes.Small, 0, ColorSchemeTypes.Red);
			SpawnFleetShip (SmallPrefabs_Red[1], SmallStarshipLocations [1], StarshipTypes.Small, 1, ColorSchemeTypes.Red);
			SpawnFleetShip (SmallPrefabs_Red[2], SmallStarshipLocations [2], StarshipTypes.Small, 2, ColorSchemeTypes.Red);
			SpawnFleetShip (SmallPrefabs_Red[3], SmallStarshipLocations [3], StarshipTypes.Small, 3, ColorSchemeTypes.Red);
			SpawnFleetShip (SmallPrefabs_Red[4], SmallStarshipLocations [4], StarshipTypes.Small, 4, ColorSchemeTypes.Red);
		}
		else if (FleetColorScheme == ColorSchemeTypes.Gray) {
			SpawnFleetShip (CarrierPrefabs_Gray[0], CarrierLocations [0], StarshipTypes.Carrier, 0, ColorSchemeTypes.Gray);
			SpawnFleetShip (CarrierPrefabs_Gray[1], CarrierLocations [1], StarshipTypes.Carrier, 1, ColorSchemeTypes.Gray);
			SpawnFleetShip (CarrierPrefabs_Gray[2], CarrierLocations [2], StarshipTypes.Carrier, 2, ColorSchemeTypes.Gray);
			SpawnFleetShip (CarrierPrefabs_Gray[3], CarrierLocations [3], StarshipTypes.Carrier, 3, ColorSchemeTypes.Gray);
			SpawnFleetShip (LargePrefabs_Gray[0], LargeStarshipLocations [0], StarshipTypes.Large, 0, ColorSchemeTypes.Gray);
			SpawnFleetShip (LargePrefabs_Gray[1], LargeStarshipLocations [1], StarshipTypes.Large, 1, ColorSchemeTypes.Gray);
			SpawnFleetShip (LargePrefabs_Gray[2], LargeStarshipLocations [2], StarshipTypes.Large, 2, ColorSchemeTypes.Gray);
			SpawnFleetShip (LargePrefabs_Gray[3], LargeStarshipLocations [3], StarshipTypes.Large, 3, ColorSchemeTypes.Gray);
			SpawnFleetShip (MediumPrefabs_Gray[0], MediumStarshipLocations [0], StarshipTypes.Medium, 0, ColorSchemeTypes.Gray);
			SpawnFleetShip (MediumPrefabs_Gray[1], MediumStarshipLocations [1], StarshipTypes.Medium, 1, ColorSchemeTypes.Gray);
			SpawnFleetShip (MediumPrefabs_Gray[2], MediumStarshipLocations [2], StarshipTypes.Medium, 2, ColorSchemeTypes.Gray);
			SpawnFleetShip (MediumPrefabs_Gray[3], MediumStarshipLocations [3], StarshipTypes.Medium, 3, ColorSchemeTypes.Gray);
			SpawnFleetShip (SmallPrefabs_Gray[0], SmallStarshipLocations [0], StarshipTypes.Small, 0, ColorSchemeTypes.Gray);
			SpawnFleetShip (SmallPrefabs_Gray[1], SmallStarshipLocations [1], StarshipTypes.Small, 1, ColorSchemeTypes.Gray);
			SpawnFleetShip (SmallPrefabs_Gray[2], SmallStarshipLocations [2], StarshipTypes.Small, 2, ColorSchemeTypes.Gray);
			SpawnFleetShip (SmallPrefabs_Gray[3], SmallStarshipLocations [3], StarshipTypes.Small, 3, ColorSchemeTypes.Gray);
			SpawnFleetShip (SmallPrefabs_Gray[4], SmallStarshipLocations [4], StarshipTypes.Small, 4, ColorSchemeTypes.Gray);
		}
		else if (FleetColorScheme == ColorSchemeTypes.Blue) {
			SpawnFleetShip (CarrierPrefabs_Blue[0], CarrierLocations [0], StarshipTypes.Carrier, 0, ColorSchemeTypes.Blue);
			SpawnFleetShip (CarrierPrefabs_Blue[1], CarrierLocations [1], StarshipTypes.Carrier, 1, ColorSchemeTypes.Blue);
			SpawnFleetShip (CarrierPrefabs_Blue[2], CarrierLocations [2], StarshipTypes.Carrier, 2, ColorSchemeTypes.Blue);
			SpawnFleetShip (CarrierPrefabs_Blue[3], CarrierLocations [3], StarshipTypes.Carrier, 3, ColorSchemeTypes.Blue);
			SpawnFleetShip (LargePrefabs_Blue[0], LargeStarshipLocations [0], StarshipTypes.Large, 0, ColorSchemeTypes.Blue);
			SpawnFleetShip (LargePrefabs_Blue[1], LargeStarshipLocations [1], StarshipTypes.Large, 1, ColorSchemeTypes.Blue);
			SpawnFleetShip (LargePrefabs_Blue[2], LargeStarshipLocations [2], StarshipTypes.Large, 2, ColorSchemeTypes.Blue);
			SpawnFleetShip (LargePrefabs_Blue[3], LargeStarshipLocations [3], StarshipTypes.Large, 3, ColorSchemeTypes.Blue);
			SpawnFleetShip (MediumPrefabs_Blue[0], MediumStarshipLocations [0], StarshipTypes.Medium, 0, ColorSchemeTypes.Blue);
			SpawnFleetShip (MediumPrefabs_Blue[1], MediumStarshipLocations [1], StarshipTypes.Medium, 1, ColorSchemeTypes.Blue);
			SpawnFleetShip (MediumPrefabs_Blue[2], MediumStarshipLocations [2], StarshipTypes.Medium, 2, ColorSchemeTypes.Blue);
			SpawnFleetShip (MediumPrefabs_Blue[3], MediumStarshipLocations [3], StarshipTypes.Medium, 3, ColorSchemeTypes.Blue);
			SpawnFleetShip (SmallPrefabs_Blue[0], SmallStarshipLocations [0], StarshipTypes.Small, 0, ColorSchemeTypes.Blue);
			SpawnFleetShip (SmallPrefabs_Blue[1], SmallStarshipLocations [1], StarshipTypes.Small, 1, ColorSchemeTypes.Blue);
			SpawnFleetShip (SmallPrefabs_Blue[2], SmallStarshipLocations [2], StarshipTypes.Small, 2, ColorSchemeTypes.Blue);
			SpawnFleetShip (SmallPrefabs_Blue[3], SmallStarshipLocations [3], StarshipTypes.Small, 3, ColorSchemeTypes.Blue);
			SpawnFleetShip (SmallPrefabs_Blue[4], SmallStarshipLocations [4], StarshipTypes.Small, 4, ColorSchemeTypes.Blue);
		}
		else if (FleetColorScheme == ColorSchemeTypes.Purple) {
			SpawnFleetShip (CarrierPrefabs_Purple[0], CarrierLocations [0], StarshipTypes.Carrier, 0, ColorSchemeTypes.Purple);
			SpawnFleetShip (CarrierPrefabs_Purple[1], CarrierLocations [1], StarshipTypes.Carrier, 1, ColorSchemeTypes.Purple);
			SpawnFleetShip (CarrierPrefabs_Purple[2], CarrierLocations [2], StarshipTypes.Carrier, 2, ColorSchemeTypes.Purple);
			SpawnFleetShip (CarrierPrefabs_Purple[3], CarrierLocations [3], StarshipTypes.Carrier, 3, ColorSchemeTypes.Purple);
			SpawnFleetShip (LargePrefabs_Purple[0], LargeStarshipLocations [0], StarshipTypes.Large, 0, ColorSchemeTypes.Purple);
			SpawnFleetShip (LargePrefabs_Purple[1], LargeStarshipLocations [1], StarshipTypes.Large, 1, ColorSchemeTypes.Purple);
			SpawnFleetShip (LargePrefabs_Purple[2], LargeStarshipLocations [2], StarshipTypes.Large, 2, ColorSchemeTypes.Purple);
			SpawnFleetShip (LargePrefabs_Purple[3], LargeStarshipLocations [3], StarshipTypes.Large, 3, ColorSchemeTypes.Purple);
			SpawnFleetShip (MediumPrefabs_Purple[0], MediumStarshipLocations [0], StarshipTypes.Medium, 0, ColorSchemeTypes.Purple);
			SpawnFleetShip (MediumPrefabs_Purple[1], MediumStarshipLocations [1], StarshipTypes.Medium, 1, ColorSchemeTypes.Purple);
			SpawnFleetShip (MediumPrefabs_Purple[2], MediumStarshipLocations [2], StarshipTypes.Medium, 2, ColorSchemeTypes.Purple);
			SpawnFleetShip (MediumPrefabs_Purple[3], MediumStarshipLocations [3], StarshipTypes.Medium, 3, ColorSchemeTypes.Purple);
			SpawnFleetShip (SmallPrefabs_Purple[0], SmallStarshipLocations [0], StarshipTypes.Small, 0, ColorSchemeTypes.Purple);
			SpawnFleetShip (SmallPrefabs_Purple[1], SmallStarshipLocations [1], StarshipTypes.Small, 1, ColorSchemeTypes.Purple);
			SpawnFleetShip (SmallPrefabs_Purple[2], SmallStarshipLocations [2], StarshipTypes.Small, 2, ColorSchemeTypes.Purple);
			SpawnFleetShip (SmallPrefabs_Purple[3], SmallStarshipLocations [3], StarshipTypes.Small, 3, ColorSchemeTypes.Purple);
			SpawnFleetShip (SmallPrefabs_Purple[4], SmallStarshipLocations [4], StarshipTypes.Small, 4, ColorSchemeTypes.Purple);
		}
		else if (FleetColorScheme == ColorSchemeTypes.Green) {
			SpawnFleetShip (CarrierPrefabs_Green[0], CarrierLocations [0], StarshipTypes.Carrier, 0, ColorSchemeTypes.Green);
			SpawnFleetShip (CarrierPrefabs_Green[1], CarrierLocations [1], StarshipTypes.Carrier, 1, ColorSchemeTypes.Green);
			SpawnFleetShip (CarrierPrefabs_Green[2], CarrierLocations [2], StarshipTypes.Carrier, 2, ColorSchemeTypes.Green);
			SpawnFleetShip (CarrierPrefabs_Green[3], CarrierLocations [3], StarshipTypes.Carrier, 3, ColorSchemeTypes.Green);
			SpawnFleetShip (LargePrefabs_Green[0], LargeStarshipLocations [0], StarshipTypes.Large, 0, ColorSchemeTypes.Green);
			SpawnFleetShip (LargePrefabs_Green[1], LargeStarshipLocations [1], StarshipTypes.Large, 1, ColorSchemeTypes.Green);
			SpawnFleetShip (LargePrefabs_Green[2], LargeStarshipLocations [2], StarshipTypes.Large, 2, ColorSchemeTypes.Green);
			SpawnFleetShip (LargePrefabs_Green[3], LargeStarshipLocations [3], StarshipTypes.Large, 3, ColorSchemeTypes.Green);
			SpawnFleetShip (MediumPrefabs_Green[0], MediumStarshipLocations [0], StarshipTypes.Medium, 0, ColorSchemeTypes.Green);
			SpawnFleetShip (MediumPrefabs_Green[1], MediumStarshipLocations [1], StarshipTypes.Medium, 1, ColorSchemeTypes.Green);
			SpawnFleetShip (MediumPrefabs_Green[2], MediumStarshipLocations [2], StarshipTypes.Medium, 2, ColorSchemeTypes.Green);
			SpawnFleetShip (MediumPrefabs_Green[3], MediumStarshipLocations [3], StarshipTypes.Medium, 3, ColorSchemeTypes.Green);
			SpawnFleetShip (SmallPrefabs_Green[0], SmallStarshipLocations [0], StarshipTypes.Small, 0, ColorSchemeTypes.Green);
			SpawnFleetShip (SmallPrefabs_Green[1], SmallStarshipLocations [1], StarshipTypes.Small, 1, ColorSchemeTypes.Green);
			SpawnFleetShip (SmallPrefabs_Green[2], SmallStarshipLocations [2], StarshipTypes.Small, 2, ColorSchemeTypes.Green);
			SpawnFleetShip (SmallPrefabs_Green[3], SmallStarshipLocations [3], StarshipTypes.Small, 3, ColorSchemeTypes.Green);
			SpawnFleetShip (SmallPrefabs_Green[4], SmallStarshipLocations [4], StarshipTypes.Small, 4, ColorSchemeTypes.Green);
		}
		else if (FleetColorScheme == ColorSchemeTypes.Teal) {
			SpawnFleetShip (CarrierPrefabs_Teal[0], CarrierLocations [0], StarshipTypes.Carrier, 0, ColorSchemeTypes.Teal);
			SpawnFleetShip (CarrierPrefabs_Teal[1], CarrierLocations [1], StarshipTypes.Carrier, 1, ColorSchemeTypes.Teal);
			SpawnFleetShip (CarrierPrefabs_Teal[2], CarrierLocations [2], StarshipTypes.Carrier, 2, ColorSchemeTypes.Teal);
			SpawnFleetShip (CarrierPrefabs_Teal[3], CarrierLocations [3], StarshipTypes.Carrier, 3, ColorSchemeTypes.Teal);
			SpawnFleetShip (LargePrefabs_Teal[0], LargeStarshipLocations [0], StarshipTypes.Large, 0, ColorSchemeTypes.Teal);
			SpawnFleetShip (LargePrefabs_Teal[1], LargeStarshipLocations [1], StarshipTypes.Large, 1, ColorSchemeTypes.Teal);
			SpawnFleetShip (LargePrefabs_Teal[2], LargeStarshipLocations [2], StarshipTypes.Large, 2, ColorSchemeTypes.Teal);
			SpawnFleetShip (LargePrefabs_Teal[3], LargeStarshipLocations [3], StarshipTypes.Large, 3, ColorSchemeTypes.Teal);
			SpawnFleetShip (MediumPrefabs_Teal[0], MediumStarshipLocations [0], StarshipTypes.Medium, 0, ColorSchemeTypes.Teal);
			SpawnFleetShip (MediumPrefabs_Teal[1], MediumStarshipLocations [1], StarshipTypes.Medium, 1, ColorSchemeTypes.Teal);
			SpawnFleetShip (MediumPrefabs_Teal[2], MediumStarshipLocations [2], StarshipTypes.Medium, 2, ColorSchemeTypes.Teal);
			SpawnFleetShip (MediumPrefabs_Teal[3], MediumStarshipLocations [3], StarshipTypes.Medium, 3, ColorSchemeTypes.Teal);
			SpawnFleetShip (SmallPrefabs_Teal[0], SmallStarshipLocations [0], StarshipTypes.Small, 0, ColorSchemeTypes.Teal);
			SpawnFleetShip (SmallPrefabs_Teal[1], SmallStarshipLocations [1], StarshipTypes.Small, 1, ColorSchemeTypes.Teal);
			SpawnFleetShip (SmallPrefabs_Teal[2], SmallStarshipLocations [2], StarshipTypes.Small, 2, ColorSchemeTypes.Teal);
			SpawnFleetShip (SmallPrefabs_Teal[3], SmallStarshipLocations [3], StarshipTypes.Small, 3, ColorSchemeTypes.Teal);
			SpawnFleetShip (SmallPrefabs_Teal[4], SmallStarshipLocations [4], StarshipTypes.Small, 4, ColorSchemeTypes.Teal);
		}
	}

	public GameObject SpawnMainMenuShip(Transform spawnLocationTransform, StarshipTypes typeOfStarship, int shipIndex, ColorSchemeTypes coloring) {

		// Spawn New Starship
		if (coloring == ColorSchemeTypes.Red) {
			if (typeOfStarship == StarshipTypes.Small) {
				return CreateMainMenuStarship (SmallPrefabs_Red [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return CreateMainMenuStarship (MediumPrefabs_Red [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return CreateMainMenuStarship (LargePrefabs_Red [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CreateMainMenuStarship (CarrierPrefabs_Red [shipIndex]);
			}
		}
		else if (coloring == ColorSchemeTypes.Gray) {
			if (typeOfStarship == StarshipTypes.Small) {
				return CreateMainMenuStarship (SmallPrefabs_Gray [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return CreateMainMenuStarship (MediumPrefabs_Gray [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return CreateMainMenuStarship (LargePrefabs_Gray [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CreateMainMenuStarship (CarrierPrefabs_Gray [shipIndex]);
			}
		}
		else if (coloring == ColorSchemeTypes.Blue) {
			if (typeOfStarship == StarshipTypes.Small) {
				return CreateMainMenuStarship (SmallPrefabs_Blue [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return CreateMainMenuStarship (MediumPrefabs_Blue [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return CreateMainMenuStarship (LargePrefabs_Blue [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CreateMainMenuStarship (CarrierPrefabs_Blue [shipIndex]);
			}
		}
		else if (coloring == ColorSchemeTypes.Green) {
			if (typeOfStarship == StarshipTypes.Small) {
				return CreateMainMenuStarship (SmallPrefabs_Green [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return CreateMainMenuStarship (MediumPrefabs_Green [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return CreateMainMenuStarship (LargePrefabs_Green [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CreateMainMenuStarship (CarrierPrefabs_Green [shipIndex]);
			}
		}
		else if (coloring == ColorSchemeTypes.Purple) {
			if (typeOfStarship == StarshipTypes.Small) {
				return CreateMainMenuStarship (SmallPrefabs_Purple [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return CreateMainMenuStarship (MediumPrefabs_Purple [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return CreateMainMenuStarship (LargePrefabs_Purple [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CreateMainMenuStarship (CarrierPrefabs_Purple [shipIndex]);
			}
		}
		else if (coloring == ColorSchemeTypes.Teal) {
			if (typeOfStarship == StarshipTypes.Small) {
				return CreateMainMenuStarship (SmallPrefabs_Teal [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return CreateMainMenuStarship (MediumPrefabs_Teal [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return CreateMainMenuStarship (LargePrefabs_Teal [shipIndex]);
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CreateMainMenuStarship (CarrierPrefabs_Teal [shipIndex]);
			}
		}

		return CreateMainMenuStarship (CarrierPrefabs_Teal [CurrentStarshipIndex]);
	}

	private GameObject CreateMainMenuStarship(GameObject prefabToUse) {

		Vector3 spawnPosition = Vector3.zero;

		GameObject newStarshipBaseGO = new GameObject (prefabToUse.name);
		if (CurrentMode == DemoModes.PremadeStarshipViewer)
			CurrentStarship = newStarshipBaseGO;
		StarshipMovement moveScript = newStarshipBaseGO.AddComponent<StarshipMovement> ();
		StarshipHealth healthScript = newStarshipBaseGO.AddComponent<StarshipHealth> ();

		GameObject newStarshipPrefab = GameObject.Instantiate (prefabToUse, spawnPosition, Quaternion.identity) as GameObject;
		newStarshipPrefab.name = prefabToUse.name + "_Model";
		newStarshipPrefab.transform.parent = newStarshipBaseGO.transform;
		if (CurrentStarshipType == StarshipTypes.Carrier) {
			moveScript.SetupMovement (newStarshipPrefab.transform, 1.0f, 3.25f, 7.5f, 0.15f);
			healthScript.SetupStarshipHealth (80);
		} else if (CurrentStarshipType == StarshipTypes.Large) {
			moveScript.SetupMovement (newStarshipPrefab.transform, 1.25f, 1.5f, 7.5f, 0.25f);
			healthScript.SetupStarshipHealth (100);
		} else if (CurrentStarshipType == StarshipTypes.Medium) {
			moveScript.SetupMovement (newStarshipPrefab.transform, 1.0f, 1.25f, 7.5f, 0.15f);
			healthScript.SetupStarshipHealth (50);
		} else if (CurrentStarshipType == StarshipTypes.Small) {
			moveScript.SetupMovement (newStarshipPrefab.transform, 0.5f, 1.25f, 7.5f, 0.05f);
			healthScript.SetupStarshipHealth (10);
		}

		if (CurrentMode == DemoModes.PremadeStarshipViewer) {
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				carrierScript = newStarshipPrefab.GetComponent<CarrierManager> ();
			} else {
				carrierScript = null;
			}

			// Add Debris Script
			GenerateDebrisOnDestroy debrisScript = newStarshipPrefab.AddComponent<GenerateDebrisOnDestroy>();
			debrisScript.StarshipType = CurrentStarshipType;
			debrisScript.StarshipColoring = (StarshipDebrisColors)CurrentColorScheme;
			CurrentDebrisScript = debrisScript;
		}

		return newStarshipBaseGO;
	}

	private void SpawnFleetShip(GameObject prefabToUse, Transform spawnLocation, StarshipTypes typeOfStarship, int indexIn, ColorSchemeTypes coloring) {
		
		GameObject newStarshipBaseGO = new GameObject (prefabToUse.name);
		if (CurrentMode == DemoModes.PremadeStarshipViewer)
			CurrentStarship = newStarshipBaseGO;
		StarshipMovement moveScript = newStarshipBaseGO.AddComponent<StarshipMovement> ();
		StarshipHealth healthScript = newStarshipBaseGO.AddComponent<StarshipHealth> ();
		if (healthScript != null) {
			if (typeOfStarship == StarshipTypes.Carrier)
				healthScript.SetupStarshipHealth (80);
			else if (typeOfStarship == StarshipTypes.Large)
				healthScript.SetupStarshipHealth (100);
			else if (typeOfStarship == StarshipTypes.Medium)
				healthScript.SetupStarshipHealth (60);
			else if (typeOfStarship == StarshipTypes.Small)
				healthScript.SetupStarshipHealth (20);
		}

		GameObject newStarshipPrefab = GameObject.Instantiate (prefabToUse, spawnLocation.position, Quaternion.identity) as GameObject;
		newStarshipPrefab.name = prefabToUse.name + "_Model";
		newStarshipPrefab.transform.parent = newStarshipBaseGO.transform;
		if (CurrentStarshipType == StarshipTypes.Carrier)
			moveScript.SetupMovement (newStarshipPrefab.transform, 1.0f, 3.25f, 7.5f, 0.15f);
		else if (CurrentStarshipType == StarshipTypes.Large)
			moveScript.SetupMovement (newStarshipPrefab.transform, 1.25f, 1.5f, 7.5f, 0.25f);
		else if (CurrentStarshipType == StarshipTypes.Medium)
			moveScript.SetupMovement (newStarshipPrefab.transform, 1.0f, 1.25f, 7.5f, 0.15f);
		else if (CurrentStarshipType == StarshipTypes.Small)
			moveScript.SetupMovement (newStarshipPrefab.transform, 0.5f, 1.25f, 7.5f, 0.05f);

		ShipMouseSelection mouseSelectionScript = newStarshipPrefab.AddComponent<ShipMouseSelection> ();
		mouseSelectionScript.Type = typeOfStarship;
		mouseSelectionScript.ShipIndex = indexIn;
		mouseSelectionScript.Coloring = coloring;

		// Add To Fleet List
		SpawnedFleetList.Add (newStarshipBaseGO);
	}

	private void ClearCurrentFleet() {
		if (SpawnedFleetList != null) {
			if (SpawnedFleetList.Count > 0) {
				for (int i = 0; i < SpawnedFleetList.Count; i++) {
					Destroy (SpawnedFleetList [i]);
				}
			}
			SpawnedFleetList.Clear ();
		}
	}

	private void ClearCurrentStarship() {
		if (CurrentStarship != null) {
			Destroy (CurrentStarship.gameObject);
			CurrentStarship = null;
		}
	}

	private void ClearCurrentStardock() {
		if (CurrentStardock != null) {
			CurrentStardockScript.DestroyInConstructionStarship ();
			CurrentStardockScript = null;
			Destroy (CurrentStardock.gameObject);
			CurrentStardock = null;
		}
	}

	public void StardockSpawn() {
		if (CurrentStardockScript != null) {
			CurrentStardockScript.TypeToBuild = CurrentStarshipType;
			CurrentStardockScript.ShipToBuildIndex = CurrentStarshipIndex;
			CurrentStardockScript.IsDemoStarDock = true;
			CurrentStardockScript.DestroyBuiltStarship = true;
			CurrentStardockScript.TestBuildStarship = true;
		}
	}

	private void SpawnStardock() {
		// Clear Old Stardock
		ClearCurrentStardock();

		CreateStardock(StarshipPrefabManager.GlobalAccess.GetStardockPrefab(CurrentStarshipType, CurrentColorScheme));
	}

	private void ClearCurrentJumpgate() {
		if (CurrentJumpgate != null) {
			CurrentJumpgateScript = null;
			Destroy (CurrentJumpgate.gameObject);
			CurrentJumpgate = null;
		}
	}

	public void JumpgateSpawn() {
		if (CurrentJumpgateScript != null) {
			CurrentJumpgateScript.TestShipIndex = CurrentStarshipIndex;
			CurrentJumpgateScript.DestroySpawnedStarship = true;
			CurrentJumpgateScript.TestSpawnStarship = true;
		}
	}

	private void SpawnJumpgate() {
		// Clear Old Jumpgate
		ClearCurrentJumpgate();

		CreateJumpgate(StarshipPrefabManager.GlobalAccess.GetJumpgatePrefab(CurrentStarshipType, CurrentColorScheme));
	}

	private void SpawnStarship() {
		// Transfer To Old
		ClearCurrentStarship();

		// Spawn New Starship
		if (CurrentColorScheme == ColorSchemeTypes.Red) {
			if (CurrentStarshipType == StarshipTypes.Small) {
				CreateStarship (SmallPrefabs_Red [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Medium) {
				CreateStarship (MediumPrefabs_Red [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Large) {
				CreateStarship (LargePrefabs_Red [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Carrier) {
				CreateStarship (CarrierPrefabs_Red [CurrentStarshipIndex]);
			}
		}
		else if (CurrentColorScheme == ColorSchemeTypes.Gray) {
			if (CurrentStarshipType == StarshipTypes.Small) {
				CreateStarship (SmallPrefabs_Gray [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Medium) {
				CreateStarship (MediumPrefabs_Gray [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Large) {
				CreateStarship (LargePrefabs_Gray [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Carrier) {
				CreateStarship (CarrierPrefabs_Gray [CurrentStarshipIndex]);
			}
		}
		else if (CurrentColorScheme == ColorSchemeTypes.Blue) {
			if (CurrentStarshipType == StarshipTypes.Small) {
				CreateStarship (SmallPrefabs_Blue [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Medium) {
				CreateStarship (MediumPrefabs_Blue [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Large) {
				CreateStarship (LargePrefabs_Blue [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Carrier) {
				CreateStarship (CarrierPrefabs_Blue [CurrentStarshipIndex]);
			}
		}
		else if (CurrentColorScheme == ColorSchemeTypes.Green) {
			if (CurrentStarshipType == StarshipTypes.Small) {
				CreateStarship (SmallPrefabs_Green [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Medium) {
				CreateStarship (MediumPrefabs_Green [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Large) {
				CreateStarship (LargePrefabs_Green [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Carrier) {
				CreateStarship (CarrierPrefabs_Green [CurrentStarshipIndex]);
			}
		}
		else if (CurrentColorScheme == ColorSchemeTypes.Purple) {
			if (CurrentStarshipType == StarshipTypes.Small) {
				CreateStarship (SmallPrefabs_Purple [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Medium) {
				CreateStarship (MediumPrefabs_Purple [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Large) {
				CreateStarship (LargePrefabs_Purple [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Carrier) {
				CreateStarship (CarrierPrefabs_Purple [CurrentStarshipIndex]);
			}
		}
		else if (CurrentColorScheme == ColorSchemeTypes.Teal) {
			if (CurrentStarshipType == StarshipTypes.Small) {
				CreateStarship (SmallPrefabs_Teal [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Medium) {
				CreateStarship (MediumPrefabs_Teal [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Large) {
				CreateStarship (LargePrefabs_Teal [CurrentStarshipIndex]);
			}
			else if (CurrentStarshipType == StarshipTypes.Carrier) {
				CreateStarship (CarrierPrefabs_Teal [CurrentStarshipIndex]);
			}
		}

		UpdateGUIElements ();
		SetupShipTypeCamera (CurrentStarshipType);
	}

	private CarrierManager carrierScript;
	public GameObject CarrierButtonPanelGO;
	public GameObject OpenDoorsButton;
	public GameObject CloseDoorsButton;
	private void UpdateCarrierControls() {
		if (CurrentMode == DemoModes.PremadeStarshipViewer) {
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				if (!CarrierButtonPanelGO.activeSelf)
					CarrierButtonPanelGO.SetActive (true);
				if (carrierScript != null) {
					if (carrierScript.DoorsOpen) {
						if (OpenDoorsButton.activeSelf)
							OpenDoorsButton.SetActive (false);		
						if (!CloseDoorsButton.activeSelf)
							CloseDoorsButton.SetActive (true);		
					} else {
						if (!OpenDoorsButton.activeSelf)
							OpenDoorsButton.SetActive (true);		
						if (CloseDoorsButton.activeSelf)
							CloseDoorsButton.SetActive (false);
					}
				}
			} else {
				if (CarrierButtonPanelGO.activeSelf)
					CarrierButtonPanelGO.SetActive (false);
			}
		} else if (CurrentMode == DemoModes.FleetViewer) {
			if (CarrierButtonPanelGO.activeSelf)
				CarrierButtonPanelGO.SetActive (false);
		}
	}
	public void OpenBayDoors() {
		if (CurrentStarshipType == StarshipTypes.Carrier) {
			if (carrierScript != null) {
				carrierScript.OpenBayDoors = true;
			}
		}
	}
	public void CloseBayDoors() {
		if (CurrentStarshipType == StarshipTypes.Carrier) {
			if (carrierScript != null) {
				carrierScript.CloseBayDoors = true;
			}
		}
	}
	public void DestroyCurrentStarship() {
		if (CurrentDebrisScript != null) {
			CurrentDebrisScript.DestroyStarship ();
			spawnStarshipDelayTimer = 0;
		}
	}

	private void CreateJumpgate(GameObject prefabToUse) {

		Vector3 spawnPosition = Vector3.zero;

		GameObject newStarshipBaseGO = new GameObject (prefabToUse.name);
		CurrentJumpgate = newStarshipBaseGO;
		StarshipHealth healthScript = newStarshipBaseGO.AddComponent<StarshipHealth> ();

		GameObject newStarshipPrefab = GameObject.Instantiate (prefabToUse, spawnPosition, Quaternion.identity) as GameObject;
		CurrentJumpgateScript = newStarshipPrefab.GetComponent<JumpgateController> ();
		newStarshipPrefab.name = prefabToUse.name + "_Model";
		newStarshipPrefab.transform.parent = newStarshipBaseGO.transform;
		if (CurrentStarshipType == StarshipTypes.Carrier) {
			healthScript.SetupStarshipHealth (80);
		} else if (CurrentStarshipType == StarshipTypes.Large) {
			healthScript.SetupStarshipHealth (100);
		} else if (CurrentStarshipType == StarshipTypes.Medium) {
			healthScript.SetupStarshipHealth (50);
		} else if (CurrentStarshipType == StarshipTypes.Small) {
			healthScript.SetupStarshipHealth (10);
		}
//
//		if (CurrentMode == DemoModes.PremadeStarshipViewer) {
//			if (CurrentStarshipType == StarshipTypes.Carrier) {
//				carrierScript = newStarshipPrefab.GetComponent<CarrierManager> ();
//			} else {
//				carrierScript = null;
//			}
//
//			// Add Debris Script
//			GenerateDebrisOnDestroy debrisScript = newStarshipPrefab.AddComponent<GenerateDebrisOnDestroy>();
//			debrisScript.StarshipType = CurrentStarshipType;
//			debrisScript.StarshipColoring = (StarshipDebrisColors)CurrentColorScheme;
//			CurrentDebrisScript = debrisScript;
//		}
	}

	private void CreateStardock(GameObject prefabToUse) {

		Vector3 spawnPosition = Vector3.zero;

		GameObject newStarshipBaseGO = new GameObject (prefabToUse.name);
		CurrentStardock = newStarshipBaseGO;
		StarshipHealth healthScript = newStarshipBaseGO.AddComponent<StarshipHealth> ();

		GameObject newStarshipPrefab = GameObject.Instantiate (prefabToUse, spawnPosition, Quaternion.identity) as GameObject;
		CurrentStardockScript = newStarshipPrefab.GetComponent<StardockController> ();
		newStarshipPrefab.name = prefabToUse.name + "_Model";
		newStarshipPrefab.transform.parent = newStarshipBaseGO.transform;
		if (CurrentStarshipType == StarshipTypes.Carrier) {
			healthScript.SetupStarshipHealth (80);
		} else if (CurrentStarshipType == StarshipTypes.Large) {
			healthScript.SetupStarshipHealth (100);
		} else if (CurrentStarshipType == StarshipTypes.Medium) {
			healthScript.SetupStarshipHealth (50);
		} else if (CurrentStarshipType == StarshipTypes.Small) {
			healthScript.SetupStarshipHealth (10);
		}
		//
		//		if (CurrentMode == DemoModes.PremadeStarshipViewer) {
		//			if (CurrentStarshipType == StarshipTypes.Carrier) {
		//				carrierScript = newStarshipPrefab.GetComponent<CarrierManager> ();
		//			} else {
		//				carrierScript = null;
		//			}
		//
		//			// Add Debris Script
		//			GenerateDebrisOnDestroy debrisScript = newStarshipPrefab.AddComponent<GenerateDebrisOnDestroy>();
		//			debrisScript.StarshipType = CurrentStarshipType;
		//			debrisScript.StarshipColoring = (StarshipDebrisColors)CurrentColorScheme;
		//			CurrentDebrisScript = debrisScript;
		//		}
	}

	private void CreateStarship(GameObject prefabToUse) {

		Vector3 spawnPosition = Vector3.zero;

		GameObject newStarshipBaseGO = new GameObject (prefabToUse.name);
		if (CurrentMode == DemoModes.PremadeStarshipViewer)
			CurrentStarship = newStarshipBaseGO;
		StarshipMovement moveScript = newStarshipBaseGO.AddComponent<StarshipMovement> ();
		StarshipHealth healthScript = newStarshipBaseGO.AddComponent<StarshipHealth> ();

		GameObject newStarshipPrefab = GameObject.Instantiate (prefabToUse, spawnPosition, Quaternion.identity) as GameObject;
		newStarshipPrefab.name = prefabToUse.name + "_Model";
		newStarshipPrefab.transform.parent = newStarshipBaseGO.transform;
		if (CurrentStarshipType == StarshipTypes.Carrier) {
			moveScript.SetupMovement (newStarshipPrefab.transform, 1.0f, 3.25f, 7.5f, 0.15f);
			healthScript.SetupStarshipHealth (80);
		} else if (CurrentStarshipType == StarshipTypes.Large) {
			moveScript.SetupMovement (newStarshipPrefab.transform, 1.25f, 1.5f, 7.5f, 0.25f);
			healthScript.SetupStarshipHealth (100);
		} else if (CurrentStarshipType == StarshipTypes.Medium) {
			moveScript.SetupMovement (newStarshipPrefab.transform, 1.0f, 1.25f, 7.5f, 0.15f);
			healthScript.SetupStarshipHealth (50);
		} else if (CurrentStarshipType == StarshipTypes.Small) {
			moveScript.SetupMovement (newStarshipPrefab.transform, 0.5f, 1.25f, 7.5f, 0.05f);
			healthScript.SetupStarshipHealth (10);
		}

		if (CurrentMode == DemoModes.PremadeStarshipViewer) {
			if (CurrentStarshipType == StarshipTypes.Carrier) {
				carrierScript = newStarshipPrefab.GetComponent<CarrierManager> ();
			} else {
				carrierScript = null;
			}

			// Add Debris Script
			GenerateDebrisOnDestroy debrisScript = newStarshipPrefab.AddComponent<GenerateDebrisOnDestroy>();
			debrisScript.StarshipType = CurrentStarshipType;
			debrisScript.StarshipColoring = (StarshipDebrisColors)CurrentColorScheme;
			CurrentDebrisScript = debrisScript;
		}
	}
}
