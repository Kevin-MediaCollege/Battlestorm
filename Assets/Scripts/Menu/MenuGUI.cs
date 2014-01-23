using UnityEngine;
using System.Collections;

public class MenuGUI:MonoBehaviour {
	public GUIStyle buttonStyle;
	public float minorOffset;
	private bool offsetbool;

	private string lastGUITooltip;
	private string tooltip;

	public AudioClip buttonHover;
	public AudioClip buttonClick;
	public Camera optionCamera;
	public Camera creditsCamera;
	public FadeScript fade;
	public CameraMenu flybyCamera;
	private bool openMainMenu;
	private bool openOptions;
	private bool openCredits;

	public Texture optionBackground;
	public GUIStyle optionStyle;
	public GUIStyle optionindexStyle;
	public GUIStyle optionToggle;
	public GUIStyle sliderStyle;
	public GUIStyle thumbStyle;

	public GUIStyle inputStyle;

	public int optionIndex;

	public Vector2[] resolutions;
	public Vector2 currentresolution;
	public int selectedresolutions;

	public Light ingameLight;

	private bool givingInput;
	private KeyCode selectedKeyCode;
	public int keyCodeNum;

	public AudioSource music;
	void Start () {
		Time.timeScale = 1.0f;
		selectedresolutions = 3;
		currentresolution = resolutions[selectedresolutions];
		StartCoroutine("giveOffset");
		openMainMenu = true;
	}

	void FixedUpdate() {
		if(music.volume != VolumeManager.MusicVolume){
			music.volume = VolumeManager.MusicVolume;
		}
		if(optionIndex == -1){
			optionIndex = 2;
		}
		else if(optionIndex == 4){
			optionIndex = 0;
		}
		if(openOptions && fade.alphaFadeValue == 1){
			optionIndex = 0;
			fade.fadingOut = false;
			this.camera.enabled = false;
			optionCamera.enabled = true;
			flybyCamera.stopmoving = true;
			openMainMenu = false;
		}
		if(openCredits && fade.alphaFadeValue == 1){
			flybyCamera.stopmoving = true;
			this.camera.enabled = false;
			creditsCamera.enabled = true;
			fade.fadingOut = false;
			openMainMenu = false;
		}
		if(!openOptions && !openCredits && fade.alphaFadeValue == 1){
			fade.fadingOut = false;
			this.camera.enabled = true;
			optionCamera.enabled = false;
			flybyCamera.stopmoving = false;
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
		flybyCamera.stopmoving = false;
		this.camera.enabled = true;
	}
	IEnumerator giveOffset(){
		while (true) {
			yield return new WaitForSeconds (0.1f);

			if(offsetbool) {
				minorOffset += 0.5f;
			} else {
				minorOffset -= 0.5f;
			}

			if (minorOffset == 5)
				offsetbool = false;

			if (minorOffset == -5)
				offsetbool = true;
		}
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
			if(GUI.Button(new Rect(5, 435, 190 + (minorOffset / 2), 90 + minorOffset), new GUIContent("Play", "Play"), buttonStyle)) {
				playSound(1);
				LoadingScreen.Instance.loadLoadingScreen("GameMap");
			}

			if(GUI.Button(new Rect(20, 535, 300 + (minorOffset / 2), 90 + minorOffset), new GUIContent("Options", "Options"), buttonStyle)){
				playSound(1);
				fade.fadingOut = true;
				openOptions = true;
			}

			if(GUI.Button(new Rect(15, 635, 300 + (minorOffset / 2), 90 + minorOffset), new GUIContent("Credits", "Credits"), buttonStyle)){
				playSound(1);
				fade.fadingOut = true;
				openCredits = true;
			}

			tooltip = GUI.tooltip;

			if(!Application.isWebPlayer) {
				if(GUI.Button(new Rect(1050, 635, 190 + (minorOffset / 2), 90 + minorOffset), new GUIContent("Quit", "Quit"), buttonStyle)){
					Application.Quit();
				}
			}
		}
		if(!openMainMenu && !openOptions){
			if(GUI.Button(new Rect(550,600,200,100),"Back",optionindexStyle)){
				playSound(1);
				fade.fadingOut = true;
				StartCoroutine("delay");
			}
		}
		if(!openMainMenu && !openCredits){
			GUI.DrawTexture(new Rect(400,0,500,900),optionBackground);
			if(GUI.Button(new Rect(-50, 635, 300 + (minorOffset / 2), 90 + minorOffset), new GUIContent("Menu", "Menu"), buttonStyle)){
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
			}
			if(optionIndex == 1){
				//Audio 1
				DrawInputWindow();
			}
			if(optionIndex == 2){
				//Graphics 2
				DrawGraphicsWindow();
			}
			if(optionIndex == 3){
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
		}
	}
	void DrawGraphicsWindow(){ 
		GUI.Label(new Rect(600,25,100,50),"Graphics",optionStyle);
		GUI.Label(new Rect(530,80,100,50),"Fullscreen",optionStyle);
		Screen.fullScreen = GUI.Toggle(new Rect(710,95,75,25),Screen.fullScreen,"",optionToggle);
		
		GUI.Label(new Rect(600,140,100,50),"Resolution",optionStyle);
		GUI.Label(new Rect(600,175,100,50),"" + currentresolution.x +":" + currentresolution.y,optionStyle);
		if(GUI.Button(new Rect(700,230,70,50),"+",optionindexStyle)){
			if(selectedresolutions == 3){}else{
				selectedresolutions += 1;
			}
			currentresolution = resolutions[selectedresolutions];
		}
		if(GUI.Button(new Rect(520,230,70,50),"-",optionindexStyle)){
			if(selectedresolutions == 0){}else{
				selectedresolutions -= 1;
			}
			currentresolution = resolutions[selectedresolutions];
		}
		optionindexStyle.fontSize = 20;
		if(GUI.Button(new Rect(610,230,70,50),"Apply",optionindexStyle)){
			Screen.SetResolution(Mathf.FloorToInt(currentresolution.x),Mathf.FloorToInt(currentresolution.y),Screen.fullScreen);
		}
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
			GUI.Label(new Rect(10,-5,100,50),"Vsync",optionStyle);
			GUI.Label(new Rect(10,10,100,50),"Count",optionStyle);
			QualitySettings.vSyncCount = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(5, 65, 90, 15), QualitySettings.vSyncCount, 0.0F, 2.0F,sliderStyle,thumbStyle));
		GUI.EndGroup();

		//Texture Quality
		GUI.BeginGroup(new Rect(470,520,120,100),"",optionStyle);
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
			GUI.Label(new Rect(10,-5,100,50),"Shadows",optionStyle);
			optionindexStyle.fontSize = 20;
		if(GUI.Button(new Rect(60,45,50,50),"On",optionindexStyle)){
				ingameLight.shadows = LightShadows.Hard;
			}
		if(GUI.Button(new Rect(5,45,50,50),"Off",optionindexStyle)){
				ingameLight.shadows = LightShadows.None;
			}
			optionindexStyle.fontSize = 40;
		GUI.EndGroup();

		GUI.BeginGroup(new Rect(700,520,120,100),"",optionStyle);
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

	}
}
