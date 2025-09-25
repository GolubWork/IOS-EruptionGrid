#if UNITY_EDITOR
using _Scripts.GridSpawn;
using UnityEditor;
using UnityEngine;

namespace Code.Meta.Levels.Configs
{
    [CustomEditor(typeof(LevelConfigs))]
    public class LevelConfigEditor : Editor
    {
        private LevelConfigs targetScript;
        private SerializedProperty levelsProp;

        private const int DefaultGridSize = 5;

        private void OnEnable()
        {
            targetScript = (LevelConfigs)target;
            levelsProp = serializedObject.FindProperty("Levels");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.LabelField("Level Config Editor", EditorStyles.boldLabel);

            for (int i = 0; i < levelsProp.arraySize; i++)
            {
                SerializedProperty levelProperty = levelsProp.GetArrayElementAtIndex(i);
                SerializedProperty levelIdProp = levelProperty.FindPropertyRelative("levelId");
                SerializedProperty levelStatusIdProp = levelProperty.FindPropertyRelative("levelStatusId");
                SerializedProperty levelSecondsProp = levelProperty.FindPropertyRelative("levelSeconds");
                SerializedProperty gameResource = levelProperty.FindPropertyRelative("gameResource");
                SerializedProperty gridProp = levelProperty.FindPropertyRelative("grid");

                SerializedProperty xProp = gridProp.FindPropertyRelative("X");
                SerializedProperty yProp = gridProp.FindPropertyRelative("Y");

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField($"Level {i + 1}", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(levelIdProp, new GUIContent("Level ID"));
                EditorGUILayout.PropertyField(levelStatusIdProp, new GUIContent("Level Status"));
                levelSecondsProp.floatValue = EditorGUILayout.FloatField("Level Seconds", levelSecondsProp.floatValue);
                gameResource.intValue = EditorGUILayout.IntField("Game Resources", gameResource.intValue);

                int newX = EditorGUILayout.IntField("Grid Width (X)", xProp.intValue);
                int newY = EditorGUILayout.IntField("Grid Height (Y)", yProp.intValue);

                if (newX != xProp.intValue || newY != yProp.intValue)
                {
                    xProp.intValue = Mathf.Max(1, newX); 
                    yProp.intValue = Mathf.Max(1, newY);
                    InitializeGrid(gridProp, xProp.intValue, yProp.intValue);
                }

                RenderGrid(gridProp, xProp.intValue, yProp.intValue);

                if (GUILayout.Button($"Remove Level {i + 1}"))
                {
                    levelsProp.DeleteArrayElementAtIndex(i);
                    break;
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(10);
            }

            if (GUILayout.Button("Add Level"))
            {
                AddNewLevel();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void RenderGrid(SerializedProperty gridProp, int width, int height)
        {
            SerializedProperty columnsProp = gridProp.FindPropertyRelative("columns");

            if (columnsProp == null || columnsProp.arraySize == 0)
            {
                EditorGUILayout.HelpBox("Grid is not initialized or empty!", MessageType.Warning);
                return;
            }

            EditorGUILayout.LabelField("Grid Preview (Editable)", EditorStyles.boldLabel);

            GUILayout.Space(10);
            for (int y = 0; y < height; y++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < width; x++)
                {
                    SerializedProperty cellProp = columnsProp.GetArrayElementAtIndex(x).FindPropertyRelative("rows").GetArrayElementAtIndex(y);
                    cellProp.boolValue = EditorGUILayout.Toggle(cellProp.boolValue, GUILayout.Width(20));
                }
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(10);
        }

        private void InitializeGrid(SerializedProperty gridProp, int width, int height)
        {
            SerializedProperty columnsProp = gridProp.FindPropertyRelative("columns");

            columnsProp.arraySize = width;
            for (int x = 0; x < width; x++)
            {
                SerializedProperty rowsProp = columnsProp.GetArrayElementAtIndex(x).FindPropertyRelative("rows");
                rowsProp.arraySize = height;
            }
        }

        private void AddNewLevel()
        {
            int newIndex = levelsProp.arraySize;
            levelsProp.InsertArrayElementAtIndex(newIndex);

            SerializedProperty newLevelProp = levelsProp.GetArrayElementAtIndex(newIndex);
            SerializedProperty newGridProp = newLevelProp.FindPropertyRelative("grid");

            InitializeGrid(newGridProp, DefaultGridSize, DefaultGridSize);

            SerializedProperty levelIdProp = newLevelProp.FindPropertyRelative("levelId");
            SerializedProperty levelSecondsProp = newLevelProp.FindPropertyRelative("levelSeconds");
            SerializedProperty gameResource = newLevelProp.FindPropertyRelative("gameResource");
            SerializedProperty xProp = newGridProp.FindPropertyRelative("X");
            SerializedProperty yProp = newGridProp.FindPropertyRelative("Y");

            levelIdProp.enumValueIndex = 0;
            levelSecondsProp.floatValue = 120f;
            gameResource.intValue = 10;
            xProp.intValue = DefaultGridSize;
            yProp.intValue = DefaultGridSize;
        }
    }
}
#endif