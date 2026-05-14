\# Modular Popup Framework Architecture



\## Overview



The Modular Popup Framework is a reusable and scalable popup management system designed for Unity mobile and web games.



The framework focuses on:

\- modular popup architecture

\- popup queue handling

\- reusable transitions

\- popup lifecycle management

\- mobile-friendly workflows

\- separation of responsibilities



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

\- manages popup stacking rules



\---



\## PopupSpawner



Responsible for popup creation and instantiation.



Responsibilities:

\- instantiate popup prefabs

\- configure popup parents

\- initialize popup data

\- support reusable popup workflows



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





\# Design Goals



The framework was designed to:

\- reduce popup implementation duplication

\- support scalable UI workflows

\- simplify popup integration

\- support reusable popup architecture

\- improve maintainability

\- separate popup responsibilities cleanly



\---



\# Scalability Considerations



The framework architecture supports:

\- multiple popup types

\- popup queuing

\- reusable transitions

\- independent popup modules

\- future pooling support

\- asynchronous popup workflows



\---



