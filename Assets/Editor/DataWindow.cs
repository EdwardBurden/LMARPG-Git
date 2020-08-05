using UnityEditor;
using UnityEngine;

public abstract class DataWindow : EditorWindow
{
    private string ErrorString;
    protected static SD_Base DataModel;
    protected static SerialisedGameObject Target;

    protected void Add()
    {
        bool isfilled = DataModel.ValidateModel();
        if (isfilled)
        {
            DataHandler.Export(DataModel);
        }
        else
            ErrorString = "Fields Not Valid";
    }

    protected virtual void DrawWindow()
    {
        if (DataModel != null)
            DataModel.CreateData();
    }

    private void OnGUI()
    {
        DrawWindow();
        if (!string.IsNullOrWhiteSpace(ErrorString))
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.black;
            EditorGUILayout.LabelField("Error:" + ErrorString, style);
        }
    }

    protected void Assign()
    {
        Target.DataReferanceID = DataModel.ID;
    }
}
