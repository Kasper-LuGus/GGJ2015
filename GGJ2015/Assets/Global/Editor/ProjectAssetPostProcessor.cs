using UnityEngine;
using UnityEditor;
using System.Collections;
 
class ProjectAssetPostprocessor : AssetPostprocessor
{
	// Use this to restrict this script to certain directories.
	// Assets placed outside of these directories can then remain unaffected by this script and retain the settings set by the author (e.g. for asset packs or standard assets).
	
	protected static string[] affectedFolder = new string[]{};

	public static bool IsAffectedFolder(string path)
	{
		foreach (string s in affectedFolder)
		{
			if (path.StartsWith(s))
				return true;
		}
		
		return false;
	}
	 
	protected void OnPreprocessModel ()
	{
		PreprocessModel();
	}

	protected void PreprocessModel ()
	{
		if (!IsAffectedFolder(assetPath))
			return;

		ModelImporter importer = (ModelImporter) assetImporter;

		importer.globalScale = 1.0f;
		importer.importMaterials = false;

		string pathWithoutExtension = FilePathWithoutExtension(assetPath);

		if (pathWithoutExtension.Contains("@"))
		{
			importer.importAnimation = true;

		}
		else
		{
			importer.importAnimation = false;
			importer.animationType = ModelImporterAnimationType.None;
		}

		Debug.Log("AssetPostprocessor: preprocessed " + assetPath +".");
	}
	
	protected string FilePathWithoutExtension(string path)
	{
		int fileExtPos = path.LastIndexOf(".");

		if (fileExtPos >= 0 )
			return path.Substring(0, fileExtPos);
		else
			return path;
	}
}