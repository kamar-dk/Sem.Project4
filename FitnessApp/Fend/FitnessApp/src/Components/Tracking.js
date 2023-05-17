import React, { useEffect, useState } from "react";
import { useTrail, animated } from "react-spring";
import { Parallax } from "react-parallax";
import { Calendar } from "react-calendar";
import { Grid, Paper, Typography } from "@material-ui/core";

function Tracking() {
  const [date, setDate] = useState(new Date());
  const [calories, setCalories] = useState(2000);
  const percentage = calories / 2000 * 100;
 const cappedPercentage = percentage > 100 ? 100 : percentage;

  const fetchCalories = () => {
    var url = "https://localhost:7221/api/TraningDatas";
    return fetch(url, {
      method: "GET",
      credentials: "include",
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => setCalories(data.calories));
  };

  useEffect(() => {
    fetchCalories();
  }, [date]);

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
                      <div className="text-center">Selected date: {date.toDateString()} </div>
                    }
                  </p>
                  <p>
                    RunningSessions:{" "}
                    {
                      <div className="text-center">Selected date: {date.toDateString()} </div>
                    }
                  </p>
                </div>
              </Paper>
            </Grid>
          </Grid>
        </div>
      </React.Fragment>
      <div>
        <div style={{ padding: 100, textAlign: "center"}}>
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
}

export default Tracking;