#pragma strict

var drawCrosshair = true;
var width : float = 2;
var height : float = 2;

private var tex : Texture2D;
var crosshairColor = Color.white;   //The crosshair color

private var newHeight : float;
private var lineStyle : GUIStyle;

function Awake (){
    tex = Texture2D(1,1);
    SetColor(tex,crosshairColor); //Set color
    lineStyle = GUIStyle();
    lineStyle.normal.background = tex;
}

function OnGUI (){
    var centerPoint = Vector2(Screen.width/2,Screen.height/2);
    var screenRatio : float = Screen.height/100;

    newHeight = height * screenRatio;

    if(drawCrosshair){
        GUI.Box(Rect(centerPoint.x-(width/2), centerPoint.y - (newHeight * 0.5), width, newHeight),GUIContent.none,lineStyle);
        GUI.Box(Rect(centerPoint.x-((newHeight * 0.5)), (centerPoint.y -(width/2)), newHeight, width),GUIContent.none,lineStyle);
    }    
}

function SetColor(myTexture : Texture2D, myColor : Color)
{
    for (var y : int = 0; y < myTexture.height; ++y)
    {
        for (var x : int = 0; x < myTexture.width; ++x)
        {
            myTexture.SetPixel(x, y, myColor);
        }
     }
        myTexture.Apply();
}