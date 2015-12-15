using UnityEngine;
using UnityEditor;
using System.Collections;

// This asset preprocessor will automatically set any and all Textures (or just textures keyed with a "Sprite_" or "_Sprite" filename) to truecolor, avoiding automatic compression.
// This will also automatically set all Textures to point filtering.
// This script uses EditorPrefs. It will write to your drive under: 
//		On Mac OS X, EditorPrefs are stored in ~/Library/Preferences/com.unity3d.UnityEditor.plist.
//		On Windows, EditorPrefs are stored in the registry under the HKCU\Software\Unity Technologies\UnityEditor key

public class SpriteToolkitPreProcessor : AssetPostprocessor {
	void OnPostprocessTexture(Texture2D texture)
	{
		// Note: EditorPrefs.GetBool default return is 0 if the key doesn't exist. This will exit the PreProcessor if it has never been enabled.
		if (!EditorPrefs.GetBool ("STK_EnableSpritePreProcessor")) {
			// Not enabled. Quit.
			return;
		}

		if (EditorPrefs.GetBool ("STK_RestrictToSprites")) {
			string assetFilename = assetPath.ToLower ();
			if (assetFilename.Contains ("_sprite") || assetFilename.Contains ("sprite_")) {
				goto work;
			} else {
				return;
			}
		}

	work:
		MonoBehaviour.print ("STK is going to work");
		TextureImporter importer = (TextureImporter)assetImporter;
		if (EditorPrefs.GetBool ("STK_PreventAutoCompressSpritesInEditor")) {
			importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
		}

		if (EditorPrefs.GetBool ("STK_PreventAutoFilterSpritesInEditor")) {
			importer.filterMode = FilterMode.Point;
		}

		if (EditorSettings.defaultBehaviorMode == EditorBehaviorMode.Mode3D && EditorPrefs.GetBool ("STK_AssumeEverythingIsASprite")) {
			importer.textureType = TextureImporterType.Sprite;
		}
	}
}
