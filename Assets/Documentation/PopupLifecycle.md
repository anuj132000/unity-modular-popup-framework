\# Popup Lifecycle



This document explains the lifecycle flow of popups inside the Modular Popup Framework.



\---



\# Lifecycle Flow



Request Popup

&#x20;     ↓

Queue Popup

&#x20;     ↓

Spawn Popup

&#x20;     ↓

Initialize Popup

&#x20;     ↓

Play Open Animation

&#x20;     ↓

Popup Active

&#x20;     ↓

User Interaction

&#x20;     ↓

Play Close Animation

&#x20;     ↓

Cleanup / Destroy



\---



\# Detailed Lifecycle

\- validate popup request

\- forward request to queue system

\- prevent popup overlap

\- maintain ordered popup flow

\- instantiate popup prefab if not exist

\- assign hierarchy parent

\- setup content

\- configure visuals

\- bind callbacks

\- prepare animation state

\- slide in

\- scale bounce

\- fade in

\- enable user interaction

\- block gameplay input

\- confirmation actions

\- close requests

\- fade out

\- slide out

\- scale down

\- cleanup callbacks

\- release references

\- notify queue manager



\---



\# Lifecycle Design Goals



The lifecycle architecture was designed to:

\- standardize popup behavior

\- reduce duplicated logic

\- simplify popup implementation

\- support scalable transitions

\- improve maintainability



\---





