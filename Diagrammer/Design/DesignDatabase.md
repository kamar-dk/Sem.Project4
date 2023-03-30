```mermaid
erDiagram

User {
    string Email PK
    string Name
    string Lastname
    string Password
}

TraningData {
    string Email PK, FK
}

UserData {
    string Email PK, FK
    float weight
    float height
    string gender
    datetime dob
}

FavoriteTraningPrograms {
    string Email PK, FK
}

Server {


}

TraningPrograms {
    int TraningProgramID PK
}

Diet {

}

User ||--|| TraningData : has
User ||--|{ UserData : has
User ||--|| FavoriteTraningPrograms : has
FavoriteTraningPrograms }|--|{ TraningPrograms : has
Server ||--o{ TraningPrograms : has
Server ||--o{ User : has
Server ||--o{ Diet : has

```