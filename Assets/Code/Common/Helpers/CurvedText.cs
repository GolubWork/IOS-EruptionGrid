using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TMP_Text))]
public class CurvedText : MonoBehaviour
{
    public AnimationCurve curve = new AnimationCurve(
        new Keyframe(0, 0),
        new Keyframe(0.5f, 0.2f),
        new Keyframe(1, 0)
    );

    public float curveScale = 10f;

    private TMP_Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            // Получаем позицию центра символа
            Vector3 charMidBaseline = (vertices[vertexIndex + 0] + vertices[vertexIndex + 2]) / 2;
            float x0 = (charMidBaseline.x - textComponent.rectTransform.rect.xMin) / textComponent.rectTransform.rect.width;

            // Смещение по кривой
            float curveY = curve.Evaluate(x0) * curveScale;

            // Применяем смещение
            Vector3 offset = new Vector3(0, curveY, 0);
            for (int j = 0; j < 4; j++)
                vertices[vertexIndex + j] += offset;
        }

        // Применяем обновлённые вершины
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}