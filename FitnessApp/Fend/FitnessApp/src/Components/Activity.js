import React from 'react';
import { useState } from 'react';
import Dropdown from './Dropdown';
function Activity(){
    return(
        <React.Fragment>
                  <div> Add Activity </div> 
                  <br></br>  
                <MyForm></MyForm>
                <br></br>    
        </React.Fragment>
    )
}

function PutActivity(event){
    event.preventDefault()
    console.log(event.target[0].value)
    console.log(event.target[1].value) 
    const  payload = {
        "Activity": event.target[0].value,
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

const options = [
    { value: "1", label: "Running" },
    { value: "2", label: "Swimming" },
    { value: "3", label: "Cycling" },
    { value: "4", label: "Walking" },
    { value: "5", label: "Hiking" },
    { value: "6", label: "Yoga" },
    { value: "7", label: "Pilates" },
    { value: "8", label: "Dancing" },
    { value: "9", label: "Weight Lifting" },
    { value: "10", label: "Crossfit" },
    { value: "11", label: "Martial Arts" },
    { value: "12", label: "Boxing" },
    { value: "13", label: "Tennis" },
    { value: "14", label: "Soccer" },
    { value: "15", label: "Basketball" },
];

function MyForm() {
    const [Activity, setActivity] = useState("");


    return (
      <form onSubmit={PutActivity}>
        <label>Enter Activity:
           <Dropdown placeHolder="Select..." 
           options={options} 
           value={Activity}
           onChange={(e) => setActivity(e.target.value)}
           />
        </label>
        <br></br>
        <label>Enter Activity:
          <input
            type="text" 
            value={Activity}
            onChange={(e) => setActivity(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter Duration:

        </label>
        <div>
          <button type="submit">Submit</button>
        </div>
      </form>
    )
}
export default Activity;