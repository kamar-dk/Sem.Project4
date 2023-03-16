```mermaid
---
title: UC2 State Diagram
---
stateDiagram-v2
    [*] --> Menu
    Menu --> Settings
    Settings --> ConfirmBox
    ConfirmBox --> LoggingOut : Yes
    ConfirmBox --> Settings : No
    LoggingOut --> Home
    Home --> [*]
```