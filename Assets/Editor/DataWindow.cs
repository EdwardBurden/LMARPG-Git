using UnityEditor;

public abstract class DataWindow : EditorWindow
{
    protected static SD_Base DataModel;
    protected static SerialisedGameObject Target;

    protected void Add()
    {
        bool isfilled = DataModel.ValidateModel();
        if (isfilled)
        {
            DataHandler.Export(DataModel);
        }
    }

    protected virtual void DrawWindow() {
        if (DataModel != null)
            DataModel.CreateData();
    }

    private void OnGUI()
    {
        DrawWindow();
    }

    protected void Assign()
    {
        Target.DataReferanceID = DataModel.ID;
    }
}
