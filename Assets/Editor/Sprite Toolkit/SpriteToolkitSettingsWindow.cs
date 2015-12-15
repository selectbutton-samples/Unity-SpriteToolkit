using UnityEngine;
using UnityEditor;
using System.Collections;

public class SpriteToolkitSettingsWindow : EditorWindow {

	static GUIContent tooltip1 = new GUIContent("Enable Sprite Toolkit","Enable the Sprite Toolkit");
	static GUIContent tooltip2 = new GUIContent("Import All as Sprite","Imports all texture assets as sprite-type textures.\n\nDisabled in Unity2D mode.");
	static GUIContent tooltip3 = new GUIContent("Disable Compression","Prevents Unity from enabling texture compression for any new assets.\n\nAll textures will be set to Truecolor format.");
	static GUIContent tooltip4 = new GUIContent("Disable Filtering","Prevents Unity from enabling texture filtering for any new assets.\n\nAll textures will be set to point (pixelated) filtering.");
	static GUIContent tooltip5 = new GUIContent("Restrict to Sprites","Restricts Sprite Toolkit to textures with a special identifier in the filename.\n\nSee the helpbox below for details.");


	void OnGUI()
	{
		bool enable, compression, filter, sprites, assumeSprite;
		enable 		= EditorPrefs.GetBool ("STK_EnableSpritePreProcessor");
		compression	= EditorPrefs.GetBool ("STK_PreventAutoCompressSpritesInEditor");
		filter		= EditorPrefs.GetBool ("STK_PreventAutoFilterSpritesInEditor");
		sprites		= EditorPrefs.GetBool ("STK_RestrictToSprites");
		assumeSprite= EditorPrefs.GetBool ("STK_AssumeEverythingIsASprite");
		
		EditorGUI.BeginChangeCheck ();
		enable 		= EditorGUILayout.BeginToggleGroup 	(tooltip1, enable);
		EditorGUI.BeginDisabledGroup (EditorSettings.defaultBehaviorMode == EditorBehaviorMode.Mode2D);
		assumeSprite= EditorGUILayout.Toggle 			(tooltip2, assumeSprite);
		EditorGUI.EndDisabledGroup();
		compression	= EditorGUILayout.Toggle 			(tooltip3, compression);
		filter		= EditorGUILayout.Toggle 			(tooltip4, filter);
		sprites		= EditorGUILayout.Toggle 			(tooltip5, sprites);
		EditorGUILayout.EndToggleGroup ();
		
		EditorGUILayout.HelpBox("You can update textures by right-clicking on a desired texture and selecting Reimport.\n\n" +
								"When using the \"Restrict to Sprites\" option, the Sprite Toolkit will only run on Texture assets with the following text somewhere in the filename:\n\n" +
		                        "_Sprite\n" +
		                        "Sprite_\n\n" +
		                        "Example: Sprite_Blob.png\n" +
		                        "Example: Blob_Sprite.png",MessageType.Info);
		
		if (EditorGUI.EndChangeCheck ()) {
			EditorPrefs.SetBool ("STK_EnableSpritePreProcessor", enable);
			EditorPrefs.SetBool ("STK_PreventAutoCompressSpritesInEditor", compression);
			EditorPrefs.SetBool ("STK_PreventAutoFilterSpritesInEditor", filter);
			EditorPrefs.SetBool ("STK_RestrictToSprites", sprites);
			EditorPrefs.SetBool ("STK_AssumeEverythingIsASprite", assumeSprite);
		}
	}
	
	[MenuItem("Editor/Sprite Toolkit Settings")]
	static void Init() {
		SpriteToolkitSettingsWindow window = EditorWindow.GetWindow<SpriteToolkitSettingsWindow>("STK Settings");
		window.Show();
	}
	
	//[MenuItem("Editor/Purge STK Settings")]
	static void Purge() {
		EditorPrefs.DeleteKey ("STK_EnableSpritePreProcessor");
		EditorPrefs.DeleteKey ("STK_PreventAutoCompressSpritesInEditor");
		EditorPrefs.DeleteKey ("STK_PreventAutoFilterSpritesInEditor");
		EditorPrefs.DeleteKey ("STK_RestrictToSprites");
	}
	
}
