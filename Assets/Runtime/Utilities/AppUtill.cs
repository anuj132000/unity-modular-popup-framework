using UnityEngine;

public static class AppUtill {

    public static bool IsEditor = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;    

    public static bool IsScreenPortrait {
        get { return Screen.orientation == ScreenOrientation.Portrait; }
    }
}
