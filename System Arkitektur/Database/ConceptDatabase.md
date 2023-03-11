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

TraeningsPrograms {

}

Diet {

}

User ||--|| TraningsData : has
User }|--|| UserData : contains
User ||--|| FavoriteTraningsPrograms : has
FavoriteTraningsPrograms }|--|{ TraeningsPrograms : has
Server }o--|| TraeningsPrograms : has
Server }o--|| User : has
Server }o--|| Diet : has

```