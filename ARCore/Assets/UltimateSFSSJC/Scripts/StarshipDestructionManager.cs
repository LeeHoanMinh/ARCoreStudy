using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Starship destruction manager. Scirpt Manages Destruction of Starships based on Color Scheme and Type.
/// </summary>
public class StarshipDestructionManager : MonoBehaviour {
	public static StarshipDestructionManager GlobalAccess;
	void Awake () {
		GlobalAccess = this;
	}

	public GameObject CarrierDebrisFSPrefab;
	public GameObject LargeDebrisFSPrefab;
	public GameObject MediumDebrisFSPrefab;
	public GameObject SmallDebrisFSPrefab;

	[Space(6)]

	public GameObject CarrierSubExplosionPrefab;
	public GameObject LargeSubExplosionPrefab;
	public GameObject MediumSubExplosionPrefab;
	public GameObject SmallSubExplosionPrefab;

	[Space(6)]

	public GameObject CarrierMainExplosionPrefab;
	public GameObject LargeMainExplosionPrefab;
	public GameObject MediumMainExplosionPrefab;
	public GameObject SmallMainExplosionPrefab;

	[Space(12)]

	public Material BloodTexture01_Red;
	public Material BloodTexture02_Red;
	public Material BloodTexture03_Red;
	public Material BloodTexture04_Red;
	public Material BloodStardockTexture01_Red;
	public Material BloodStarbaseTexture01_Red;
	public Material BloodStarbaseTexture02_Red;

	[Space(12)]

	public Material BloodTexture01_Gray;
	public Material BloodTexture02_Gray;
	public Material BloodTexture03_Gray;
	public Material BloodTexture04_Gray;
	public Material BloodStardockTexture01_Gray;
	public Material BloodStarbaseTexture01_Gray;
	public Material BloodStarbaseTexture02_Gray;

	[Space(12)]

	public Material BloodTexture01_Blue;
	public Material BloodTexture02_Blue;
	public Material BloodTexture03_Blue;
	public Material BloodTexture04_Blue;
	public Material BloodStardockTexture01_Blue;
	public Material BloodStarbaseTexture01_Blue;
	public Material BloodStarbaseTexture02_Blue;

	[Space(12)]

	public Material BloodTexture01_Green;
	public Material BloodTexture02_Green;
	public Material BloodTexture03_Green;
	public Material BloodTexture04_Green;
	public Material BloodStardockTexture01_Green;
	public Material BloodStarbaseTexture01_Green;
	public Material BloodStarbaseTexture02_Green;

	[Space(12)]

	public Material BloodTexture01_Purple;
	public Material BloodTexture02_Purple;
	public Material BloodTexture03_Purple;
	public Material BloodTexture04_Purple;
	public Material BloodStardockTexture01_Purple;
	public Material BloodStarbaseTexture01_Purple;
	public Material BloodStarbaseTexture02_Purple;

	[Space(12)]

	public Material BloodTexture01_Teal;
	public Material BloodTexture02_Teal;
	public Material BloodTexture03_Teal;
	public Material BloodTexture04_Teal;
	public Material BloodStardockTexture01_Teal;
	public Material BloodStarbaseTexture01_Teal;
	public Material BloodStarbaseTexture02_Teal;

	[Space(12)]

	public Material BloodTexture01_DebrisMaterial;
	public Material BloodTexture02_DebrisMaterial;
	public Material BloodTexture03_DebrisMaterial;
	public Material BloodStardock01_DebrisMaterial;
	public Material BloodStarbase01_DebrisMaterial;
	public Material BloodStarbase02_DebrisMaterial;
	public Material BloodTexture04_DebrisMaterial;

	public void GetDebrisMaterials(StarshipTypes typeToGenerate, StarshipDebrisColors colorToGenerate, BFPStarshipDebris scriptToUpdate, Transform[] debrisTransforms) {
		// Generate Starship and Stardock Debris

		if (typeToGenerate == StarshipTypes.Carrier) {
			GameObject newCarrierExplosion = GameObject.Instantiate (CarrierMainExplosionPrefab, scriptToUpdate.transform.position, scriptToUpdate.transform.rotation) as GameObject;
			newCarrierExplosion.name = "CarrierExplosion";
			GameObject newFlamesSmokePrefab = GameObject.Instantiate (CarrierDebrisFSPrefab, scriptToUpdate.transform.position, scriptToUpdate.transform.rotation) as GameObject;
			newFlamesSmokePrefab.transform.parent = scriptToUpdate.transform;
		} else if (typeToGenerate == StarshipTypes.Large) {
			GameObject newLargeExplosion = GameObject.Instantiate (LargeMainExplosionPrefab, scriptToUpdate.transform.position, scriptToUpdate.transform.rotation) as GameObject;
			newLargeExplosion.name = "LargeExplosion";
			GameObject newFlamesSmokePrefab = GameObject.Instantiate (LargeDebrisFSPrefab, scriptToUpdate.transform.position, scriptToUpdate.transform.rotation) as GameObject;
			newFlamesSmokePrefab.transform.parent = scriptToUpdate.transform;
		} else if (typeToGenerate == StarshipTypes.Medium) {
			GameObject newMediumExplosion = GameObject.Instantiate (MediumMainExplosionPrefab, scriptToUpdate.transform.position, scriptToUpdate.transform.rotation) as GameObject;
			newMediumExplosion.name = "MediumExplosion";
			GameObject newFlamesSmokePrefab = GameObject.Instantiate (MediumDebrisFSPrefab, scriptToUpdate.transform.position, scriptToUpdate.transform.rotation) as GameObject;
			newFlamesSmokePrefab.transform.parent = scriptToUpdate.transform;
		} else if (typeToGenerate == StarshipTypes.Small) {
			GameObject newSmallExplosion = GameObject.Instantiate (SmallMainExplosionPrefab, scriptToUpdate.transform.position, scriptToUpdate.transform.rotation) as GameObject;
			newSmallExplosion.name = "SmallExplosion";
			GameObject newFlamesSmokePrefab = GameObject.Instantiate (SmallDebrisFSPrefab, scriptToUpdate.transform.position, scriptToUpdate.transform.rotation) as GameObject;
			newFlamesSmokePrefab.transform.parent = scriptToUpdate.transform;
		}

		if (colorToGenerate == StarshipDebrisColors.Gray) {
			if (typeToGenerate == StarshipTypes.Carrier) 
				scriptToUpdate.SetupMaterials (debrisTransforms, CarrierSubExplosionPrefab, CarrierDebrisFSPrefab, BloodTexture01_Gray, BloodTexture02_Gray, BloodTexture03_Gray, BloodStardockTexture01_Gray, BloodStarbaseTexture01_Gray, BloodStarbaseTexture02_Gray, BloodTexture04_Gray, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Large) 
				scriptToUpdate.SetupMaterials (debrisTransforms, LargeSubExplosionPrefab, LargeDebrisFSPrefab, BloodTexture01_Gray, BloodTexture02_Gray, BloodTexture03_Gray, BloodStardockTexture01_Gray, BloodStarbaseTexture01_Gray, BloodStarbaseTexture02_Gray, BloodTexture04_Gray, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Medium) 
				scriptToUpdate.SetupMaterials (debrisTransforms, MediumSubExplosionPrefab, MediumDebrisFSPrefab, BloodTexture01_Gray, BloodTexture02_Gray, BloodTexture03_Gray, BloodStardockTexture01_Gray, BloodStarbaseTexture01_Gray, BloodStarbaseTexture02_Gray, BloodTexture04_Gray, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Small) 
				scriptToUpdate.SetupMaterials (debrisTransforms, SmallSubExplosionPrefab, SmallDebrisFSPrefab, BloodTexture01_Gray, BloodTexture02_Gray, BloodTexture03_Gray, BloodStardockTexture01_Gray, BloodStarbaseTexture01_Gray, BloodStarbaseTexture02_Gray, BloodTexture04_Gray, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
		}
		else if (colorToGenerate == StarshipDebrisColors.Red) {
			if (typeToGenerate == StarshipTypes.Carrier) 
				scriptToUpdate.SetupMaterials (debrisTransforms, CarrierSubExplosionPrefab, CarrierDebrisFSPrefab, BloodTexture01_Red, BloodTexture02_Red, BloodTexture03_Red, BloodStardockTexture01_Red, BloodStarbaseTexture01_Red, BloodStarbaseTexture02_Red, BloodTexture04_Red, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Large) 
				scriptToUpdate.SetupMaterials (debrisTransforms, LargeSubExplosionPrefab, LargeDebrisFSPrefab, BloodTexture01_Red, BloodTexture02_Red, BloodTexture03_Red, BloodStardockTexture01_Red, BloodStarbaseTexture01_Red, BloodStarbaseTexture02_Red, BloodTexture04_Red, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Medium) 
				scriptToUpdate.SetupMaterials (debrisTransforms, MediumSubExplosionPrefab, MediumDebrisFSPrefab, BloodTexture01_Red, BloodTexture02_Red, BloodTexture03_Red, BloodStardockTexture01_Red, BloodStarbaseTexture01_Red, BloodStarbaseTexture02_Red, BloodTexture04_Red, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Small) 
				scriptToUpdate.SetupMaterials (debrisTransforms, SmallSubExplosionPrefab, SmallDebrisFSPrefab, BloodTexture01_Red, BloodTexture02_Red, BloodTexture03_Red, BloodStardockTexture01_Red, BloodStarbaseTexture01_Red, BloodStarbaseTexture02_Red, BloodTexture04_Red, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
		}
		else if (colorToGenerate == StarshipDebrisColors.Blue) {
			if (typeToGenerate == StarshipTypes.Carrier) 
				scriptToUpdate.SetupMaterials (debrisTransforms, CarrierSubExplosionPrefab, CarrierDebrisFSPrefab, BloodTexture01_Blue, BloodTexture02_Blue, BloodTexture03_Blue, BloodStardockTexture01_Blue, BloodStarbaseTexture01_Blue, BloodStarbaseTexture02_Blue, BloodTexture04_Blue, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Large) 
				scriptToUpdate.SetupMaterials (debrisTransforms, LargeSubExplosionPrefab, LargeDebrisFSPrefab, BloodTexture01_Blue, BloodTexture02_Blue, BloodTexture03_Blue, BloodStardockTexture01_Blue, BloodStarbaseTexture01_Blue, BloodStarbaseTexture02_Blue, BloodTexture04_Blue, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Medium) 
				scriptToUpdate.SetupMaterials (debrisTransforms, MediumSubExplosionPrefab, MediumDebrisFSPrefab, BloodTexture01_Blue, BloodTexture02_Blue, BloodTexture03_Blue, BloodStardockTexture01_Blue, BloodStarbaseTexture01_Blue, BloodStarbaseTexture02_Blue, BloodTexture04_Blue, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Small) 
				scriptToUpdate.SetupMaterials (debrisTransforms, SmallSubExplosionPrefab, SmallDebrisFSPrefab, BloodTexture01_Blue, BloodTexture02_Blue, BloodTexture03_Blue, BloodStardockTexture01_Blue, BloodStarbaseTexture01_Blue, BloodStarbaseTexture02_Blue, BloodTexture04_Blue, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
		}
		else if (colorToGenerate == StarshipDebrisColors.Green) {
			if (typeToGenerate == StarshipTypes.Carrier) 
				scriptToUpdate.SetupMaterials (debrisTransforms, CarrierSubExplosionPrefab, CarrierDebrisFSPrefab, BloodTexture01_Green, BloodTexture02_Green, BloodTexture03_Green, BloodStardockTexture01_Green, BloodStarbaseTexture01_Green, BloodStarbaseTexture02_Green, BloodTexture04_Green, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Large) 
				scriptToUpdate.SetupMaterials (debrisTransforms, LargeSubExplosionPrefab, LargeDebrisFSPrefab, BloodTexture01_Green, BloodTexture02_Green, BloodTexture03_Green, BloodStardockTexture01_Green, BloodStarbaseTexture01_Green, BloodStarbaseTexture02_Green, BloodTexture04_Green, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Medium) 
				scriptToUpdate.SetupMaterials (debrisTransforms, MediumSubExplosionPrefab, MediumDebrisFSPrefab, BloodTexture01_Green, BloodTexture02_Green, BloodTexture03_Green, BloodStardockTexture01_Green, BloodStarbaseTexture01_Green, BloodStarbaseTexture02_Green, BloodTexture04_Green, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Small) 
				scriptToUpdate.SetupMaterials (debrisTransforms, SmallSubExplosionPrefab, SmallDebrisFSPrefab, BloodTexture01_Green, BloodTexture02_Green, BloodTexture03_Green, BloodStardockTexture01_Green, BloodStarbaseTexture01_Green, BloodStarbaseTexture02_Green, BloodTexture04_Green, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
		}
		else if (colorToGenerate == StarshipDebrisColors.Purple) {
			if (typeToGenerate == StarshipTypes.Carrier) 
				scriptToUpdate.SetupMaterials (debrisTransforms, CarrierSubExplosionPrefab, CarrierDebrisFSPrefab, BloodTexture01_Purple, BloodTexture02_Purple, BloodTexture03_Purple, BloodStardockTexture01_Purple, BloodStarbaseTexture01_Purple, BloodStarbaseTexture02_Purple, BloodTexture04_Purple, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Large) 
				scriptToUpdate.SetupMaterials (debrisTransforms, LargeSubExplosionPrefab, LargeDebrisFSPrefab, BloodTexture01_Purple, BloodTexture02_Purple, BloodTexture03_Purple, BloodStardockTexture01_Purple, BloodStarbaseTexture01_Purple, BloodStarbaseTexture02_Purple, BloodTexture04_Purple, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Medium) 
				scriptToUpdate.SetupMaterials (debrisTransforms, MediumSubExplosionPrefab, MediumDebrisFSPrefab, BloodTexture01_Purple, BloodTexture02_Purple, BloodTexture03_Purple, BloodStardockTexture01_Purple, BloodStarbaseTexture01_Purple, BloodStarbaseTexture02_Purple, BloodTexture04_Purple, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Small) 
				scriptToUpdate.SetupMaterials (debrisTransforms, SmallSubExplosionPrefab, SmallDebrisFSPrefab, BloodTexture01_Purple, BloodTexture02_Purple, BloodTexture03_Purple, BloodStardockTexture01_Purple, BloodStarbaseTexture01_Purple, BloodStarbaseTexture02_Purple, BloodTexture04_Purple, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
		}
		else if (colorToGenerate == StarshipDebrisColors.Teal) {
			if (typeToGenerate == StarshipTypes.Carrier) 
				scriptToUpdate.SetupMaterials (debrisTransforms, CarrierSubExplosionPrefab, CarrierDebrisFSPrefab, BloodTexture01_Teal, BloodTexture02_Teal, BloodTexture03_Teal, BloodStardockTexture01_Teal, BloodStarbaseTexture01_Teal, BloodStarbaseTexture02_Teal, BloodTexture04_Teal, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Large) 
				scriptToUpdate.SetupMaterials (debrisTransforms, LargeSubExplosionPrefab, LargeDebrisFSPrefab, BloodTexture01_Teal, BloodTexture02_Teal, BloodTexture03_Teal, BloodStardockTexture01_Teal, BloodStarbaseTexture01_Teal, BloodStarbaseTexture02_Teal, BloodTexture04_Teal, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Medium) 
				scriptToUpdate.SetupMaterials (debrisTransforms, MediumSubExplosionPrefab, MediumDebrisFSPrefab, BloodTexture01_Teal, BloodTexture02_Teal, BloodTexture03_Teal, BloodStardockTexture01_Teal, BloodStarbaseTexture01_Teal, BloodStarbaseTexture02_Teal, BloodTexture04_Teal, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
			else if (typeToGenerate == StarshipTypes.Small) 
				scriptToUpdate.SetupMaterials (debrisTransforms, SmallSubExplosionPrefab, SmallDebrisFSPrefab, BloodTexture01_Teal, BloodTexture02_Teal, BloodTexture03_Teal, BloodStardockTexture01_Teal, BloodStarbaseTexture01_Teal, BloodStarbaseTexture02_Teal, BloodTexture04_Teal, BloodTexture01_DebrisMaterial, BloodTexture02_DebrisMaterial, BloodTexture03_DebrisMaterial, BloodStardock01_DebrisMaterial, BloodStarbase01_DebrisMaterial, BloodStarbase02_DebrisMaterial, BloodTexture04_DebrisMaterial);
		}
	}
}
