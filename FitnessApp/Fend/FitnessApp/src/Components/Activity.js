import React from 'react';
import { useState } from 'react';
import Dropdown from './Dropdown';
import { Grid, Paper, Typography } from "@material-ui/core";
function Activity(){
    return(
        <React.Fragment>
     <div style={{ padding: 20 }}>
      <Grid container spacing={2}>
        {/* Left Container */}
        <Grid item xs={12} md={6}>
          <Paper style={{ padding: 20 }}>
            <div className="left-Container">
              <h1 style={{backgroundColor: "lightblue"}}>Add Activity</h1>
                <ActivityForm></ActivityForm>
                <br/>
            </div>
          </Paper>
        </Grid>
        {/* Right Container */}
        <Grid item xs={12} md={6}>
          <Paper
            style={{
              padding: 50,
              maxHeight: "100vh",
              width: "50vh",
              overflow: "auto",
              display: "flex",
              alignItems: "center",
            }}
          >
            <div className="right-Container">
              <Typography variant="h4">Understand Programs and Adding Activities</Typography>
              <Typography variant="body1" style={{ whiteSpace: "pre-line" }}>
                {/* General information about Training programs and activity*/}
                he he hæ hæ hæ hø hø hø
              </Typography>
            </div>
          </Paper>          
        </Grid>
      </Grid>
    </div> 
        </React.Fragment>
    )
}

function PutActivity(event){
    event.preventDefault()
    console.log(event.target[0].value)
    console.log(event.target[1].value) 
    const  payload = {
        "Activity": event.target[0].value,
        "Duration": event.target[1].value,
        "Distance": event.target[2].value,
        "MaxHeartRate": event.target[3].value,
        "AvgHeartRate": event.target[4].value,
        "birthDate": new Date(event.target[5].value).toISOString(),
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
];

function ActivityForm() {
    const [Activity, setActivity] = useState("");
    const [Duration, setDuration] = useState("");
    const [Distance, setDistance] = useState("");
    const [MaxHeartRate, setMaxHeartRate] = useState("");
    const [AvgHeartRate, setAvgHeartRate] = useState("");

    return (
      <form onSubmit={PutActivity}>
        <label>Enter Activity:
           <Dropdown placeHolder="Select..." 
           options={options} 
           value={Activity}
           onChange={(e) => setActivity(e.target.value)}
           />
        </label>
        <label>Enter Duration:
          <input
            type="text" 
            value={Duration}
            onChange={(e) => setDuration(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter Distance:
          <input
            type="text"
            value={Distance}
            onChange={(e) => setDistance(e.target.value)}
          />
        </label>
        <br></br>
        <label>Max Heart Rate:
        <input
            type="text"
            value={MaxHeartRate}
            onChange={(e) => setMaxHeartRate(e.target.value)}
          />
        </label>
        <br></br>
        <label>Average Heart Rate:
        <input
            type="text"
            value={AvgHeartRate}
            onChange={(e) => setAvgHeartRate(e.target.value)}
          />
        </label>
        <br></br>

        <label>Enter Date:
          <input
            type="date"
            value={new Date().toISOString()}
          />
        </label>
        <br></br>
        <div>
          <button type="submit">Add</button>
        </div>
      </form>
    )
}


export default Activity;