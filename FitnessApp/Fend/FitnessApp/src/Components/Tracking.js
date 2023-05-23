import React, { useEffect, useState } from "react";
import { useTrail, animated } from "react-spring";
import { Parallax } from "react-parallax";
import { Calendar } from "react-calendar";
import { Grid, Paper, Typography } from "@material-ui/core";



function Tracking() {
  const [date, setDate] = useState(new Date());
  const [data, setData] = useState([]);
  const [filtereddata, setFiltered] = useState([])
  const [calories, setCalories] = useState(0);
  const [userData, setUserData] = useState({});
  const percentage = calories / 2000 * 100;
 const cappedPercentage = percentage > 100 ? 100 : percentage;

 useEffect(() => {
  fetchData();
}, [date]);

  const fetchData = async() => {
    console.log("FetchData called");
   const email = localStorage.getItem("email"); //gets the user email for a comparison later
    const url = `https://localhost:7221/api/TraningDatas`;
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

          //console.log(email); //was for testing comparisons
          //console.log(data.userId); // same as above
          return dataDate.getTime() === targetDate.getTime() && email === data.userId; //compares dates and emails, returns the ones that are both true.
          }
          return false;
        })

        setFiltered(filtered); // setting it to use later

       


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
  const arrange = () => {
    const arranged = [...filtereddata].sort((a,b) => {
      const dateA = new Date(a.sessionDate);
      const dateB = new Date(b.sessionDate);
      return dateA.getTime() - dateB.getTime();
    });

    return arranged;
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
              <Paper style={{ padding: 10, maxHeight:"300px", overflow:"auto" }}>
                <div className="left-Container">
                  <h1 style={{ backgroundColor: "lightblue" }}>Sessions:</h1>
                  <p>
                    {arrange().map((data) => (
                      <div key={data.id} className="text-center">
                        {data.trainingType} Session <br />
                        Date: {data.sessionDate} <br />
                        Distance: {data.distance}m <br />
                        Session Time: {data.sessionHourTime}:{data.sessionMinuteTime}:{data.sessionSecondTime} <br />
                        Calories Burned: {data.calories} <br />
                        Min/Max/Avg Heart rate: {data.minHeartRate}-{data.maxHeartRate}/{data.avgHeartRate} <br />
                        VO2 max: {data.vo2Max} mL/kg/min <br /> <br />
                      </div>
                    ))}
                  </p>
                </div>
              </Paper>
            </Grid>
          </Grid>
        </div>
      </React.Fragment>
      <div>
        <div style={{ padding: 120, textAlign: "center", color: "white"}}>
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