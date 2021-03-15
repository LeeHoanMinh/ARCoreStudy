using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarshipPrefabManager : MonoBehaviour {
	public static StarshipPrefabManager GlobalAccess;
	void Awake() {
		GlobalAccess = this;
	}

	[Space(5)]

	// Color Scheme = Red
	public GameObject[] CarrierPrefabs_Red;
	public GameObject[] LargePrefabs_Red;
	public GameObject[] MediumPrefabs_Red;
	public GameObject[] SmallPrefabs_Red;
	public GameObject[] JumpgatePrefabs_Red;
	public GameObject[] StardockPrefabs_Red;
	public GameObject[] StarbasePrefabs_Red;

	[Space(5)]

	// Color Scheme = Gray
	public GameObject[] CarrierPrefabs_Gray;
	public GameObject[] LargePrefabs_Gray;
	public GameObject[] MediumPrefabs_Gray;
	public GameObject[] SmallPrefabs_Gray;
	public GameObject[] JumpgatePrefabs_Gray;
	public GameObject[] StardockPrefabs_Gray;
	public GameObject[] StarbasePrefabs_Gray;

	[Space(5)]

	// Color Scheme = Blue
	public GameObject[] CarrierPrefabs_Blue;
	public GameObject[] LargePrefabs_Blue;
	public GameObject[] MediumPrefabs_Blue;
	public GameObject[] SmallPrefabs_Blue;
	public GameObject[] JumpgatePrefabs_Blue;
	public GameObject[] StardockPrefabs_Blue;
	public GameObject[] StarbasePrefabs_Blue;

	[Space(5)]

	// Color Scheme = Green
	public GameObject[] CarrierPrefabs_Green;
	public GameObject[] LargePrefabs_Green;
	public GameObject[] MediumPrefabs_Green;
	public GameObject[] SmallPrefabs_Green;
	public GameObject[] JumpgatePrefabs_Green;
	public GameObject[] StardockPrefabs_Green;
	public GameObject[] StarbasePrefabs_Green;

	[Space(5)]

	// Color Scheme = Purple
	public GameObject[] CarrierPrefabs_Purple;
	public GameObject[] LargePrefabs_Purple;
	public GameObject[] MediumPrefabs_Purple;
	public GameObject[] SmallPrefabs_Purple;
	public GameObject[] JumpgatePrefabs_Purple;
	public GameObject[] StardockPrefabs_Purple;
	public GameObject[] StarbasePrefabs_Purple;

	[Space(5)]

	// Color Scheme = Teal
	public GameObject[] CarrierPrefabs_Teal;
	public GameObject[] LargePrefabs_Teal;
	public GameObject[] MediumPrefabs_Teal;
	public GameObject[] SmallPrefabs_Teal;
	public GameObject[] JumpgatePrefabs_Teal;
	public GameObject[] StardockPrefabs_Teal;
	public GameObject[] StarbasePrefabs_Teal;

	[Space(5)]

	// Material Sets
	public Material[] MaterialSet_Red;
	public Material[] MaterialSet_Gray;
	public Material[] MaterialSet_Blue;
	public Material[] MaterialSet_Green;
	public Material[] MaterialSet_Purple;
	public Material[] MaterialSet_Teal;

	[Space(5)]

	public Material[] ConstructionMaterialSet;

	public AudioClip ConstructionSoundFX;

	// Small Nano Construction Effects
	public ParticleSystem NanobeamHitSystem_Small;
	public void AddNanobeamHitSparksSmall(Vector3 position) {
		if (NanobeamHitSystem_Small != null) {
			NanobeamHitSystem_Small.transform.position = position;
			NanobeamHitSystem_Small.Emit (Random.Range (4, 6));
		}
	}
	public ParticleSystem PrimeNanoEffectSmallSystem;
	public void AddSmallPrimeNanoEffects(Vector3 position) {
		if (PrimeNanoEffectSmallSystem != null) {
			PrimeNanoEffectSmallSystem.transform.position = position;
			PrimeNanoEffectSmallSystem.Emit (Random.Range (10, 16));
		}
	}
	// Medium Nano Construction Effects
	public ParticleSystem NanobeamHitSystem_Medium;
	public void AddNanobeamHitSparksMedium(Vector3 position) {
		if (NanobeamHitSystem_Medium != null) {
			NanobeamHitSystem_Medium.transform.position = position;
			NanobeamHitSystem_Medium.Emit (Random.Range (4, 6));
		}
	}
	public ParticleSystem PrimeNanoEffectMediumSystem;
	public void AddMediumPrimeNanoEffects(Vector3 position) {
		if (PrimeNanoEffectMediumSystem != null) {
			PrimeNanoEffectMediumSystem.transform.position = position;
			PrimeNanoEffectMediumSystem.Emit (Random.Range (10, 16));
		}
	}
	// Large Nano Construction Effects
	public ParticleSystem NanobeamHitSystem_Large;
	public void AddNanobeamHitSparksLarge(Vector3 position) {
		if (NanobeamHitSystem_Large != null) {
			NanobeamHitSystem_Large.transform.position = position;
			NanobeamHitSystem_Large.Emit (Random.Range (4, 6));
		}
	}
	public ParticleSystem PrimeNanoEffectLargeSystem;
	public void AddLargePrimeNanoEffects(Vector3 position) {
		if (PrimeNanoEffectLargeSystem != null) {
			PrimeNanoEffectLargeSystem.transform.position = position;
			PrimeNanoEffectLargeSystem.Emit (Random.Range (10, 16));
		}
	}


	public ParticleSystem ConstructionSparksSystem;
	public ParticleSystem SmallConstructionSparksSystem;
	public void AddConstructionSparks(Vector3 position) {
		if (ConstructionSparksSystem != null) {
			ConstructionSparksSystem.transform.position = position;
			ConstructionSparksSystem.Emit (Random.Range (6, 12));
		}
	}
	public void AddSmallConstructionSparks(Vector3 position) {
		if (SmallConstructionSparksSystem != null) {
			SmallConstructionSparksSystem.transform.position = position;
			SmallConstructionSparksSystem.Emit (Random.Range (6, 12));
		}
	}

	public Material[] GetConstructionMaterials() {
		return ConstructionMaterialSet;
	}

	public Material[] GetMaterialSet(ColorSchemeTypes coloringToGet) {
		if (coloringToGet == ColorSchemeTypes.Red)
			return MaterialSet_Red;
		else if (coloringToGet == ColorSchemeTypes.Gray)
			return MaterialSet_Gray;
		else if (coloringToGet == ColorSchemeTypes.Blue)
			return MaterialSet_Blue;
		else if (coloringToGet == ColorSchemeTypes.Green)
			return MaterialSet_Green;
		else if (coloringToGet == ColorSchemeTypes.Purple)
			return MaterialSet_Purple;
		else if (coloringToGet == ColorSchemeTypes.Teal)
			return MaterialSet_Teal;
		else
			return MaterialSet_Red;
	}

	public GameObject GetJumpgatePrefab(StarshipTypes typeOfStarship, ColorSchemeTypes coloring) {
		if (typeOfStarship == StarshipTypes.Small) {
			if (coloring == ColorSchemeTypes.Red)
				return JumpgatePrefabs_Red [0];
			else if (coloring == ColorSchemeTypes.Gray)
				return JumpgatePrefabs_Gray [0];
			else if (coloring == ColorSchemeTypes.Blue)
				return JumpgatePrefabs_Blue [0];
			else if (coloring == ColorSchemeTypes.Green)
				return JumpgatePrefabs_Green [0];
			else if (coloring == ColorSchemeTypes.Purple)
				return JumpgatePrefabs_Purple [0];
			else if (coloring == ColorSchemeTypes.Teal)
				return JumpgatePrefabs_Teal [0];
		}
		else if (typeOfStarship == StarshipTypes.Medium) {
			if (coloring == ColorSchemeTypes.Red)
				return JumpgatePrefabs_Red [1];
			else if (coloring == ColorSchemeTypes.Gray)
				return JumpgatePrefabs_Gray [1];
			else if (coloring == ColorSchemeTypes.Blue)
				return JumpgatePrefabs_Blue [1];
			else if (coloring == ColorSchemeTypes.Green)
				return JumpgatePrefabs_Green [1];
			else if (coloring == ColorSchemeTypes.Purple)
				return JumpgatePrefabs_Purple [1];
			else if (coloring == ColorSchemeTypes.Teal)
				return JumpgatePrefabs_Teal [1];
		}
		else {
			if (coloring == ColorSchemeTypes.Red)
				return JumpgatePrefabs_Red [2];
			else if (coloring == ColorSchemeTypes.Gray)
				return JumpgatePrefabs_Gray [2];
			else if (coloring == ColorSchemeTypes.Blue)
				return JumpgatePrefabs_Blue [2];
			else if (coloring == ColorSchemeTypes.Green)
				return JumpgatePrefabs_Green [2];
			else if (coloring == ColorSchemeTypes.Purple)
				return JumpgatePrefabs_Purple [2];
			else if (coloring == ColorSchemeTypes.Teal)
				return JumpgatePrefabs_Teal [2];
		}

		return JumpgatePrefabs_Red [0];
	}

	public GameObject GetStardockPrefab(StarshipTypes typeOfStarship, ColorSchemeTypes coloring) {
		if (typeOfStarship == StarshipTypes.Small) {
			if (coloring == ColorSchemeTypes.Red)
				return StardockPrefabs_Red [0];
			else if (coloring == ColorSchemeTypes.Gray)
				return StardockPrefabs_Gray [0];
			else if (coloring == ColorSchemeTypes.Blue)
				return StardockPrefabs_Blue [0];
			else if (coloring == ColorSchemeTypes.Green)
				return StardockPrefabs_Green [0];
			else if (coloring == ColorSchemeTypes.Purple)
				return StardockPrefabs_Purple [0];
			else if (coloring == ColorSchemeTypes.Teal)
				return StardockPrefabs_Teal [0];
		}
		else if (typeOfStarship == StarshipTypes.Medium) {
			if (coloring == ColorSchemeTypes.Red)
				return StardockPrefabs_Red [1];
			else if (coloring == ColorSchemeTypes.Gray)
				return StardockPrefabs_Gray [1];
			else if (coloring == ColorSchemeTypes.Blue)
				return StardockPrefabs_Blue [1];
			else if (coloring == ColorSchemeTypes.Green)
				return StardockPrefabs_Green [1];
			else if (coloring == ColorSchemeTypes.Purple)
				return StardockPrefabs_Purple [1];
			else if (coloring == ColorSchemeTypes.Teal)
				return StardockPrefabs_Teal [1];
		}
		else {
			if (coloring == ColorSchemeTypes.Red)
				return StardockPrefabs_Red [2];
			else if (coloring == ColorSchemeTypes.Gray)
				return StardockPrefabs_Gray [2];
			else if (coloring == ColorSchemeTypes.Blue)
				return StardockPrefabs_Blue [2];
			else if (coloring == ColorSchemeTypes.Green)
				return StardockPrefabs_Green [2];
			else if (coloring == ColorSchemeTypes.Purple)
				return StardockPrefabs_Purple [2];
			else if (coloring == ColorSchemeTypes.Teal)
				return StardockPrefabs_Teal [2];
		}

		return StardockPrefabs_Red [0];
	}

	public GameObject GetStarbasePrefab(StarshipTypes typeOfStarship, ColorSchemeTypes coloring) {
		if (typeOfStarship == StarshipTypes.Small) {
			if (coloring == ColorSchemeTypes.Red)
				return StarbasePrefabs_Red [0];
			else if (coloring == ColorSchemeTypes.Gray)
				return StarbasePrefabs_Gray [0];
			else if (coloring == ColorSchemeTypes.Blue)
				return StarbasePrefabs_Blue [0];
			else if (coloring == ColorSchemeTypes.Green)
				return StarbasePrefabs_Green [0];
			else if (coloring == ColorSchemeTypes.Purple)
				return StarbasePrefabs_Purple [0];
			else if (coloring == ColorSchemeTypes.Teal)
				return StarbasePrefabs_Teal [0];
		}
		else if (typeOfStarship == StarshipTypes.Medium) {
			if (coloring == ColorSchemeTypes.Red)
				return StarbasePrefabs_Red [1];
			else if (coloring == ColorSchemeTypes.Gray)
				return StarbasePrefabs_Gray [1];
			else if (coloring == ColorSchemeTypes.Blue)
				return StarbasePrefabs_Blue [1];
			else if (coloring == ColorSchemeTypes.Green)
				return StarbasePrefabs_Green [1];
			else if (coloring == ColorSchemeTypes.Purple)
				return StarbasePrefabs_Purple [1];
			else if (coloring == ColorSchemeTypes.Teal)
				return StarbasePrefabs_Teal [1];
		}
		else {
			if (coloring == ColorSchemeTypes.Red)
				return StarbasePrefabs_Red [2];
			else if (coloring == ColorSchemeTypes.Gray)
				return StarbasePrefabs_Gray [2];
			else if (coloring == ColorSchemeTypes.Blue)
				return StarbasePrefabs_Blue [2];
			else if (coloring == ColorSchemeTypes.Green)
				return StarbasePrefabs_Green [2];
			else if (coloring == ColorSchemeTypes.Purple)
				return StarbasePrefabs_Purple [2];
			else if (coloring == ColorSchemeTypes.Teal)
				return StarbasePrefabs_Teal [2];
		}

		return StardockPrefabs_Red [0];
	}

	public GameObject GetStarshipPrefab(StarshipTypes typeOfStarship, ColorSchemeTypes coloring, int shipIndex) {

		// Spawn New Starship
		if (coloring == ColorSchemeTypes.Red) {
			if (typeOfStarship == StarshipTypes.Small) {
				return SmallPrefabs_Red [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return MediumPrefabs_Red [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return LargePrefabs_Red [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CarrierPrefabs_Red [shipIndex];
			}
		}
		else if (coloring == ColorSchemeTypes.Gray) {
			if (typeOfStarship == StarshipTypes.Small) {
				return SmallPrefabs_Gray [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return MediumPrefabs_Gray [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return LargePrefabs_Gray [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CarrierPrefabs_Gray [shipIndex];
			}
		}
		else if (coloring == ColorSchemeTypes.Blue) {
			if (typeOfStarship == StarshipTypes.Small) {
				return SmallPrefabs_Blue [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return MediumPrefabs_Blue [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return LargePrefabs_Blue [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CarrierPrefabs_Blue [shipIndex];
			}
		}
		else if (coloring == ColorSchemeTypes.Green) {
			if (typeOfStarship == StarshipTypes.Small) {
				return SmallPrefabs_Green [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return MediumPrefabs_Green [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return LargePrefabs_Green [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CarrierPrefabs_Green [shipIndex];
			}
		}
		else if (coloring == ColorSchemeTypes.Purple) {
			if (typeOfStarship == StarshipTypes.Small) {
				return SmallPrefabs_Purple [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return MediumPrefabs_Purple [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return LargePrefabs_Purple [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CarrierPrefabs_Purple [shipIndex];
			}
		}
		else if (coloring == ColorSchemeTypes.Teal) {
			if (typeOfStarship == StarshipTypes.Small) {
				return SmallPrefabs_Teal [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Medium) {
				return MediumPrefabs_Teal [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Large) {
				return LargePrefabs_Teal [shipIndex];
			}
			else if (typeOfStarship == StarshipTypes.Carrier) {
				return CarrierPrefabs_Teal [shipIndex];
			}
		}

		return null;
	}

}
