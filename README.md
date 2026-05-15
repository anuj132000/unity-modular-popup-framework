

\# Unity Modular Popup Framework



Production-ready modular popup framework for Unity games with queue-based popup handling, reusable transitions, scalable architecture, and mobile-friendly UI workflows.



\---



\# Demo Preview



\## Popup Transition

!\[Popup Transition](Demo/Popups\_3.gif)



\## Featured Popup

!\[Featured Popup](Demo/LevelWinPopup.gif)



\---



\# Overview



The Modular Popup Framework is a reusable and scalable popup management system designed for Unity mobile and web games.



\---



\# Features



\- Queue-based popup management

\- Reusable popup lifecycle system

\- Modular popup architecture

\- Decoupled popup spawning workflow

\- Popup transition support

\- Background input blocking

\- Mobile-friendly popup handling

\- Extensible popup flow system

\- Runtime/Demo separation

\- Scalable popup workflow design

\- separation of responsibilities



\---



\# Architecture Overview



Game / UI Events

&#x20;       ↓

PopupManager

&#x20;       ↓

PopupQueueManager

&#x20;       ↓

PopupSpawner

&#x20;       ↓

BasePopup

&#x20;       ↓

PopupAnimation





\---



\# Core Components



\## PopupManager



Main entry point for popup handling.



Responsibilities:

\- handles popup requests

\- manages popup lifecycle

\- communicates with queue system

\- controls popup visibility flow



\---



\## PopupQueueManager



Responsible for popup sequencing.



Responsibilities:

\- prevents popup overlap

\- queues popup requests

\- ensures ordered popup display



\---



\## PopupSpawner



Responsible for popup creation and instantiation.



Responsibilities:

\- instantiate popup prefabs

\- configure popup parents

\- initialize popup data



\---



\## BasePopup



Base abstraction for all popup implementations.



Responsibilities:

\- open/close lifecycle

\- animation callbacks

\- visibility handling

\- interaction state

\- popup initialization



\---



\## TouchBlockerLayer



Prevents background interaction while popup is active.



Responsibilities:

\- block gameplay input

\- maintain popup focus

\- improve user experience



\---



\# Popup Lifecycle



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

Object Pool / Destroy



\---



\# Folder Structure



Runtime/

&#x20;├── Animation/

&#x20;├── Core/

&#x20;├── Input/

&#x20;├── Popup/

&#x20;└── Utilities/



Documentation/

&#x20;├── Architecture.md

&#x20;└── PopupLifecycle.md



Demo/

&#x20;├── Screenshots/

&#x20;└── GIFs/



\---



\# Screenshots



\## Level Win Popup

!\[Level Win Popup](Demo/level\_win\_popup.png)



\## Level Loose Popup

!\[Level Loose Popup](Demo/level\_loose\_popup\_landscape.png)

!\[Level Loose Popup](Demo/level\_loose\_popup\_portrait.png)



\## Settings Popup

!\[Settings Popup](Demo/settings\_popup\_landscape.png)

!\[Settings Popup](Demo/settings\_popup\_portrait.png)



\## Quit Popup

!\[Quit Popup](Demo/quit\_popup.png)



\---



\# Design Goals



The framework was designed to:

\- reduce popup implementation duplication

\- support scalable UI workflows

\- simplify popup integration

\- improve maintainability

\- separate popup responsibilities cleanly



\---



\# Documentation



Additional documentation:

\- \[Architecture](Documentation/Architecture.md)

\- \[Popup Lifecycle](Documentation/PopupLifecycle.md)



\---



\# Tech Stack



\- Unity

\- C#

\- DOTween

\- Mobile-first architecture



\---



\# Repository Goals



This repository focuses on showcasing:

\- scalable Unity architecture

\- reusable gameplay systems

\- production-oriented engineering workflows

\- maintainable popup management patterns

\- modular Unity framework design

