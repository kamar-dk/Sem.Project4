import React from 'react';
import { useState } from 'react';
function TilføjModelTilJob(){
    return(
        <React.Fragment>
                  <div> Tilføj en model til et job </div> 
                  <br></br>  
                <MyForm></MyForm>
                <br></br>    
        </React.Fragment>
    )
}

function PutModelToJob(event){
    event.preventDefault()
    console.log(event.target[0].value)
    console.log(event.target[1].value)

    const  payload = {
        "jobId": event.target[0].value,
        "modelId": event.target[1].value,
    }
    var url = "https://localhost:7181/api/jobs/" + event.target[0].value + "/model/" + event.target[1].value;
    fetch(url, {
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
    const [jobId, setJobId] = useState("");
    const [modelId, setModelId] = useState("");

    return (
      <form onSubmit={PutModelToJob}>
        <label>Enter jobId:
          <input
            type="text" 
            value={jobId}
            onChange={(e) => setJobId(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter modelId:
          <input
            type="text"
            value={modelId}
            onChange={(e) => setModelId(e.target.value)}
          />
        </label>
        <div>
          <button type="submit">Submit</button>
        </div>
      </form>
    )
}
export default TilføjModelTilJob;