using UnityEngine;
using UnityEditor;
using System.Collections;
using Rotorz.ReorderableList;

[CustomEditor(typeof(ProductDatabase))]
public class LevelsDatabaseEditor : Editor
{
    private SerializedProperty _list;
    private ProductDatabase _target;
    private void OnEnable()
    {
        _list = serializedObject.FindProperty("items");
        _target = target as ProductDatabase;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ReorderableListGUI.Title("Shop Items");
        ReorderableListGUI.ListField(_list);
        
        serializedObject.ApplyModifiedProperties();
    }

}


[CustomPropertyDrawer(typeof(ProductItem))]
public class ProductItemDrawer : PropertyDrawer
{
    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {

        float offset = 10;       

        float currX = pos.x;
        float width = 40;

        Rect pos0 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = 60;
        Rect pos1 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = 60;
        Rect pos2 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = 120;
        Rect pos3 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX = pos.x;
        offset += EditorGUIUtility.singleLineHeight + 10;

        width = 60;
        Rect pos4 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = pos.width - width;
        Rect pos5 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX = pos.x;
        offset += EditorGUIUtility.singleLineHeight + 10;
        width = 80;
        Rect pos6 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = pos.width - width;
        Rect pos7 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX = pos.x;
        offset += EditorGUIUtility.singleLineHeight + 10;
        width = 80;
        Rect pos8 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = pos.width - width;
        Rect pos9 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX = pos.x;
        offset += EditorGUIUtility.singleLineHeight + 10;
        width = 80;
        Rect pos10 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = pos.width - width;
        Rect pos11 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX = pos.x;
        offset += EditorGUIUtility.singleLineHeight + 10;
        width = 80;
        Rect pos12 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = pos.width - width;
        Rect pos13 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        if (prop.FindPropertyRelative("isRealPrice").boolValue)
        {
            currX = pos.x;
            offset += EditorGUIUtility.singleLineHeight + 10;
            width = 120;
            Rect pos14 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

            currX += width;
            width = pos.width - width;
            Rect pos15 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

            currX = pos.x;
            offset += EditorGUIUtility.singleLineHeight + 10;
            width = 100;
            Rect pos16 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

            currX += width;
            width = pos.width - width;
            Rect pos17 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(pos14, new GUIContent("Pack Name (sku):"));
            EditorGUI.PropertyField(pos15, prop.FindPropertyRelative("packName"), GUIContent.none);
            EditorGUI.LabelField(pos16, new GUIContent("Amount:"));
            EditorGUI.PropertyField(pos17, prop.FindPropertyRelative("amount"), GUIContent.none);

        }
        currX = pos.x;
        offset += EditorGUIUtility.singleLineHeight + 10;
        width = 100;
        Rect pos18 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = pos.width- width;
        Rect pos19 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX = pos.x;
        offset += EditorGUIUtility.singleLineHeight + 10;
        width = 80;
        Rect pos20 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = pos.width - width;
        Rect pos21 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX = pos.x;
        offset += EditorGUIUtility.singleLineHeight + 10;
        width = 80;
        Rect pos22 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX += width;
        width = pos.width - width;
        Rect pos23 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX = pos.x;
        offset += EditorGUIUtility.singleLineHeight + 10;
        width = 120;
        Rect pos24 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        currX = pos.x;
        offset += EditorGUIUtility.singleLineHeight + 10;
        width = pos.width;
        Rect pos25 = new Rect(currX, pos.y + offset, width, EditorGUIUtility.singleLineHeight);

        EditorGUI.LabelField(pos0, new GUIContent("ID:"));
        EditorGUI.PropertyField(pos1, prop.FindPropertyRelative("itemID"), GUIContent.none);
        EditorGUI.LabelField(pos2, new GUIContent("Name ID:"));
        EditorGUI.PropertyField(pos3, prop.FindPropertyRelative("nameID"), GUIContent.none);
        EditorGUI.LabelField(pos4, new GUIContent("Name:"));
        EditorGUI.PropertyField(pos5, prop.FindPropertyRelative("name"), GUIContent.none);
        EditorGUI.LabelField(pos6, new GUIContent("Description:"));
        EditorGUI.PropertyField(pos7, prop.FindPropertyRelative("description"), GUIContent.none);
        EditorGUI.LabelField(pos8, new GUIContent("Price:"));
        EditorGUI.PropertyField(pos9, prop.FindPropertyRelative("price"), GUIContent.none);
        EditorGUI.LabelField(pos10, new GUIContent("Real Price:"));
        EditorGUI.PropertyField(pos11, prop.FindPropertyRelative("realPrice"), GUIContent.none);
        EditorGUI.LabelField(pos12, new GUIContent("Is Real Price:"));
        EditorGUI.PropertyField(pos13, prop.FindPropertyRelative("isRealPrice"), GUIContent.none);
        
        EditorGUI.LabelField(pos18, new GUIContent("Unlock Height:"));
        EditorGUI.PropertyField(pos19, prop.FindPropertyRelative("heightNeeded"), GUIContent.none);
        
        EditorGUI.LabelField(pos20, new GUIContent("Is Equipable:"));
        EditorGUI.PropertyField(pos21, prop.FindPropertyRelative("isEquippable"), GUIContent.none);
        EditorGUI.LabelField(pos22, new GUIContent("Sprite:"));
        EditorGUI.PropertyField(pos23, prop.FindPropertyRelative("sprite"), GUIContent.none);

        EditorGUI.LabelField(pos24, new GUIContent("Unequips Items:"));
        //EditorGUI.PropertyField(pos25, prop.FindPropertyRelative("unequips"), GUIContent.none);

        pos25.height = ReorderableListGUI.CalculateListFieldHeight(prop.FindPropertyRelative("unequips"));

        ReorderableListGUI.ListFieldAbsolute(pos25, prop.FindPropertyRelative("unequips"));

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lineMulti = 18;
        if (property.FindPropertyRelative("isRealPrice").boolValue)
        {
            lineMulti = 20;
        }

            float height = ReorderableListGUI.CalculateListFieldHeight(property.FindPropertyRelative("unequips"));
        return height + EditorGUIUtility.singleLineHeight * lineMulti;
    }

}