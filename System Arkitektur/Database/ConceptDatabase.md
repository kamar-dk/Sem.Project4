```mermaid
---
title: FitnessApp Concept Er-diagram
---
erDiagram

User {

}

TraningData {
    
}

UserData {

}

FavoriteTraningPrograms {
    
}

Server {

}

TraningPrograms {

}

Diet {

}

User ||--|| TraningData : has
User ||--|{ UserData : contains
User ||--|| FavoriteTraningPrograms : has
FavoriteTraningPrograms }|--|{ TraningPrograms : has
Server ||--o{ TraningPrograms : has
Server ||--o{ User : has
Server ||--o{ Diet : has

```