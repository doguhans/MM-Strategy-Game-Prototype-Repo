#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;


public class ResourcesPrefabPathBuilder : IPreprocessBuildWithReport
{
    public int callbackOrder {get {return 0;}}//=> throw new System.NotImplementedException();

    public void OnPreprocessBuild(BuildReport report)
    {
        MasterManager.PopulateNetworkedPrefabs();
    }
}
#endif