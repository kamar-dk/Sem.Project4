import React from 'react';
import { useState } from 'react';
function Activity(){
    return(
        <React.Fragment>
                  <div> Tilføj Model </div> 
                  <br></br>  
                <MyForm></MyForm>
                <br></br>    
        </React.Fragment>
    )
}

function PutModel(event){
    event.preventDefault()
    console.log(event.target[0].value)
    console.log(event.target[1].value) 
    const  payload = {
        "firstName": event.target[0].value,
        "lastName": event.target[1].value,
        "email": event.target[2].value,
        "phoneNo": event.target[3].value,
        "addresLine1": event.target[4].value,
        "addresLine2": event.target[5].value,
        "zip": event.target[6].value,
        "city": event.target[7].value,
        "country": event.target[8].value,
        "birthDate": new Date(event.target[9].value).toISOString(),
        "nationality": event.target[10].value,
        "height": event.target[11].value,
        "shoeSize": event.target[12].value,
        "hairColor": event.target[13].value,
        "eyeColor": event.target[14].value,
        "comments": event.target[15].value,
        "password": event.target[16].value,
    }
    fetch('https://localhost:7181/api/Models', {
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
    const [phoneNo, setPhoneNo] = useState("");
    const [addressLine1, setAddressLine1] = useState("");
    const [addressLine2, setAddressLine2] = useState("");
    const [zip, setZip] = useState("");
    const [city, setCity] = useState("");
    const [country, setCountry] = useState("");
    const [birthday, setBirthday] = useState(new Date());
    const [nationality, setNationality] = useState("");
    const [height, setHeight] = useState("");
    const [shoeSize, setShoeSize] = useState("");
    const [hairColor, setHairColor] = useState("");
    const [eyeColor, setEyeColor] = useState("");
    const [comments, setComments] = useState("");
    const [password, setPassword] = useState("");

    return (
      <form onSubmit={PutModel}>
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
        <label>Enter phone:
          <input
            type="text" 
            value={phoneNo}
            onChange={(e) => setPhoneNo(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter addressLine1:
          <input
            type="text" 
            value={addressLine1}
            onChange={(e) => setAddressLine1(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter addressLine2:
          <input
            type="text" 
            value={addressLine2}
            onChange={(e) => setAddressLine2(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter zip:
          <input
            type="text" 
            value={zip}
            onChange={(e) => setZip(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter city:
          <input
            type="text" 
            value={city}
            onChange={(e) => setCity(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter country:
          <input
            type="text" 
            value={country}
            onChange={(e) => setCountry(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter birthday:
          <input
            type={Date} 
            value={birthday}
            onChange={(e) => setBirthday(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter nationality:
          <input
            type="text" 
            value={nationality}
            onChange={(e) => setNationality(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter height:
          <input
            type="text" 
            value={height}
            onChange={(e) => setHeight(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter shoeSize:
          <input
            type="text" 
            value={shoeSize}
            onChange={(e) => setShoeSize(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter hairColor:
          <input
            type="text" 
            value={hairColor}
            onChange={(e) => setHairColor(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter eyeColor:
          <input
            type="text" 
            value={eyeColor}
            onChange={(e) => setEyeColor(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter comments:
          <input
            type="text" 
            value={comments}
            onChange={(e) => setComments(e.target.value)}
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
export default Activity;