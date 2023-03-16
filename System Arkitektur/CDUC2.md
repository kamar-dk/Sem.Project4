```mermaid
---
title: CD UC2
---
classDiagram
    class UI {
        +void ShowDeleteUserWarning()
    }

    class FitnessApp{
        +void OpenMenu()
        +void RedirectPage(pagename)
        +void DeleteUser(UserEmail)
        -void LogUserOut(UserEmail)
    }
        
    class Database{
        +void RemoveUserProfile(UserEmail)
    }
```