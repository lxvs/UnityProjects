using UnityEngine;

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

    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;
    public SkinnedMeshRenderer targetMesh;
    public delegate void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment);
    public OnEquipmentChanged onEquipmentChanged;

    public SkinnedMeshRenderer[] defualtOutfitsPrefabs;
    SkinnedMeshRenderer[] defaultOutfitsInstances;

    public Material[] materials;

    private void Start()
    {
        int equipmentSlotNum = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipment = new Equipment[equipmentSlotNum];
        currentMeshes = new SkinnedMeshRenderer[equipmentSlotNum];

        defaultOutfitsInstances = new SkinnedMeshRenderer[defualtOutfitsPrefabs.Length];
        for (int i = 0; i < defualtOutfitsPrefabs .Length; i++)
        {
            defaultOutfitsInstances[i] = Instantiate(defualtOutfitsPrefabs[i]);
            defaultOutfitsInstances[i].transform.parent = targetMesh.transform;
            defaultOutfitsInstances[i].bones = targetMesh.bones;
            defaultOutfitsInstances[i].rootBone = targetMesh.rootBone;
        }


    }

    //private void Update()
    //{
    //    if (Input.GetButtonDown("Debug")) 
    //    {
    //        Debug.Log("<color=green>" 
    //            + "blendshapeWeight = "
    //            + targetMesh.GetBlendShapeWeight(0) + ", "
    //            + targetMesh.GetBlendShapeWeight(1) + ", "
    //            + targetMesh.GetBlendShapeWeight(2) + "</color>");
    //    }
    //}
    public void Equip(Equipment newEquipment)
    {
        int equipmentSlot = (int)newEquipment.equipmentType;
        Equipment oldEquipment = currentEquipment[equipmentSlot];
        if (oldEquipment != null)
        {
            Destroy(currentMeshes[equipmentSlot].gameObject);
        }
        currentEquipment[equipmentSlot] = newEquipment;
        if (newEquipment.mesh != null)
        {
            if (defaultOutfitsInstances[equipmentSlot] != null)
            {
                Destroy(defaultOutfitsInstances[equipmentSlot].gameObject);
            }
            SkinnedMeshRenderer newMesh = Instantiate(newEquipment.mesh);
            newMesh.transform.parent = targetMesh.transform;

            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;

            if((int)newEquipment.equipmentType <= 2)
            {
                //if (newEquipment.equipmentType == EquipmentType.Chest)
                //{
                //    targetMesh.SetBlendShapeWeight(1, 100f);
                //    targetMesh.SetBlendShapeWeight(2, 100f);
                //}
                //if (newEquipment.equipmentType == EquipmentType.Legs)
                //{
                //    targetMesh.SetBlendShapeWeight(0, 100f);
                //}

                ////Debug.Log("<color=blue>"
                ////    + "blendshapeWeight = "
                ////    + targetMesh.GetBlendShapeWeight(0) + ", "
                ////    + targetMesh.GetBlendShapeWeight(1) + ", "
                ////    + targetMesh.GetBlendShapeWeight(2) + "</color>");

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
        

        Inventory.instance.Remove(newEquipment);
        Inventory.instance.Add(oldEquipment);
        
        if (onEquipmentChanged != null) onEquipmentChanged.Invoke(newEquipment, oldEquipment);

    }
}
