using UnityEngine;
using System.Collections;

public class MenuGUI:MonoBehaviour {
	public Texture2D buttonPlayNormal;
	public Texture2D buttonPlayOn;
	public Texture2D buttonOptionsNormal;
	public Texture2D buttonOptionsOn;
	public Texture2D buttonCreditsNormal;
	public Texture2D buttonCreditsOn;
	public Texture2D buttonQuitNormal;
	public Texture2D ButtonQuitOn;
	public Texture2D buttonBackNormal;
	public Texture2D buttonBackOn;
	public Texture2D buttonDownloadNormal;
	public Texture2D buttonDownloadOn;
	
	public Texture logo;
	public Texture jasonLogo;
	
	public string standaloneUrl;
	
	public GUIStyle buttonStyle;
	
	public AudioClip buttonHover;
	public AudioClip buttonClick;
	
	public Camera optionCamera;
	public Camera creditsCamera;
	
	public FadeScript fade;
	
	public CameraMenu flybyCamera;
	
	public Texture optionBackground;
	
	public GUIStyle optionStyle;
	public GUIStyle optionindexStyle;
	public GUIStyle optionToggle;
	public GUIStyle sliderStyle;
	public GUIStyle thumbStyle;
	public GUIStyle inputStyle;
	
	public Vector2[] resolutions;
	public Vector2 currentresolution;
	
	public Light ingameLight;
	
	public AudioSource music;
	
	public int optionIndex;
	public int keyCodeNum;
	public int selectedresolutions;
	
	private KeyCode selectedKeyCode;
	
	private bool offsetBool;
	private bool givingInput;
	private bool openMainMenu;
	private bool openOptions;
	private bool openCredits;
	
	private string lastGUITooltip;
	private string tooltip;
	
	void Start () {
		Time.timeScale = 1.0f;
		selectedresolutions = 3;
		currentresolution = resolutions[selectedresolutions];
		openMainMenu = true;
	}
	
	void FixedUpdate() {
		if(optionIndex == -1){
			optionIndex = 3;
		} else if(optionIndex == 4){
			optionIndex = 0;
		}
		if(openOptions && fade.alphaFadeValue == 1){
			optionIndex = 0;
			fade.fadingOut = false;
			this.camera.enabled = false;
			optionCamera.enabled = true;
			flybyCamera.stopMoving = true;
			openMainMenu = false;
		}
		if(openCredits && fade.alphaFadeValue == 1){
			flybyCamera.stopMoving = true;
			this.camera.enabled = false;
			creditsCamera.enabled = true;
			fade.fadingOut = false;
			openMainMenu = false;
		}
		if(!openOptions && !openCredits && fade.alphaFadeValue == 1){
			fade.fadingOut = false;
			this.camera.enabled = true;
			optionCamera.enabled = false;
			flybyCamera.stopMoving = false;
			openMainMenu = true;
		}
		if(tooltip != "" && tooltip != lastGUITooltip) {
			playSound (2);
			lastGUITooltip = tooltip;
		}
		
		if(tooltip == "") {
			lastGUITooltip = "NoTooltip";
		}
	}
	void enableMainCamera(){
		optionCamera.enabled = false;
		creditsCamera.enabled = false;
		flybyCamera.stopMoving = false;
		this.camera.enabled = true;
	}
	
	void playSound(int sound) {
		if (!audio.isPlaying) {
			switch (sound) {
			case 1:
				audio.PlayOneShot(buttonClick);
				break;
			case 2:
				audio.PlayOneShot(buttonHover);
				break;
			}
		}
	}
	IEnumerator delay(){
		yield return new WaitForSeconds(0.45f);
		openMainMenu = true;
		openCredits = false;
		openOptions = false;
		enableMainCamera();
	}
	void OnGUI() {
		GUI.depth = 100000;
		float rx = Screen.width / GameManager.nativeWidth;
		float ry = Screen.height / GameManager.nativeHeight;
		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 
		
		if(openMainMenu){
			GUI.DrawTexture(new Rect(500, 50, 296, 240), logo);
			GUI.DrawTexture(new Rect(930, 15, 334, 181), jasonLogo);

			buttonStyle.normal.background = buttonPlayNormal;
			buttonStyle.hover.background = buttonPlayOn;
			buttonStyle.active.background = buttonPlayOn;
			if(GUI.Button(new Rect(30, 650, 256, 49), new GUIContent("", "Play"), buttonStyle)) {
				playSound(1);
				LoadingScreen.Instance.loadLoadingScreen("GameMap");
			}
			
			buttonStyle.normal.background = buttonOptionsNormal;
			buttonStyle.hover.background = buttonOptionsOn;
			buttonStyle.active.background = buttonOptionsOn;
			if(GUI.Button(new Rect(515, 650, 256, 49), new GUIContent("", "Options"), buttonStyle)){
				playSound(1);
				fade.fadingOut = true;
				openOptions = true;
			}
			
			buttonStyle.normal.background = buttonCreditsNormal;
			buttonStyle.hover.background = buttonCreditsOn;
			buttonStyle.active.background = buttonCreditsOn;
			if(GUI.Button(new Rect(1005, 650, 256, 49), new GUIContent("", "Credits"), buttonStyle)){
				playSound(1);
				fade.fadingOut = true;
				openCredits = true;
			}
			
			tooltip = GUI.tooltip;
		}
		if(!openMainMenu && !openOptions){
			if(GUI.Button(new Rect(550,650,256,49),"Back",optionindexStyle)){
				playSound(1);
				fade.fadingOut = true;
				StartCoroutine("delay");
			}
		}
		if(!openMainMenu && !openCredits){
			GUI.DrawTexture(new Rect(400,0,500,900),optionBackground);
			buttonStyle.normal.background = buttonBackNormal;
			buttonStyle.hover.background = buttonBackOn;
			buttonStyle.active.background = buttonBackOn;
			if(GUI.Button(new Rect(30, 650, 256, 49), new GUIContent("", "Menu"), buttonStyle)){
				playSound(1);
				fade.fadingOut = true;
				StartCoroutine("delay");
			}
			
			if(GUI.Button(new Rect(400,0,50,720),"",optionindexStyle)){
				optionIndex -= 1;
				
			}
			if(GUI.Button(new Rect(850,0,50,720),"",optionindexStyle)){
				optionIndex += 1;
			}
			
			if(optionIndex == 0){
				//INPUT 0
				DrawMainOptionsWindow();
			} else if(optionIndex == 1){
				//Audio 1
				DrawInputWindow();
			} else if(optionIndex == 2){
				//Graphics 2
				DrawGraphicsWindow();
			} else if(optionIndex == 3){
				//SOUND 2
				DrawAudioWindow();
			}
		}
	}
	void DrawAudioWindow(){
		optionStyle.fontSize = 40;
		GUI.Label(new Rect(600,25,100,50),"Audio",optionStyle);
		optionStyle.fontSize = 20;
		GUI.Label(new Rect(600,150,100,50),"Music",optionStyle);
		VolumeManager.MusicVolume = GUI.HorizontalSlider(new Rect(550, 195, 200, 15), VolumeManager.MusicVolume, 0.0F, 1.0F,sliderStyle,thumbStyle);
		
		GUI.Label(new Rect(600,230,100,50),"Sound Effects",optionStyle);
		VolumeManager.SoundVolume = GUI.HorizontalSlider(new Rect(550, 275, 200, 15), VolumeManager.SoundVolume, 0.0F, 1.0F,sliderStyle,thumbStyle);
		
	}
	void DrawMainOptionsWindow(){
		optionStyle.fontSize = 40;
		GUI.Label(new Rect(600,25,100,50),"Options",optionStyle);
		optionStyle.fontSize = 20;
		optionStyle.fontSize = 29;
		GUI.Label(new Rect(600,150,100,50),"Click on the sides of the menu",optionStyle);
		GUI.Label(new Rect(600,200,100,50),"to go through the options",optionStyle);
		optionStyle.fontSize = 20;
	}
	void DrawInputWindow(){
		if(givingInput){
			Event e = Event.current;
			if (e.isKey){
				Debug.Log("Detected key code: " + e.keyCode);
				selectedKeyCode = e.keyCode;
				
				switch(keyCodeNum){
				case 0:
					InputHandler.left = selectedKeyCode;
					break;
				case 1:
					InputHandler.right = selectedKeyCode;
					break;
				case 2:
					InputHandler.forward = selectedKeyCode;
					break;
				case 3:
					InputHandler.back = selectedKeyCode;
					break;
				case 4:
					InputHandler.up = selectedKeyCode;
					break;
				case 5:
					InputHandler.down = selectedKeyCode;
					break;
				case 6:
					InputHandler.minimap = selectedKeyCode;
					break;
				case 7:
					InputHandler.buildingManager = selectedKeyCode;
					break;
				}
				givingInput = false;
			}
		}
		if(givingInput){
			inputStyle.fontSize = 65;
			GUI.Label(new Rect(465,220,100,50),"Press any key",inputStyle);
			inputStyle.fontSize = 20;
		}
		if(!givingInput){
			optionStyle.fontSize = 40;
			GUI.Label(new Rect(600,25,100,50),"Input",optionStyle);
			optionStyle.fontSize = 20;
			GUI.BeginGroup(new Rect(460,100,400,50),"",optionStyle);
			GUI.DrawTexture(new Rect(0,0,300,50),optionBackground);
			GUI.Label(new Rect(5,0,100,50),"Movement - Left  =",inputStyle);
			GUI.Label(new Rect(200,0,100,50),"" + InputHandler.left.ToString(),optionStyle);
			
			optionindexStyle.fontSize = 20;
			if(GUI.Button(new Rect(320,0,50,50),"Set",optionindexStyle)){
				givingInput = true;
				keyCodeNum = 0;
			}
			optionindexStyle.fontSize = 40;
			GUI.EndGroup();
			
			GUI.BeginGroup(new Rect(460,160,400,50),"",optionStyle);
			GUI.DrawTexture(new Rect(0,0,300,50),optionBackground);
			GUI.Label(new Rect(5,0,100,50),"Movement - Right  =",inputStyle);
			GUI.Label(new Rect(200,0,100,50),"" + InputHandler.right.ToString(),optionStyle);
			
			optionindexStyle.fontSize = 20;
			if(GUI.Button(new Rect(320,0,50,50),"Set",optionindexStyle)){
				givingInput = true;
				keyCodeNum = 1;
			}
			optionindexStyle.fontSize = 40;
			GUI.EndGroup();
			
			GUI.BeginGroup(new Rect(460,220,400,50),"",optionStyle);
			GUI.DrawTexture(new Rect(0,0,300,50),optionBackground);
			GUI.Label(new Rect(5,0,100,50),"Movement - Forward  =",inputStyle);
			GUI.Label(new Rect(200,0,100,50),"" + InputHandler.forward.ToString(),optionStyle);
			
			optionindexStyle.fontSize = 20;
			if(GUI.Button(new Rect(320,0,50,50),"Set",optionindexStyle)){
				givingInput = true;
				keyCodeNum = 2;
			}
			optionindexStyle.fontSize = 40;
			GUI.EndGroup();
			
			GUI.BeginGroup(new Rect(460,280,400,50),"",optionStyle);
			GUI.DrawTexture(new Rect(0,0,300,50),optionBackground);
			GUI.Label(new Rect(5,0,100,50),"Movement - Backwards  =",inputStyle);
			GUI.Label(new Rect(200,0,100,50),"" + InputHandler.back.ToString(),optionStyle);
			
			optionindexStyle.fontSize = 20;
			if(GUI.Button(new Rect(320,0,50,50),"Set",optionindexStyle)){
				givingInput = true;
				keyCodeNum = 3;
			}
			optionindexStyle.fontSize = 40;
			GUI.EndGroup();
			
			GUI.BeginGroup(new Rect(460,340,400,50),"",optionStyle);
			GUI.DrawTexture(new Rect(0,0,300,50),optionBackground);
			GUI.Label(new Rect(5,0,100,50),"Movement - Up  =",inputStyle);
			GUI.Label(new Rect(200,0,100,50),"" + InputHandler.up.ToString(),optionStyle);
			
			optionindexStyle.fontSize = 20;
			if(GUI.Button(new Rect(320,0,50,50),"Set",optionindexStyle)){
				givingInput = true;
				keyCodeNum = 4;
			}
			optionindexStyle.fontSize = 40;
			GUI.EndGroup();
			
			GUI.BeginGroup(new Rect(460,400,400,50),"",optionStyle);
			GUI.DrawTexture(new Rect(0,0,300,50),optionBackground);
			GUI.Label(new Rect(5,0,100,50),"Movement - Down  =",inputStyle);
			GUI.Label(new Rect(200,0,100,50),"" + InputHandler.down.ToString(),optionStyle);
			
			optionindexStyle.fontSize = 20;
			if(GUI.Button(new Rect(320,0,50,50),"Set",optionindexStyle)){
				givingInput = true;
				keyCodeNum = 5;
			}
			optionindexStyle.fontSize = 40;
			GUI.EndGroup();
			
			GUI.BeginGroup(new Rect(460,460,400,50),"",optionStyle);
			GUI.DrawTexture(new Rect(0,0,300,50),optionBackground);
			GUI.Label(new Rect(5,0,100,50),"Minimap Key  =",inputStyle);
			GUI.Label(new Rect(200,0,100,50),"" + InputHandler.minimap.ToString(),optionStyle);
			
			optionindexStyle.fontSize = 20;
			if(GUI.Button(new Rect(320,0,50,50),"Set",optionindexStyle)){
				givingInput = true;
				keyCodeNum = 6;
			}
			optionindexStyle.fontSize = 40;
			GUI.EndGroup();
			
			GUI.BeginGroup(new Rect(460,520,400,50),"",optionStyle);
			GUI.DrawTexture(new Rect(0,0,300,50),optionBackground);
			GUI.Label(new Rect(5,0,100,50),"BuildingManager =",inputStyle);
			GUI.Label(new Rect(200,0,100,50),"" + InputHandler.buildingManager.ToString(),optionStyle);
			
			optionindexStyle.fontSize = 20;
			if(GUI.Button(new Rect(320,0,50,50),"Set",optionindexStyle)){
				givingInput = true;
				keyCodeNum = 7;
			}
			optionindexStyle.fontSize = 40;
			GUI.EndGroup();
			
			GUI.Label(new Rect(485, 590, 50, 50), "Custom input is not implemented yet.", inputStyle);
		}
	}
	void DrawGraphicsWindow(){ 
		int oldSize = optionStyle.fontSize;

		optionStyle.fontSize = 40;
		GUI.Label(new Rect(600,25,100,50),"Graphics",optionStyle);

		optionStyle.fontSize = oldSize;
		optionindexStyle.fontSize = 40;
		
		//Anti Aliasing
		GUI.BeginGroup(new Rect(470,300,120,100),"",optionStyle);
		GUI.DrawTexture(new Rect(0,0,120,100),optionBackground);
		GUI.Label(new Rect(10,-5,100,50),"Anti-Aliasing",optionStyle);
		QualitySettings.antiAliasing = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(5, 65, 90, 15), QualitySettings.antiAliasing, 0.0F, 8.0F,sliderStyle,thumbStyle));
		GUI.EndGroup();
		
		//Texture Quality
		GUI.BeginGroup(new Rect(470,410,120,100),"",optionStyle);
		GUI.DrawTexture(new Rect(0,0,120,100),optionBackground);
		GUI.Label(new Rect(10,-5,100,50),"Texture",optionStyle);
		GUI.Label(new Rect(10,10,100,50),"Quality",optionStyle);
		QualitySettings.masterTextureLimit = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(5, 65, 90, 15), QualitySettings.masterTextureLimit, 3.0F, 0.0F,sliderStyle,thumbStyle));
		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect(700,300,120,100),"",optionStyle);
		GUI.DrawTexture(new Rect(0,0,120,100),optionBackground);
		GUI.Label(new Rect(10,-5,100,50),"Anisotropic",optionStyle);
		optionindexStyle.fontSize = 20;
		if(GUI.Button(new Rect(60,45,50,50),"On",optionindexStyle)){
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
		}
		if(GUI.Button(new Rect(5,45,50,50),"Off",optionindexStyle)){
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
		}
		optionindexStyle.fontSize = 40;
		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect(700,410,120,100),"",optionStyle);
		GUI.DrawTexture(new Rect(0,0,120,100),optionBackground);
		GUI.Label(new Rect(10,-5,100,50),"Fog",optionStyle);
		optionindexStyle.fontSize = 20;
		if(GUI.Button(new Rect(60,45,50,50),"On",optionindexStyle)){
			RenderSettings.fog = true;
		}
		if(GUI.Button(new Rect(5,45,50,50),"Off",optionindexStyle)){
			RenderSettings.fog = false;
		}
		optionindexStyle.fontSize = 40;
		GUI.EndGroup();
		
		GUI.DrawTexture(new Rect(500,550,280,110),optionBackground);
		GUI.Label(new Rect(590,555,100,50),"Field of View",optionStyle);
		GUI.Label(new Rect(590,575,100,50),"" + InputHandler.FOV,optionStyle);
		InputHandler.FOV = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(530, 620, 200, 15), InputHandler.FOV, 30, 140,sliderStyle,thumbStyle));
	}
}
