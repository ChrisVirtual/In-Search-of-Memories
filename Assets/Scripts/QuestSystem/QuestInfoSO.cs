using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ScriptableObject containing information about a quest
[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScripatbleObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; } //Unique identifier for the quest

    [Header("General")]
    public string displayName; //Display name of the quest

    [Header("Requirements")]
    public int levelRequirement; //Required level to start the quest
    public QuestInfoSO[] questPrerequisites; //Array of prerequisite quests required to start this quest

    [Header("Steps")]
    public GameObject[] questStepPrefabs; //Array of prefabs representing the steps of the quest

    [Header("Rewards")]
    public int goldReward; //Amount of gold rewarded upon quest completion
    public int experienceReward; //Amount of experience rewarded upon quest completion

    //Ensure ID is always the name of the ScriptableObject asset
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name; //Set ID to the name of the ScriptableObject asset
        UnityEditor.EditorUtility.SetDirty(this); //Mark the asset as dirty to ensure changes are saved
        #endif
    }
}