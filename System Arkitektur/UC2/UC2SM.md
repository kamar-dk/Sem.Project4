```mermaid
---
title: UC2 State Diagram
---
stateDiagram-v2
    [*] --> Menu : User opens menu
    Menu --> Settings : User clicks on "Indstillinger"
    Settings --> "SletBruger" : User clicks on "slet bruger"
    "SletBruger" --> ConfirmBox : Confirm box pops up
    ConfirmBox --> LoggingOut : Clicks Yes 
    ConfirmBox --> Settings : clicks No
    LoggingOut --> FrontPage : Takes user to front page
    FrontPage --> [*]
```