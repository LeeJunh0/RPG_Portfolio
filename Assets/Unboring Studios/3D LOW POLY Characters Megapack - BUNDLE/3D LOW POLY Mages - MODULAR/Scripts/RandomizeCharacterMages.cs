using System;
using TMPro;
using UnityEngine;

namespace UnboringFuture
{
    namespace CharactersMegapack
    {
        public class RandomizeCharacterMages : MonoBehaviour
        {
            public GameObject character;
            public Material[] materials;

            public GameObject[] leftHandWeapons;
            public GameObject[] rightHandWeapons;
            public GameObject[] accessoires;

            public GameObject[] headBaseCharModels;
            public GameObject[] hairBaseCharModels;
            public GameObject[] hairBaseCharModelsHeadArmorAllowed;
            public GameObject[] eyebrowsBaseCharModels;
            public GameObject[] beardBaseCharModels;
            public GameObject[] torsoBaseCharModels;
            public GameObject[] armsBaseCharModels;

            public GameObject[] headArmorModels;
            public GameObject[] shoulderArmorModels;
            public GameObject[] torsoArmorModels;
            public GameObject[] armsArmorModels;
            public GameObject[] handsArmorModels;
            public GameObject[] legsArmorModels;
            public GameObject[] feetArmorModels;

            // 0 = Male Human
            // 1 = Female Human
            // 2 = Male Orc
            // 3 = Female Orc
            public int baseCharType = 0;
            public bool showHelmet = true;
            public int weaponTypeLeftHand = 0;
            public int weaponTypeRightHand = 0;
            public bool showWeapons = true;
            public int accessoireType = 0;

            [HideInInspector] public GameObject modularHead;
            [HideInInspector] public GameObject modularTorso;
            [HideInInspector] public GameObject modularArms;
            [HideInInspector] public GameObject modularHair;
            [HideInInspector] public GameObject modularBeard;
            [HideInInspector] public GameObject modularEyebrows;

            [HideInInspector] public GameObject modularHeadArmor;
            [HideInInspector] public GameObject modularShouldersArmor;
            [HideInInspector] public GameObject modularTorsoArmor;
            [HideInInspector] public GameObject modularArmsArmor;
            [HideInInspector] public GameObject modularHandsArmor;
            [HideInInspector] public GameObject modularLegsArmor;
            [HideInInspector] public GameObject modularFeetArmor;
            [HideInInspector] public GameObject modularAccessoire;

            private readonly string[] headArmorsBaseCharRequired = new string[] {
        "BattleMageArmorHood",
        "BattleMageArmorHoodOpen",
        "ClericArmorHood",
        "DarkMageRobeHood",
        "DarkMageRobeHoodOpen",
        "DruidArmorHelmet",
        "DuelistArmorHood",
        "FireMageRobeHood",
        "FireMageRobeHoodOpen",
        "IceMageRobeCrownCrystals",
        "IceMageRobeCrownHigh",
        "WizardRobeHat"
    };
            private readonly string[] headArmorHairNotAllowed = new string[] {
        "DruidArmorHelmet"
    };
            private readonly string[] torsoArmorBaseCharRequired = new string[] {
        "BattleMageArmorTorso"
    };
            private readonly string[] armsArmorBaseCharRequired = new string[] {
        "BattleMageArmorArms"
    };

            void Start()
            {
                Randomize(false);
                SetPredefined(1);
            }

            public void ShowHelmet()
            {
                if (!showHelmet)
                {
                    showHelmet = true;
                    UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Hide Helmet");

                    character.transform.Find(modularHeadArmor.gameObject.name).gameObject.SetActive(true);
                    character.transform.Find(modularHead.gameObject.name).gameObject.SetActive(false);
                    character.transform.Find(modularEyebrows.gameObject.name).gameObject.SetActive(false);
                    character.transform.Find(modularHair.gameObject.name).gameObject.SetActive(false);
                    if (baseCharType == 0 || baseCharType == 2) character.transform.Find(modularBeard.gameObject.name).gameObject.SetActive(false);
                    if (Array.IndexOf(headArmorsBaseCharRequired, modularHeadArmor.gameObject.name) >= 0)
                    {
                        character.transform.Find(modularHead.gameObject.name).gameObject.SetActive(true);
                        character.transform.Find(modularEyebrows.gameObject.name).gameObject.SetActive(true);
                        if (baseCharType == 0 || baseCharType == 2) character.transform.Find(modularBeard.gameObject.name).gameObject.SetActive(true);
                    }
                }
                else
                {
                    showHelmet = false;
                    UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Show Helmet");

                    character.transform.Find(modularHeadArmor.gameObject.name).gameObject.SetActive(false);
                    character.transform.Find(modularHead.gameObject.name).gameObject.SetActive(true);
                    character.transform.Find(modularEyebrows.gameObject.name).gameObject.SetActive(true);
                    character.transform.Find(modularHair.gameObject.name).gameObject.SetActive(true);
                    if (baseCharType == 0 || baseCharType == 2) character.transform.Find(modularBeard.gameObject.name).gameObject.SetActive(true);
                }
            }

            public void ShowWeapons()
            {
                if (!showWeapons)
                {
                    showWeapons = true;
                    UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Hide Weapons");

                    RandomizeWeapon();
                }
                else
                {
                    showWeapons = false;
                    UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Show Weapons");

                    ResetWeapons();
                }
            }

            public void SetBaseCharType(int type)
            {
                baseCharType = type;

                Randomize(true);
                SetMaterialBaseChar(UnityEngine.Random.Range(0, materials.Length));
            }

            public void SetMaterialBaseChar(int material)
            {
                Material randomMaterial = materials[material];

                foreach (Transform modularPart in character.transform)
                {
                    if (!modularPart.gameObject.name.StartsWith("BaseChar"))
                    {
                        continue;
                    }

                    if (modularPart.GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        modularPart.GetComponent<SkinnedMeshRenderer>().material = randomMaterial;
                    }
                }
            }

            public void SetMaterialArmor(int material)
            {
                Material randomMaterial = materials[material];

                foreach (Transform modularPart in character.transform)
                {
                    if (modularPart.gameObject.name.StartsWith("BaseChar"))
                    {
                        continue;
                    }

                    if (modularPart.GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        modularPart.GetComponent<SkinnedMeshRenderer>().material = randomMaterial;
                    }
                }

                foreach (GameObject weapon in leftHandWeapons)
                {
                    weapon.GetComponent<MeshRenderer>().material = randomMaterial;
                }

                foreach (GameObject weapon in rightHandWeapons)
                {
                    weapon.GetComponent<MeshRenderer>().material = randomMaterial;
                }

                foreach (GameObject accessoire in accessoires)
                {
                    accessoire.GetComponent<MeshRenderer>().material = randomMaterial;
                }
            }

            public void Randomize(bool onlyBaseChar)
            {
                ResetCharacter();
                if (!onlyBaseChar && showWeapons) RandomizeWeapon();
                if (!onlyBaseChar) RandomizeAccessoires();

                modularHead = headBaseCharModels[baseCharType];
                modularTorso = torsoBaseCharModels[baseCharType];
                if (baseCharType == 0 || baseCharType == 1)
                {
                    modularArms = armsBaseCharModels[0];
                }
                else
                {
                    modularArms = armsBaseCharModels[1];
                }

                modularBeard = beardBaseCharModels[UnityEngine.Random.Range(0, beardBaseCharModels.Length)];
                modularEyebrows = eyebrowsBaseCharModels[UnityEngine.Random.Range(0, eyebrowsBaseCharModels.Length)];

                if (!onlyBaseChar)
                {
                    modularHeadArmor = headArmorModels[UnityEngine.Random.Range(0, headArmorModels.Length)];
                    modularShouldersArmor = shoulderArmorModels[UnityEngine.Random.Range(0, shoulderArmorModels.Length)];
                    modularTorsoArmor = torsoArmorModels[UnityEngine.Random.Range(0, torsoArmorModels.Length)];
                    modularArmsArmor = armsArmorModels[UnityEngine.Random.Range(0, armsArmorModels.Length)];
                    modularHandsArmor = handsArmorModels[UnityEngine.Random.Range(0, handsArmorModels.Length)];
                    modularLegsArmor = legsArmorModels[UnityEngine.Random.Range(0, legsArmorModels.Length)];
                    modularFeetArmor = feetArmorModels[UnityEngine.Random.Range(0, feetArmorModels.Length)];
                }

                if (Array.IndexOf(headArmorHairNotAllowed, modularHeadArmor.gameObject.name) < 0)
                {
                    if (!showHelmet)
                    {
                        modularHair = hairBaseCharModels[UnityEngine.Random.Range(0, hairBaseCharModels.Length)];
                    }
                    else
                    {
                        modularHair = hairBaseCharModelsHeadArmorAllowed[UnityEngine.Random.Range(0, hairBaseCharModelsHeadArmorAllowed.Length)];
                    }
                    character.transform.Find(modularHair.gameObject.name).gameObject.SetActive(true);
                }

                character.transform.Find(modularEyebrows.gameObject.name).gameObject.SetActive(true);
                if (showHelmet) character.transform.Find(modularHeadArmor.gameObject.name).gameObject.SetActive(true);
                if (Array.IndexOf(headArmorsBaseCharRequired, modularHeadArmor.gameObject.name) >= 0 || !showHelmet)
                {
                    character.transform.Find(modularHead.gameObject.name).gameObject.SetActive(true);
                    character.transform.Find(modularEyebrows.gameObject.name).gameObject.SetActive(true);
                    if (baseCharType == 0 || baseCharType == 2) character.transform.Find(modularBeard.gameObject.name).gameObject.SetActive(true);
                }
                if (modularShouldersArmor) character.transform.Find(modularShouldersArmor.gameObject.name).gameObject.SetActive(true);
                character.transform.Find(modularTorsoArmor.gameObject.name).gameObject.SetActive(true);
                if (Array.IndexOf(torsoArmorBaseCharRequired, modularTorsoArmor.gameObject.name) >= 0)
                {
                    character.transform.Find(modularTorso.gameObject.name).gameObject.SetActive(true);
                }
                character.transform.Find(modularArmsArmor.gameObject.name).gameObject.SetActive(true);
                if (Array.IndexOf(armsArmorBaseCharRequired, modularArmsArmor.gameObject.name) >= 0)
                {
                    character.transform.Find(modularArms.gameObject.name).gameObject.SetActive(true);
                }
                character.transform.Find(modularHandsArmor.gameObject.name).gameObject.SetActive(true);
                character.transform.Find(modularLegsArmor.gameObject.name).gameObject.SetActive(true);
                character.transform.Find(modularFeetArmor.gameObject.name).gameObject.SetActive(true);
            }

            private void ResetCharacter()
            {
                foreach (Transform modularPart in character.transform)
                {
                    // Base Char
                    if (modularPart.gameObject.name.StartsWith("BaseChar"))
                    {
                        modularPart.gameObject.SetActive(false);
                    }

                    // Helmet
                    if (modularPart.gameObject.name.Contains("Hat") || modularPart.gameObject.name.Contains("Helmet") || modularPart.gameObject.name.Contains("Hood") || modularPart.gameObject.name.Contains("Crown"))
                    {
                        modularPart.gameObject.SetActive(false);
                    }

                    // Shoulders
                    if (modularPart.gameObject.name.EndsWith("Shoulders"))
                    {
                        modularPart.gameObject.SetActive(false);
                    }

                    // Torso
                    if (modularPart.gameObject.name.EndsWith("Torso"))
                    {
                        modularPart.gameObject.SetActive(false);
                    }

                    // Arms
                    if (modularPart.gameObject.name.EndsWith("Arms"))
                    {
                        modularPart.gameObject.SetActive(false);
                    }

                    // Hands
                    if (modularPart.gameObject.name.EndsWith("Hands"))
                    {
                        modularPart.gameObject.SetActive(false);
                    }

                    // Legs
                    if (modularPart.gameObject.name.EndsWith("Legs"))
                    {
                        modularPart.gameObject.SetActive(false);
                    }

                    // Feet
                    if (modularPart.gameObject.name.EndsWith("Feet"))
                    {
                        modularPart.gameObject.SetActive(false);
                    }
                }
            }

            public void RandomizeWeapon()
            {
                ResetWeapons();

                weaponTypeLeftHand = UnityEngine.Random.Range(0, 7);
                weaponTypeRightHand = UnityEngine.Random.Range(0, 11);

                // 0 = Dark Staff
                // 1 = Druid Staff
                // 2 = Flower Staff
                // 3 = Holy Staff
                // 4 = Ice Staff
                // 5 = Wooden Staff
                // 6 = Wolf Staff

                switch (weaponTypeLeftHand)
                {
                    case 0:
                        leftHandWeapons[0].gameObject.SetActive(true);
                        break;
                    case 1:
                        leftHandWeapons[1].gameObject.SetActive(true);
                        break;
                    case 2:
                        leftHandWeapons[2].gameObject.SetActive(true);
                        break;
                    case 3:
                        leftHandWeapons[3].gameObject.SetActive(true);
                        break;
                    case 4:
                        leftHandWeapons[4].gameObject.SetActive(true);
                        break;
                    case 5:
                        leftHandWeapons[5].gameObject.SetActive(true);
                        break;
                    case 6:
                        leftHandWeapons[6].gameObject.SetActive(true);
                        break;
                }

                // 0 = Wooden Staff
                // 1 = Iron Sickle
                // 2 = Gladius
                // 3 = Shield Bear
                // 4 = Paladin Bear
                // 5 = None
                // 6 = None
                // 7 = None
                // 8 = None
                // 9 = None
                // 10 = None

                switch (weaponTypeRightHand)
                {
                    case 0:
                        rightHandWeapons[0].gameObject.SetActive(true);
                        break;
                    case 1:
                        rightHandWeapons[1].gameObject.SetActive(true);
                        break;
                    case 2:
                        rightHandWeapons[2].gameObject.SetActive(true);
                        break;
                    case 3:
                        rightHandWeapons[3].gameObject.SetActive(true);
                        break;
                    case 4:
                        rightHandWeapons[4].gameObject.SetActive(true);
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                }
            }

            public void ResetWeapons()
            {
                foreach (GameObject weapon in leftHandWeapons)
                {
                    weapon.SetActive(false);
                }

                foreach (GameObject weapon in rightHandWeapons)
                {
                    weapon.SetActive(false);
                }
            }

            public void RandomizeAccessoires()
            {
                ResetAccessoires();

                accessoireType = UnityEngine.Random.Range(0, 5);

                // 0 = Empty
                // 1 = Empty
                // 2 = Small Bag
                // 3 = Scabbard
                // 4 = Iron Sickle

                switch (accessoireType)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        accessoires[0].gameObject.SetActive(true);
                        break;
                    case 3:
                        accessoires[1].gameObject.SetActive(true);
                        break;
                    case 4:
                        accessoires[2].gameObject.SetActive(true);
                        break;
                }
            }

            public void ResetAccessoires()
            {
                accessoires[0].gameObject.SetActive(false);
                accessoires[1].gameObject.SetActive(false);
                accessoires[2].gameObject.SetActive(false);
            }

            public void SetPredefined(int type)
            {
                ResetCharacter();
                ResetWeapons();
                ResetAccessoires();

                // 0 = Battle Mage
                // 1 = Cleric
                // 2 = Dark Mage
                // 3 = Druid
                // 4 = Duelist
                // 5 = Fire Mage
                // 6 = Ice Mage
                // 7 = Wizard

                switch (type)
                {
                    // Battle Mage
                    case 0:
                        modularHeadArmor = headArmorModels[0];
                        modularShouldersArmor = shoulderArmorModels[0];
                        modularTorsoArmor = torsoArmorModels[0];
                        modularArmsArmor = armsArmorModels[0];
                        modularHandsArmor = handsArmorModels[0];
                        modularLegsArmor = legsArmorModels[0];
                        modularFeetArmor = feetArmorModels[0];
                        if (showWeapons) leftHandWeapons[6].gameObject.SetActive(true);
                        if (showWeapons) rightHandWeapons[3].gameObject.SetActive(true);
                        break;
                    // Cleric
                    case 1:
                        modularHeadArmor = headArmorModels[1];
                        modularShouldersArmor = shoulderArmorModels[1];
                        modularTorsoArmor = torsoArmorModels[1];
                        modularArmsArmor = armsArmorModels[1];
                        modularHandsArmor = handsArmorModels[1];
                        modularLegsArmor = legsArmorModels[1];
                        modularFeetArmor = feetArmorModels[1];
                        if (showWeapons) leftHandWeapons[3].gameObject.SetActive(true);
                        break;
                    // Dark Mage
                    case 2:
                        modularHeadArmor = headArmorModels[2];
                        modularShouldersArmor = null;
                        modularTorsoArmor = torsoArmorModels[2];
                        modularArmsArmor = armsArmorModels[2];
                        modularHandsArmor = handsArmorModels[2];
                        modularLegsArmor = legsArmorModels[2];
                        modularFeetArmor = feetArmorModels[2];
                        if (showWeapons) leftHandWeapons[0].gameObject.SetActive(true);
                        break;
                    // Druid
                    case 3:
                        modularHeadArmor = headArmorModels[3];
                        modularShouldersArmor = shoulderArmorModels[3];
                        modularTorsoArmor = torsoArmorModels[3];
                        modularArmsArmor = armsArmorModels[3];
                        modularHandsArmor = handsArmorModels[3];
                        modularLegsArmor = legsArmorModels[3];
                        modularFeetArmor = feetArmorModels[3];
                        if (showWeapons) leftHandWeapons[1].gameObject.SetActive(true);
                        if (showWeapons) rightHandWeapons[1].gameObject.SetActive(true);
                        break;
                    // Duelist
                    case 4:
                        modularHeadArmor = headArmorModels[4];
                        modularShouldersArmor = shoulderArmorModels[3];
                        modularTorsoArmor = torsoArmorModels[4];
                        modularArmsArmor = armsArmorModels[4];
                        modularHandsArmor = handsArmorModels[4];
                        modularLegsArmor = legsArmorModels[4];
                        modularFeetArmor = feetArmorModels[4];
                        if (showWeapons) leftHandWeapons[5].gameObject.SetActive(true);
                        if (showWeapons) rightHandWeapons[2].gameObject.SetActive(true);
                        break;
                    // Fire Mage
                    case 5:
                        modularHeadArmor = headArmorModels[5];
                        modularShouldersArmor = null;
                        modularTorsoArmor = torsoArmorModels[5];
                        modularArmsArmor = armsArmorModels[5];
                        modularHandsArmor = handsArmorModels[5];
                        modularLegsArmor = legsArmorModels[5];
                        modularFeetArmor = feetArmorModels[5];
                        if (showWeapons) leftHandWeapons[5].gameObject.SetActive(true);
                        break;
                    // Ice Mage
                    case 6:
                        modularHeadArmor = headArmorModels[6];
                        modularShouldersArmor = shoulderArmorModels[4];
                        modularTorsoArmor = torsoArmorModels[6];
                        modularArmsArmor = armsArmorModels[6];
                        modularHandsArmor = handsArmorModels[6];
                        modularLegsArmor = legsArmorModels[6];
                        modularFeetArmor = feetArmorModels[6];
                        if (showWeapons) leftHandWeapons[4].gameObject.SetActive(true);
                        break;
                    // Wizard
                    case 7:
                        modularHeadArmor = headArmorModels[8];
                        modularShouldersArmor = null;
                        modularTorsoArmor = torsoArmorModels[7];
                        modularArmsArmor = armsArmorModels[7];
                        modularHandsArmor = handsArmorModels[7];
                        modularLegsArmor = legsArmorModels[7];
                        modularFeetArmor = feetArmorModels[7];
                        if (showWeapons) leftHandWeapons[1].gameObject.SetActive(true);
                        break;
                }

                Randomize(true);
                SetMaterialBaseChar(UnityEngine.Random.Range(0, materials.Length));
            }
        }
    }
}
