import React from 'react';
import { useState } from 'react';
function TilføjManager(){
    return(
        <React.Fragment>
                  <div> Tilføj Manager </div> 
                  <br></br>  
                <MyForm></MyForm>
                <br></br>    
        </React.Fragment>
    )
}

function PutManager(event){
    event.preventDefault()
    console.log(event.target[0].value)
    console.log(event.target[1].value)

    const  payload = {
        "firstName": event.target[0].value,
        "lastName": event.target[1].value,
        "email": event.target[2].value,
        "password": event.target[3].value,
    }
    fetch('https://localhost:7181/api/Managers', {
    method: 'POST', 
    headers: {
        'Accept': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem("token"),
        'Content-Type': 'application/json',
    },
    body: JSON.stringify(payload)
    })
    .then(res => res.json())
    .catch(error => alert('Something bad happened: ' + error));
}

function MyForm() {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    return (
      <form onSubmit={PutManager}>
        <label>Enter firstname:
          <input
            type="text" 
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter lastname:
          <input
            type="text"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />
        <br></br>
        </label>
        <label>Enter email:
          <input
            type="text" 
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter password:
          <input
            type="text" 
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </label>
        <div>
          <button type="submit">Submit</button>
        </div>
      </form>
    )
}
export default TilføjManager;