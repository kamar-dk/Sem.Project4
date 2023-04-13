import React from 'react';
import { useState } from 'react';

function Calories(param){
    return(
        <React.Fragment>
                  <div> Tilføj job</div> 
                  <br></br>  
                <MyForm></MyForm>
                <br></br>    
        </React.Fragment>
    )
}

function PutJobs(event){
    event.preventDefault()
    console.log(event.target[0].value)
    console.log(new Date(event.target[1].value).toISOString())
    console.log(event.target[2].value)
    console.log(event.target[3].value)
    console.log(event.target[4].value)
    
    const  payload = {
        "customer": event.target[0].value,
        "startDate": new Date(event.target[1].value).toISOString(),
        "days": parseInt(event.target[2].value),
        "location": event.target[3].value,
        "comments": event.target[4].value
    }
    fetch('https://localhost:7181/api/Jobs', {
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
    const [customer, setCustomer] = useState("");
    const [startDate, setStartDate] = useState(new Date());
    const [days, setDays] = useState("");
    const [location, setLocation] = useState("");
    const [comments, setComments] = useState("");

    return (
      <form onSubmit={PutJobs}>
        <label>Enter Customer:
          <input
            type="text" 
            value={customer}
            onChange={(e) => setCustomer(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter Startdate:
          <input
            type={Date}
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
          />
        <br></br>
        </label>
        <label>Enter Days:
          <input
            type="text" 
            value={days}
            onChange={(e) => setDays(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter Location:
          <input
            type="text" 
            value={location}
            onChange={(e) => setLocation(e.target.value)}
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

        <div>
          <button type="submit">Submit</button>
        </div>
      </form>
    )
}
export default Calories;