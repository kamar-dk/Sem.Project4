```mermaid
erDiagram

User {
    string Email PK
    string name
    string lastname
    string password
}

TraningsData {
    string Email PK, FK
}

UserData {
    string Email PK, FK
    float weight
    float height
    string gende
    datetime dob
}

FavoriteTraningsPrograms {
    string Email PK, FK
}

Server {

}

TraningsPrograms {

}

Diet {

}

User ||--|| TraningsData : has
User }|--|| UserData : contains
User ||--|| FavoriteTraningsPrograms : has
FavoriteTraningsPrograms }|--|{ TraningsPrograms : has
Server }o--|| TraningsPrograms : has
Server }o--|| User : has
Server }o--|| Diet : has

```