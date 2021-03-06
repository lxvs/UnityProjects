﻿using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Equipment Manager found!");
            return;
        }
        instance = this;
    }

    #endregion

    Inventory inventory;
    public Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;
    public SkinnedMeshRenderer targetMesh;
    public delegate void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment);
    public OnEquipmentChanged onEquipmentChangedCallBack;

    public SkinnedMeshRenderer[] defualtOutfitsPrefabs;
    SkinnedMeshRenderer[] defaultOutfitsInstances;

    public Material[] materials;

    private void Start()
    {
        inventory = Inventory.instance;

        int equipmentSlotNum = System.Enum.GetNames(typeof(EquipmentTypeSlot)).Length;
        currentEquipment = new Equipment[equipmentSlotNum];
        currentMeshes = new SkinnedMeshRenderer[equipmentSlotNum];

        defaultOutfitsInstances = new SkinnedMeshRenderer[defualtOutfitsPrefabs.Length];
        for (int i = 0; i < defualtOutfitsPrefabs.Length; i++)
        {
            if (defualtOutfitsPrefabs[i] != null)
            {
                defaultOutfitsInstances[i] = Instantiate(defualtOutfitsPrefabs[i]);
                defaultOutfitsInstances[i].transform.parent = targetMesh.transform;
                defaultOutfitsInstances[i].bones = targetMesh.bones;
                defaultOutfitsInstances[i].rootBone = targetMesh.rootBone;

            }
        }


    }

    public void Equip(Equipment newEquipment)
    {
        int equipmentSlot = (int)newEquipment.EquipmentTypeToSlot();
        Equipment oldEquipment = currentEquipment[equipmentSlot];
        if (oldEquipment != null && currentMeshes[equipmentSlot] != null)
        {
            Destroy(currentMeshes[equipmentSlot].gameObject);
        }
        currentEquipment[equipmentSlot] = newEquipment;
        if (newEquipment.mesh != null)
        {
            if (equipmentSlot < defaultOutfitsInstances.Length && defaultOutfitsInstances[equipmentSlot] != null)
            {
                Destroy(defaultOutfitsInstances[equipmentSlot].gameObject);
            }
            SkinnedMeshRenderer newMesh = Instantiate(newEquipment.mesh);
            newMesh.transform.parent = targetMesh.transform;

            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;

            if((int)newEquipment.equipmentType <= 2)
            {
                foreach (EquipmentMeshRegions equipmentBlendShapes in newEquipment.equipmentMeshRegions)
                {
                    targetMesh.SetBlendShapeWeight((int)equipmentBlendShapes, 100);
                }

                EquipmentArmor newEquipmentArmor = (EquipmentArmor)newEquipment;
                int matLength = newMesh.materials.Length;
                Material[] newMaterials = new Material[matLength];
                for (int i = 0; i < matLength; i++)
                {
                    newMaterials[i] = materials[(int)newEquipmentArmor.armorType];
                }
                newMesh.materials = newMaterials;


            }
            currentMeshes[equipmentSlot] = newMesh;

        }
        

        inventory.Remove(newEquipment);
        inventory.Add(oldEquipment);

        if (onEquipmentChangedCallBack != null) onEquipmentChangedCallBack.Invoke(newEquipment, oldEquipment);


    }

    public void Unequip(Equipment equipment)
    {
        if (equipment != null)
        {
            if(inventory.items.Count >= inventory.inventorySpace)
            {
                Debug.LogWarning("Inventory is full!");
                return;
            }
            int equipmentSlot = (int)equipment.equipmentSlot;
            currentEquipment[equipmentSlot] = null;
            if (currentMeshes[equipmentSlot] != null)
            {
                Destroy(currentMeshes[equipmentSlot].gameObject);
            }
            if (defualtOutfitsPrefabs[equipmentSlot] != null)
            {
                defaultOutfitsInstances[equipmentSlot] = Instantiate(defualtOutfitsPrefabs[equipmentSlot]);
                defaultOutfitsInstances[equipmentSlot].transform.parent = targetMesh.transform;
                defaultOutfitsInstances[equipmentSlot].bones = targetMesh.bones;
                defaultOutfitsInstances[equipmentSlot].rootBone = targetMesh.rootBone;
            }

            if (onEquipmentChangedCallBack != null) onEquipmentChangedCallBack.Invoke(null, equipment);
            inventory.Add(equipment);
        }
    }
}
