using System;
using TMPro;
using UnityEngine;

namespace UnboringFuture
{
    namespace CharactersMegapack
    {
        public class RandomizeCharacter : MonoBehaviour
        {
            public GameObject character;
            public Material[] materials;

            public GameObject[] leftHandWeapons;
            public GameObject[] rightHandWeapons;
            public GameObject[] accessoires;

            public GameObject[] headBaseCharModels;
            public GameObject[] hairBaseCharModels;
            public GameObject[] eyebrowsBaseCharModels;
            public GameObject[] beardBaseCharModels;
            public GameObject[] torsoBaseCharModels;
            public GameObject[] armsBaseCharModels;

            public GameObject[] helmetArmorModels;
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
            public int weaponType = 0;
            public bool showWeapons = true;
            public int accessoireType = 0;

            [HideInInspector] public GameObject modularHead;
            [HideInInspector] public GameObject modularTorso;
            [HideInInspector] public GameObject modularArms;
            [HideInInspector] public GameObject modularHair;
            [HideInInspector] public GameObject modularBeard;
            [HideInInspector] public GameObject modularEyebrows;

            [HideInInspector] public GameObject modularHelmetArmor;
            [HideInInspector] public GameObject modularShouldersArmor;
            [HideInInspector] public GameObject modularTorsoArmor;
            [HideInInspector] public GameObject modularArmsArmor;
            [HideInInspector] public GameObject modularHandsArmor;
            [HideInInspector] public GameObject modularLegsArmor;
            [HideInInspector] public GameObject modularFeetArmor;
            [HideInInspector] public GameObject modularAccessoire;

            private readonly string[] helmetArmorsBaseCharRequired = new string[] {
        "BerserkArmorHelmet",
        "ChainArmorHelmet",
        "ChampionArmorHelmetVisorUp",
        "IronArmorHelmetVisorUp",
        "PaladinArmorHelmetVisorUp",
        "VikingArmorHelmet",
        "VikingArmorHelmetSmallVisor",
        "VikingArmorHelmetVisor",
        "VikingArmorHelmetVisorOld"
    };
            private readonly string[] torsoArmorBaseCharRequired = new string[] {
        "BerserkArmorTorso"
    };
            private readonly string[] armsArmorBaseCharRequired = new string[] {
        "BerserkArmorArms",
        "VikingArmorArms"
    };

            void Start()
            {
                Randomize(false);
            }

            public void ShowHelmet()
            {
                if (!showHelmet)
                {
                    showHelmet = true;
                    UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Hide Helmet");

                    character.transform.Find(modularHelmetArmor.gameObject.name).gameObject.SetActive(true);
                    character.transform.Find(modularHead.gameObject.name).gameObject.SetActive(false);
                    character.transform.Find(modularEyebrows.gameObject.name).gameObject.SetActive(false);
                    character.transform.Find(modularHair.gameObject.name).gameObject.SetActive(false);
                    if (baseCharType == 0 || baseCharType == 2) character.transform.Find(modularBeard.gameObject.name).gameObject.SetActive(false);
                    if (Array.IndexOf(helmetArmorsBaseCharRequired, modularHelmetArmor.gameObject.name) >= 0)
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

                    character.transform.Find(modularHelmetArmor.gameObject.name).gameObject.SetActive(false);
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
                SetMaterialBaseChar(UnityEngine.Random.Range(0, 4));
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

                leftHandWeapons[0].GetComponent<MeshRenderer>().material = randomMaterial;
                leftHandWeapons[1].GetComponent<MeshRenderer>().material = randomMaterial;
                leftHandWeapons[2].GetComponent<MeshRenderer>().material = randomMaterial;
                leftHandWeapons[3].GetComponent<MeshRenderer>().material = randomMaterial;
                leftHandWeapons[4].GetComponent<MeshRenderer>().material = randomMaterial;

                rightHandWeapons[0].GetComponent<MeshRenderer>().material = randomMaterial;
                rightHandWeapons[1].GetComponent<MeshRenderer>().material = randomMaterial;
                rightHandWeapons[2].GetComponent<MeshRenderer>().material = randomMaterial;
                rightHandWeapons[3].GetComponent<MeshRenderer>().material = randomMaterial;
                rightHandWeapons[4].GetComponent<MeshRenderer>().material = randomMaterial;

                accessoires[0].GetComponent<MeshRenderer>().material = randomMaterial;
                accessoires[1].GetComponent<MeshRenderer>().material = randomMaterial;
                accessoires[2].GetComponent<MeshRenderer>().material = randomMaterial;
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
                modularHair = hairBaseCharModels[UnityEngine.Random.Range(0, hairBaseCharModels.Length)];

                if (!onlyBaseChar)
                {
                    modularHelmetArmor = helmetArmorModels[UnityEngine.Random.Range(0, helmetArmorModels.Length)];
                    modularShouldersArmor = shoulderArmorModels[UnityEngine.Random.Range(0, shoulderArmorModels.Length)];
                    modularTorsoArmor = torsoArmorModels[UnityEngine.Random.Range(0, torsoArmorModels.Length)];
                    modularArmsArmor = armsArmorModels[UnityEngine.Random.Range(0, armsArmorModels.Length)];
                    modularHandsArmor = handsArmorModels[UnityEngine.Random.Range(0, handsArmorModels.Length)];
                    modularLegsArmor = legsArmorModels[UnityEngine.Random.Range(0, legsArmorModels.Length)];
                    modularFeetArmor = feetArmorModels[UnityEngine.Random.Range(0, feetArmorModels.Length)];
                }

                if (!showHelmet) character.transform.Find(modularHair.gameObject.name).gameObject.SetActive(true);

                character.transform.Find(modularEyebrows.gameObject.name).gameObject.SetActive(true);
                if (showHelmet) character.transform.Find(modularHelmetArmor.gameObject.name).gameObject.SetActive(true);
                if (Array.IndexOf(helmetArmorsBaseCharRequired, modularHelmetArmor.gameObject.name) >= 0 || !showHelmet)
                {
                    character.transform.Find(modularHead.gameObject.name).gameObject.SetActive(true);
                    character.transform.Find(modularEyebrows.gameObject.name).gameObject.SetActive(true);
                    if (baseCharType == 0 || baseCharType == 2) character.transform.Find(modularBeard.gameObject.name).gameObject.SetActive(true);
                }
                character.transform.Find(modularShouldersArmor.gameObject.name).gameObject.SetActive(true);
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
                    if (modularPart.gameObject.name.EndsWith("Helmet") || modularPart.gameObject.name.EndsWith("VisorUp") || modularPart.gameObject.name.EndsWith("Visor") || modularPart.gameObject.name.EndsWith("VisorOld"))
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

                weaponType = UnityEngine.Random.Range(0, 15);

                // 0 = Gladius
                // 1 = Hammer Paladin and Shield Paladin
                // 2 = Long Sword
                // 3 = Orc Axe
                // 4 = Gladius and Shield Bear
                // 5 = Orc Axe and Shield Bear
                // 6 = Hammer Paladin and Hammer Paladin
                // 7 = Gladius and Gladius
                // 8 = Gladius
                // 9 = Orc Axe
                // 10 = Gladius and Gladius
                // 11 = Hammer Paladin
                // 12 = Gladius and Sickle
                // 13 = Halberd
                // 14 = Halberd and Shield Paladin

                switch (weaponType)
                {
                    case 0:
                        rightHandWeapons[0].gameObject.SetActive(true);
                        break;
                    case 1:
                        leftHandWeapons[2].gameObject.SetActive(true);
                        rightHandWeapons[1].gameObject.SetActive(true);
                        break;
                    case 2:
                        rightHandWeapons[2].gameObject.SetActive(true);
                        break;
                    case 3:
                        rightHandWeapons[3].gameObject.SetActive(true);
                        break;
                    case 4:
                        leftHandWeapons[3].gameObject.SetActive(true);
                        rightHandWeapons[0].gameObject.SetActive(true);
                        break;
                    case 5:
                        leftHandWeapons[3].gameObject.SetActive(true);
                        rightHandWeapons[3].gameObject.SetActive(true);
                        break;
                    case 6:
                        leftHandWeapons[1].gameObject.SetActive(true);
                        rightHandWeapons[1].gameObject.SetActive(true);
                        break;
                    case 7:
                        leftHandWeapons[0].gameObject.SetActive(true);
                        rightHandWeapons[0].gameObject.SetActive(true);
                        break;
                    case 8:
                        rightHandWeapons[0].gameObject.SetActive(true);
                        break;
                    case 9:
                        rightHandWeapons[3].gameObject.SetActive(true);
                        break;
                    case 10:
                        leftHandWeapons[0].gameObject.SetActive(true);
                        rightHandWeapons[0].gameObject.SetActive(true);
                        break;
                    case 11:
                        rightHandWeapons[1].gameObject.SetActive(true);
                        break;
                    case 12:
                        leftHandWeapons[4].gameObject.SetActive(true);
                        rightHandWeapons[0].gameObject.SetActive(true);
                        break;
                    case 13:
                        rightHandWeapons[4].gameObject.SetActive(true);
                        break;
                    case 14:
                        leftHandWeapons[2].gameObject.SetActive(true);
                        rightHandWeapons[4].gameObject.SetActive(true);
                        break;
                }
            }

            public void ResetWeapons()
            {
                leftHandWeapons[0].gameObject.SetActive(false);
                leftHandWeapons[1].gameObject.SetActive(false);
                leftHandWeapons[2].gameObject.SetActive(false);
                leftHandWeapons[3].gameObject.SetActive(false);
                leftHandWeapons[4].gameObject.SetActive(false);

                rightHandWeapons[0].gameObject.SetActive(false);
                rightHandWeapons[1].gameObject.SetActive(false);
                rightHandWeapons[2].gameObject.SetActive(false);
                rightHandWeapons[3].gameObject.SetActive(false);
                rightHandWeapons[4].gameObject.SetActive(false);
            }

            public void RandomizeAccessoires()
            {
                ResetAccessoires();

                accessoireType = UnityEngine.Random.Range(0, 5);

                // 0 = Empty
                // 1 = Empty
                // 2 = Small Bag
                // 3 = Scabbard
                // 4 = Empty Scabbard

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
        }
    }
}
