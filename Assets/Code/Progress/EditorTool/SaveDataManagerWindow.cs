using System;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Code.Progress.EditorTool
{

    public class SaveDataManagerWindow : EditorWindow
    {
        [MenuItem("Tools/Save Data Manager")]
        public static void ShowWindow()
        {
            GetWindow<SaveDataManagerWindow>("Save Data Manager");
        }

        private void OnGUI()
        {
            GUILayout.Label("Управление сохранениями", EditorStyles.boldLabel);

            string savePath = Path.Combine(Application.persistentDataPath, "Saves", "Save.json");
            bool saveExists = File.Exists(savePath);

            EditorGUILayout.Space(10);
            
            using (new EditorGUI.DisabledGroupScope(!saveExists))
            {
                if (GUILayout.Button("Удалить сохранение"))
                {
                    if (EditorUtility.DisplayDialog("Подтверждение",
                            "Вы уверены, что хотите удалить файл сохранения?",
                            "Да", "Отмена"))
                    {
                        DeleteSaveFile(savePath);
                    }
                }
            }

            EditorGUILayout.Space(5);
            
            if (saveExists)
            {
                EditorGUILayout.HelpBox($"Файл сохранения существует:\n{savePath}", MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox("Файл сохранения не найден", MessageType.Warning);
            }

            if (GUILayout.Button("Открыть папку с сохранениями"))
            {
                OpenSaveFolder();
            }
        }

        private void DeleteSaveFile(string path)
        {
            try
            {
                File.Delete(path);
                Debug.Log($"Файл сохранения успешно удален: {path}");
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при удалении файла сохранения: {e.Message}");
                EditorUtility.DisplayDialog("Ошибка", 
                    $"Не удалось удалить файл сохранения:\n{e.Message}", 
                    "ОК");
            }
        }

        private void OpenSaveFolder()
        {
            string folderPath = Path.Combine(Application.persistentDataPath, "Saves");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            EditorUtility.RevealInFinder(folderPath);
        }
    }
}
#endif