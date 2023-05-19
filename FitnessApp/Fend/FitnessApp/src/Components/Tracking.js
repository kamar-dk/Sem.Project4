import React, { useEffect, useState } from "react";
import { useTrail, animated } from "react-spring";
import { Parallax } from "react-parallax";
import { Calendar } from "react-calendar";
import { Grid, Paper, Typography } from "@material-ui/core";

function Tracking() {
  const [date, setDate] = useState(new Date());
  const [data, setData] = useState([]);
  const [calories, setCalories] = useState(0);
  const percentage = calories / 2000 * 100;
 const cappedPercentage = percentage > 100 ? 100 : percentage;

 useEffect(() => {
  fetchData();
}, [date]);

  const fetchData = async() => {
    console.log("FetchData called");
    const url = "https://localhost:7221/api/TraningDatas";
    return fetch(url, {
      method: "GET",
      mode: "cors",
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => { setData(data)

        const targetDate = date // replace with date from calender.
        targetDate.setHours(0,0,0,0); // removes timestamp from calender date if there is one.

        

        const filtered = data.filter((data) =>{
          if(data.sessionDate){
          const dataDate = new Date(data.sessionDate.substring(0,10)); // year/month/day.
          dataDate.setHours(0,0,0,0); // removes timestamp from data (we should remove from database tbh).


          return dataDate.getTime() === targetDate.getTime();
          }
          return false;
        })



        const Sum = filtered.reduce((totalCalories, item) => {  //takes sessions, and sums the calorie values.
          if(item.calories) {
            return totalCalories + item.calories;
          } else {
            return totalCalories;
         }
      },0);
      setCalories(Sum);
    });
  };
  const onClickDay = (date) => {
    setDate(date);
  };
  return (
    <div className="gradient-background">
      <React.Fragment>
        <div style={{ padding: 20 }}>
          <Grid container spacing={1}>
            <Grid item xs={2} md={8}>
              <Calendar onChange={onClickDay} value={date} />
            </Grid>
            <Grid item xs={2} md={8}>
              <Paper style={{ padding: 10 }}>
                <div className="left-Container">
                  <h1 style={{ backgroundColor: "lightblue" }}>Sessions Selected</h1>
                  <p>
                    BikeSessions:{" "}
                    {

                    }
                  </p>
                  <p>
                    RunningSessions:{" "}
                    {
                      <div className="text-center">Selected date: {date.toDateString()} </div>
                    }
                  </p>
                  <div>
                      {data.length > 0 && (
                       <ul>
                         {data.map(data => (
                        <li key={data.id}>{data.sessionDate}</li>
                          ))}
                         </ul>
                      )}
                    </div>
                </div>
              </Paper>
            </Grid>
          </Grid>
        </div>
      </React.Fragment>
      <div>
        <div style={{ padding: 120, textAlign: "center"}}>
        <h1> {date.toDateString()}</h1>
        <div style={{ width: 75, height: 300, padding: 100, display: "flex", flexDirection: "column-reverse", alignItems: "center" }}>
          <div
            style={{
              height: `${cappedPercentage}%`,
              width: "100%",
              backgroundColor: "green",
              marginLeft: "20px",
            }}
          />
        </div>
        <p>Calories: {calories}</p>
      </div>
    </div>
    </div>
  );
};
export default Tracking;