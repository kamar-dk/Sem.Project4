import { React, useState } from 'react';
import { useEffect } from 'react';
import Dropdown from './Dropdown';

function TrainingProgram(){
const [data, setData] = useState([]);
    const [id, setId] = useState([])
    
    const fetchData = () => {
        var url = "https://localhost:7221/api/TraningPrograms";
        return fetch(url, {
            method: 'GET',
            mode: 'cors',
            headers: {
              'Authorization': 'Bearer ' + localStorage.getItem("token"),
              'Content-Type': 'application/json'
            }
            })
              .then((response) => response.json())
              .then((data) => setData(data));
        }
    

    useEffect(() => {
        fetchData();
    }, [id]);

    return(
      <div>
      <h1>TrainingProgram</h1>
      <ul>
        {data.map(item => (
          <li key={item.trainingProgramID}>{item.name}</li>
        ))}
      </ul>
    </div>

      );
                }
// function PutProgram(event){
//   event.preventDefault()
//   console.log(event.target[0].value)
//   console.log(event.target[1].value) 
//   const  payload = {
//       "Activity": event.target[0].value,
//       "Duration": event.target[1].value,
//       "Distance": event.target[2].value,
//       "MaxHeartRate": event.target[3].value,
//       "AvgHeartRate": event.target[4].value,
//       "birthDate": new Date(event.target[5].value).toISOString(),
//   }
//   fetch('https://localhost:7181/api/Models', {
//   method: 'POST', 
//   headers: {
//       'Accept': 'application/json',
//       'Authorization': 'Bearer ' + localStorage.getItem("token"),
//       'Content-Type': 'application/json',
//   },
//   body: JSON.stringify(payload)
//   })
//   .then(res => res.json())
//   .catch(error => alert('Something bad happened: ' + error));
// }

// const Programs = [
//   { value: "1", label: "Running" },
//   { value: "2", label: "Sprint/Intervals" },
//   { value: "3", label: "Marathon" },
//   { value: "4", label: "Swimming" },
//   { value: "5", label: "Swim Intervals" },
//   { value: "6", label: "swimming Long Distance" },
//   { value: "7", label: "Cycling" },
//   { value: "8", label: "Cycling Mountains" },
//   { value: "9", label: "Cycling Long distance" },
//   { value: "10", label: "Cycling Sprint" },
// ];

// // function TrainingProgramForm(){
// //   const [Program, setProgram] = useState("");

// //     return (
// //       <form onSubmit={PutProgram}>
// //         <form>Select Program:
// //            <Dropdown placeHolder="Select..." 
// //            options={Programs} 
// //            value={Program}
// //            onChange={(e) => setProgram(e.target.value)}
// //            />
// //            </form>
// //         </form>
// //     )
// // }

// // function TrainingProgram(){
// //   return (
// //     <>
// //       <h1>Training Programs</h1>
// //       <div>
// //         <TrainingProgramForm></TrainingProgramForm>
// //       </div>
// //     </>
// //   );
// // };

export default TrainingProgram;
