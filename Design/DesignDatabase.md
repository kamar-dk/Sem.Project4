```mermaid
erDiagram

User {
    string Email PK
    string name
    string lastname
    string password
}

TraningData {
    string Email PK, FK
}

UserData {
    string Email PK, FK
    float weight
    float height
    string gende
    datetime dob
}

FavoriteTraningPrograms {
    string Email PK, FK
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