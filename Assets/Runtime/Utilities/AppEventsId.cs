public static class AppEventsId {

    #region Button2D Event Strings
    public static string Button2DOnMouseDown = "Button2D.OnMouseDown";
    public static string Button2DInteraction = "Button2D.Interaction";
    public static string Button2DVisibility = "Button2D.Visibility";
    #endregion

    #region PopupsQueueManager Event Strings
    public static string PopupsQueueManagerPopupAdded = "PopupsQueueManager.PopupAdded";
    public static string PopupsQueueManagerGroupAdded = "PopupsQueueManager.GroupAdded";
    public static string PopupsQueueManagerPopupRemoved = "PopupsQueueManager.PopupRemoved";
    public static string PopupsQueueManagerGroupRemoved = "PopupsQueueManager.GroupRemoved";
    public static string PopupsQueueManagerQueueIsEmpty = "PopupsQueueManager.QueueIsEmpty";
    #endregion

    #region CanvasScalar2D Event Strings
    public static string CanvasScalar2DScaled = "CanvasScalar2D.OnScaled";
    #endregion

    #region AudioPlayer Strings
    public static string AudioPlayerToggleMusic = "AudioPlayer.ToggleMusic";
    public static string AudioPlayerToggleSound = "AudioPlayer.ToggleSound";
    public static string AudioPlayerToggleVibration = "AudioPlayer.ToggleVibration";
    public static string AudioPlayerIsReady = "AudioPlayer.IsReady";
    #endregion

    #region Egg Game Event Strings
    public static string EggOnGameStartEvent = "EggGame.StartTheGame";
    public static string EggOnGameOverEvent = "EggGame.EndTheGame";
    public static string EggOnGameResumeEvent = "EggGame.ResumeTheGame";
    public static string EggOnGamePauseEvent = "EggGame.PauseTheGame";
    public static string EggOnLevelUpEvent = "EggLevelSystem.OnLevelUp";
    public static string EggOnXpEvent = "EggXpUIHandler.UpdateXpUI";
    public static string EggOnLevelChangedEvent = "EggLevelSystem.OnLevelChanged";
    public static string EggUpdateHighScoreEvent = "HighScoreHandler.UpdateHighScore";
    public static string EggOnScoreUpEvent = "ScoreUpHandler.UpdateScoreUI";
    public static string EggOnScoreDownStartEvent = "ScoreDownHandler.UpdateScoreUI";    
    public static string EggOnEndTheTimerEvent = "TimerUpHandler.EndTheTimer";
    public static string EggOnSetTimeUpEvent = "TimerUpHandler.SetTimeUp";
    public static string EggOnSetTimeDownStartEvent = "TimerDownHandler.SetTimeDown";
    public static string EggOnSetTimeDownResumeEvent = "TimerDownHandler.ResumeTimeDown";
    public static string EggOnTimerEndedEvent = "TimerHandler.OnTimerEnded";    
    public static string EggOnCameraScrollingEvent = "CameraScroller.ScrollingStatus";
    public static string EggTouchToJumpLayerOn = "TouchToJumpLayer.ToggleOn";
    public static string EggTouchToJumpLayerOff = "TouchToJumpLayer.ToggleOff";
    public static string EggGameStopNestMovement = "EggGameManager.StopNestMovement";
    public static string EggGameStartNestMovement = "EggGameManager.StartNestMovement";
    #endregion

    #region Train Game Event Stings
    public static string TrainOnScoreAndRoundUpEvent = "ScoreUpHandler.UpdateScoreRoundUI";    
    #endregion

    #region LevelUpPopup Event Strings
    public static string LevelUpPopupReplayGame = "LevelUpPopup.ReplayGame";
    public static string LevelUpPopupNextLevel = "LevelUpPopup.NextLevel";
    #endregion

    #region YouWonPopup Event Strings
    public static string YouWonPopupCollectCoins = "YouWonPopup.CollectCoins";
    #endregion

    #region YouLosePopup Event Strings
    public static string YouLosePopupReplayGame = "YouLosePopup.ReplayTheGame";
    public static string YouLosePopupResumeGame = "YouLosePopup.ResumeTheGame";
    #endregion

    #region App Level Event Strings
    public static string YesQuitTheGameEvent = "QuitTheGame.YES";
    public static string NoQuitTheGameEvent = "QuitTheGame.NO";
    #endregion 
}