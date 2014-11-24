using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Directories {

	public static string sUnityResources = Application.dataPath + "/Resources/";
	public static string sFolder = "Textures";
	public static string sAtlasFolder = "Textures/Atlases";
	public static string sAtlasSubFolder = "Atlas";
	public static string sFontFolder = "Fonts";
	public static string sFontSubFolder = "Font";
	private static string sDataPath = Application.persistentDataPath + "/Data";
	private static string sSavePath = sDataPath + "/Save";

	public static string GetFullTexturePath() {return sUnityResources + sFolder;}
	public static string GetFullAtlasPath() {return sUnityResources + sAtlasFolder;}
	
	public static string[] GetFolderNames(string sPath)
	{
		return Directory.GetDirectories(sPath);
	}

	public static string[] GetFileNames(string sPath)
	{
		return Directory.GetFiles(sPath);
	}

	public static string[] GetFileNames(string path, string searchPattern)
	{
		string[] m_arExt = searchPattern.Split(';');
		
		List<string> strFiles = new List<string>();
		foreach(string filter in m_arExt)
		{
			strFiles.AddRange(
				System.IO.Directory.GetFiles(path, filter));
		}
		return strFiles.ToArray();
	}

	public static string GetFileNameFromPath(string sFullPath, bool bWithExtension)
	{
		if(bWithExtension)
			return Path.GetFileName(sFullPath);
		else
			return Path.GetFileNameWithoutExtension(sFullPath);

	}

	public static string GetFolderName(string sFullPath)
	{
		return Path.GetDirectoryName(sFullPath);
	}

	public static string GetSavePath()
	{
		if(!Directory.Exists(sSavePath))
		{
			Directory.CreateDirectory(sSavePath);
		}
		return sSavePath;
	}
}
