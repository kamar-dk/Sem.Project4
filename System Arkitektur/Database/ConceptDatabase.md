```mermaid
erDiagram

User {

}

TraningsData {
    
}

UserData {

}

FavoriteTraningsPrograms {
    
}

Server {

}

TraningsPrograms {

}

Diet {

}

User ||--|| TraningsData : has
User ||--|{ UserData : contains
User ||--|| FavoriteTraningsPrograms : has
FavoriteTraningsPrograms }|--|{ TraningsPrograms : has
Server ||--o{ TraningsPrograms : has
Server ||--o{ User : has
Server ||--o{ Diet : has

```