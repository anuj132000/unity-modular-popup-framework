using System.Collections.Generic;
using UnityEngine;

public static class PopupsQueueManager {

    #region Data Work
    public enum PopupId {   //List of all popups Id within the app.
        NoPopup,
        SettingsPopup,
        QuitGamePopup,
        LevelUpPopup,
        YouWonPopup,
        YouLosePopup,
        GamePausePopup
    }
    public enum PopupGroup {    //List of all popups Groups within the app.
        NONE,
        ADVERTISE,
        INAPP,
        GAME
    };

    struct QueueData {  //Data structure that will hold PopupId and PopupGroup.
        public PopupId popupId;
        public PopupGroup popupGroup;

        public QueueData(PopupId pId, PopupGroup pGroup) {
            popupId = pId;
            popupGroup = pGroup;
        }
    };

    static List<QueueData> popupsQueue = new List<QueueData>(); //List if type QueueData structure.
    #endregion

    #region Public Static Methods
    //Any popup within the app must call this method before making itself visible.
    public static void AddMyPopupId(PopupId popupId, PopupGroup popupGroup = PopupGroup.NONE) {
        QueueData data = new QueueData(popupId, popupGroup);
        popupsQueue.Add(data);
        if (popupGroup == PopupGroup.NONE) {
            EventManager.triggerEvent(AppEventsId.PopupsQueueManagerPopupAdded, null, popupId.ToString());
        } else {
            EventManager.triggerEvent(AppEventsId.PopupsQueueManagerGroupAdded, null, popupGroup.ToString());
        }
        PrintQueue("Just ADDED+ PopupId...: ");
    }

    //Any popup within the app must call this method before making itself invisible.
    public static void RemoveMyPopupId(PopupId popupId) {
        PrintQueue("Just REMOVED- PopupId...: ");
        bool matchFound = DoYouHavePopupId(popupId, out int idx);        
        if(matchFound) {
            popupsQueue.RemoveAt(idx);
            EventManager.triggerEvent(AppEventsId.PopupsQueueManagerPopupRemoved, null, popupId.ToString());
        }
        if (!DoYouHaveAnyPopup()) {
            EventManager.triggerEvent(AppEventsId.PopupsQueueManagerQueueIsEmpty, null, typeof(PopupsQueueManager).ToString());
        }        
    }

    //Any popup within the app must call this method before making itself visible.
    public static void RemoveMyPopupGroup(PopupGroup popupGroup) {
        PrintQueue("Just REMOVED- Popup Group...: ");
        bool matchFound;
        int count = 0;
        do {
            matchFound = DoYouHavePopupGroup(popupGroup, out int idx);
            if(matchFound) {
                popupsQueue.RemoveAt(idx);
                count++;
            }
        } while(matchFound);        
        if (count > 0) {
            EventManager.triggerEvent(AppEventsId.PopupsQueueManagerGroupRemoved, null, popupGroup.ToString());
        }
        if (DoYouHaveAnyPopup()) {
            EventManager.triggerEvent(AppEventsId.PopupsQueueManagerQueueIsEmpty, null, typeof(PopupsQueueManager).ToString());
        }        
    }

    public static void AddBridgePopupGroup(PopupGroup popupGroup) {
        AddMyPopupId(PopupId.NoPopup, popupGroup);
    }

    public static void RemoveBridgePopupGroup(PopupGroup popupGroup) {
        PrintQueue("Just REMOVED- Bridge Popup Group...: ");
        int idx = popupsQueue.FindIndex(0, data => data.popupId.Equals(PopupId.NoPopup) && data.popupGroup.Equals(popupGroup));
        if(idx >= 0) {
            popupsQueue.RemoveAt(idx);
        }                
    }

    //Call this method to check any specific popup inside the queue using popupId.
    public static bool DoYouHavePopupId(PopupId popupId) {
        return popupsQueue.FindIndex(data => data.popupId.Equals(popupId)) >= 0;
    }

    //Call this method to check any specific popup GROUP inside the queue using popupGroup.
    public static bool DoYouHavePopupGroup(PopupGroup popupGroup) {
        return popupsQueue.FindIndex(0, data => data.popupGroup.Equals(popupGroup)) >= 0;
    }

    //Call this method to check, is any popup or group of popup present in the queue.
    public static bool DoYouHaveAnyPopup() {
        return popupsQueue.Count > 0;
    }
    #endregion

    #region Privae Methods
    private static bool DoYouHavePopupId(PopupId popupId, out int idx) {
        idx = popupsQueue.FindIndex(data => data.popupId.Equals(popupId));
        return idx >= 0;
    }

    private static bool DoYouHavePopupGroup(PopupGroup popupGroup, out int idx) {
        idx = popupsQueue.FindIndex(0, data => data.popupGroup.Equals(popupGroup));
        return idx >= 0;
    }

    private static void PrintQueue(string source) {
        foreach(QueueData data in popupsQueue) {
            source += data.popupId + "(GROUP : " + data.popupGroup + "), ";
        }
        Debug.Log("PopupQueueManager.<b><color=purple>" + source + "</color></b>");
    }
    #endregion
}