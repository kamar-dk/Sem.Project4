import React from 'react';
import { useState } from 'react';
function TilføjExpense(){
    return(
        <React.Fragment>
                  <div> Tilføj Udgift </div> 
                  <br></br>  
                <MyForm></MyForm>
                <br></br>    
        </React.Fragment>
    )
}

function PutExpense(event){
    event.preventDefault()
    console.log(event.target[0].value)
    console.log(event.target[1].value)

    const  payload = {
        "modelId": localStorage.getItem("ModelId"),
        "jobId": event.target[0].value,
        "date": new Date(event.target[1].value).toISOString(),
        "text": event.target[2].value,
        "amount": event.target[3].value,
    }
    fetch('https://localhost:7181/api/Expenses', {
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
    const [date, setDate] = useState(new Date());
    const [text, setText] = useState("");
    const [amount, setAmount] = useState("");

    return (
      <form onSubmit={PutExpense}>
        <label>Enter jobId:
          <input
            type="text" 
            value={jobId}
            onChange={(e) => setJobId(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter date:
          <input
            type={Date}
            value={date}
            onChange={(e) => setDate(e.target.value)}
          />
        <br></br>
        </label>
        <label>Enter text:
          <input
            type="text" 
            value={text}
            onChange={(e) => setText(e.target.value)}
          />
        </label>
        <br></br>
        <label>Enter amount:
          <input
            type="text" 
            value={amount}
            onChange={(e) => setAmount(e.target.value)}
          />
        </label>
        <br></br>
        <div>
          <button type="submit">Submit</button>
        </div>
      </form>
    )
}
export default TilføjExpense;