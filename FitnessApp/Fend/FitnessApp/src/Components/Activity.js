import React from 'react';
import { useState } from 'react';
import { Grid, Paper, Typography } from "@material-ui/core";
function Activity(){
    return(
        <React.Fragment>
     <div className="gradient-background">
      <Grid container spacing={2}>
        {/* Left Container */}
        <Grid item xs={12} md={6}>
          <Paper style={{ padding: 20 }}>
            <div className="left-Container">
              <h1 align="center" style={{backgroundColor: "lightblue"}}>Add Activity</h1>
              <ActivityForm
              />
                <br/>
            </div>
          </Paper>
        </Grid>
        {/* Right Container */}
        <Grid item xs={12} md={6}>
          <Paper
            style={{
              padding: 100,
              maxHeight: "100vh",
              width: "70vh",
              overflow: "auto",
              
            }}
          >
            <div className="right-Container">
              <h1  align="center" style={{backgroundColor: "lightblue"}}>How to add an activity</h1>
              <Typography variant="body1" style={{ whiteSpace: "pre-line" }}>
                {/* General information about Training programs and activity*/}
                Here you can log your activity done on any given day and then a combination of your activities under the tracking tab.
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
  var id = "0"
  var time = "T00:00:00.000Z"
    event.preventDefault()
    console.log(event.target[0].value)
    console.log(event.target[1].value + time) 
    console.log(event.target[2].value)
    console.log(event.target[3].value)
    console.log(event.target[4].value)
    console.log(event.target[5].value)
    console.log(event.target[6].value)
    console.log(event.target[7].value)
    console.log(event.target[8].value)
    console.log(event.target[9].value)
    console.log(event.target[10].value)
    const  payload = {
        "id": id,
        "userId" : localStorage.getItem("email"),
        "trainingType" : event.target[0].value,
        "sessionDate": event.target[1].value + time,
        "Distance": event.target[2].value,
        "sessionHourTime": event.target[3].value,
        "sessionMinuteTime": event.target[4].value,
        "sessionSecondTime": event.target[5].value,
        "calories": event.target[6].value,
        "MaxHeartRate": event.target[7].value,
        "MinHeartRate": event.target[8].value,
        "AvgHeartRate": event.target[9].value,
        "vo2Max": event.target[10].value,
    }
    fetch('https://localhost:7221/api/TraningDatas', {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(payload)
    })
    .then(res => res.json())
    .catch(error => alert('Something bad happened: ' + error));
}


function ActivityForm() {
    const [selectedActivity, setSelectedActivity] = useState("");
    const [date, setDate] = useState("");
    const [SessionHours, setSessionHours] = useState("");
    const [SessionMinutes, setSessionMinutes] = useState("");
    const [SessionSeconds, setSessionSeconds] = useState("");
    const [CaloriesBurned, setCaloriesBurned] = useState("");
    const [VO2Max, setVO2Max] = useState("");
    const [MinHeartRate, setMinHeartRate] = useState("");
    const [Distance, setDistance] = useState("");
    const [MaxHeartRate, setMaxHeartRate] = useState("");
    const [AvgHeartRate, setAvgHeartRate] = useState("");
    

    return (
      <div style={{ padding: 20 }}>
      <form onSubmit={PutActivity}>
        <label>Select Activity:
          <select value ={selectedActivity} onChange={(e) => setSelectedActivity(e.target.value)} >
           <option value="Running">Running</option>
           <option value="Cycling">Cycling</option>
           <option value="Swimming">Swimming</option>
           </select>
  </label>
        <br></br>
        <label>Enter Date:
          <input
            type="date"
            value={date}
            onChange={(e) => setDate(e.target.value)}
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
        <label>Session Hours:
        <input
            type="text"
            value={SessionHours}
            onChange={(e) => setSessionHours(e.target.value)}
          />
        </label>
        <br></br>
        <label>Session Minutes:
        <input
            type="text"
            value={SessionMinutes}
            onChange={(e) => setSessionMinutes(e.target.value)}
          />
        </label>
        <br></br>
        <label>Session Seconds:
        <input
            type="text"
            value={SessionSeconds}
            onChange={(e) => setSessionSeconds(e.target.value)}
          />
        </label>
        <br></br>
        <label>Calories Burned:
        <input
            type="text"
            value={CaloriesBurned}
            onChange={(e) => setCaloriesBurned(e.target.value)}
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
        <label>Min Heart Rate:
        <input
            type="text"
            value={MinHeartRate}
            onChange={(e) => setMinHeartRate(e.target.value)}
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

        <label>VO2 Max:
        <input
            type="text"
            value={VO2Max}
            onChange={(e) => setVO2Max(e.target.value)}
          />
        </label>
        <br></br>
        <div>
          <button type="submit">Add</button>
        </div>
      </form>
      </div>
    )
}


export default Activity;