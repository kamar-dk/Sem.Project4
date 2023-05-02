import React from "react";
import { useState } from "react";
import "../App.css";
import { Grid, Paper, Typography } from "@material-ui/core";

function Calories() {
  const [result, setResult] = useState(null);

  function calculateCalories(e) {
    e.preventDefault();
    const gender = e.target.gender.value;
    const weight = e.target.weight.value;
    const height = e.target.height.value;
    const age = e.target.age.value;
    const activity = e.target.activity.value;

    // apply the formula to calculate the daily calorie needs
    let calories;
    if (gender === "male") {
      calories = 88.36 + 13.4 * weight + 4.8 * height - 5.7 * age;
    } else {
      calories = 447.6 + 9.2 * weight + 3.1 * height - 4.3 * age;
    }
    calories *= activity;

    setResult(calories.toFixed(0));
  }

  return (
    <div style={{ padding: 20 }}>
      <Grid container spacing={2}>
        {/* Left Container */}
        <Grid item xs={12} md={6}>
          <Paper style={{ padding: 20 }}>
            {/* Add your calculator form here */}
            <div className="left-Container">
              <h1>Calorie Calculator</h1>
              <form onSubmit={calculateCalories}>
                <label htmlFor="gender">Gender:</label>
                <select id="gender" name="gender">
                  <option value="male">Male</option>
                  <option value="female">Female</option>
                </select>
                <br />
                <label htmlFor="weight">Weight (kg):</label>
                <input id="weight" name="weight" type="number" />
                <br />
                <label htmlFor="height">Height (cm):</label>
                <input id="height" name="height" type="number" />
                <br />
                <label htmlFor="age">Age:</label>
                <input id="age" name="age" type="number" />
                <br />
                <label htmlFor="activity">Activity level:</label>
                <select id="activity" name="activity">
                  <option value="1.2">Sedentary (little or no exercise)</option>
                  <option value="1.375">
                    Lightly active (light exercise or sports 1-3 days a week)
                  </option>
                  <option value="1.55">
                    Moderately active (moderate exercise or sports 3-5 days a
                    week)
                  </option>
                  <option value="1.725">
                    Very active (hard exercise or sports 6-7 days a week)
                  </option>
                  <option value="1.9">
                    Super active (very hard exercise or sports, physical job or
                    training twice a day)
                  </option>
                </select>
                <br />
                <button type="submit">Calculate</button>
              </form>
              {result && <p>Your daily calorie needs are: {result} calories</p>}
            </div>
          </Paper>
        </Grid>

        {/* Right Container */}
        <Grid item xs={12} md={6}>
          <Paper
            style={{
              padding: 20,
              maxHeight: "70vh",
              overflow: "auto",
              display: "flex",
              alignItems: "center",
            }}
          >
            <div className="right-Container">
              <Typography variant="h4">Understand Calories</Typography>
              <Typography variant="body1" style={{ whiteSpace: "pre-line" }}>
                {/* Add your general information about calories here */}
                Calories are a unit of measurement used to quantify the amount
                of energy that is obtained from food or expended through
                physical activity. The more calories a food contains, the more
                energy it provides to the body. However, consuming too many
                calories without burning them off through physical activity can
                lead to weight gain and other health problems. The number of
                calories a person needs each day depends on various factors such
                as age, gender, height, weight, and activity level. In general,
                the average adult needs around 2000-2500 calories per day to
                maintain their weight. However, this can vary depending on
                individual circumstances. To lose weight, a person must consume
                fewer calories than their body burns through physical activity
                and daily functions. On the other hand, to gain weight, a person
                must consume more calories than their body burns. It's important
                to note that not all calories are created equal. The source of
                calories (such as from whole foods versus processed foods) can
                impact their effect on the body. It's also important to focus on
                a balanced diet that includes a variety of nutrients, rather
                than solely counting calories.
              </Typography>
            </div>
          </Paper>
        </Grid>
      </Grid>
    </div>
  );
}

// need to be adjusted to be added to the database
function PutJobs(event) {
  event.preventDefault();
  console.log(event.target[0].value);
  console.log(new Date(event.target[1].value).toISOString());
  console.log(event.target[2].value);
  console.log(event.target[3].value);
  console.log(event.target[4].value);

  const payload = {
    customer: event.target[0].value,
    startDate: new Date(event.target[1].value).toISOString(),
    days: parseInt(event.target[2].value),
    location: event.target[3].value,
    comments: event.target[4].value,
  };
  fetch("https://localhost:7181/api/Jobs", {
    method: "POST",
    headers: {
      Accept: "application/json",
      Authorization: "Bearer " + localStorage.getItem("token"),
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  })
    .then((res) => res.json())
    .catch((error) => alert("Something bad happened: " + error));
}

function MyForm() {
  const [customer, setCustomer] = useState("");
  const [startDate, setStartDate] = useState(new Date());
  const [days, setDays] = useState("");
  const [location, setLocation] = useState("");
  const [comments, setComments] = useState("");

  return (
    <form onSubmit={PutJobs}>
      <label>
        Enter Customer:
        <input
          type="text"
          value={customer}
          onChange={(e) => setCustomer(e.target.value)}
        />
      </label>
      <br></br>
      <label>
        Enter Startdate:
        <input
          type={Date}
          value={startDate}
          onChange={(e) => setStartDate(e.target.value)}
        />
        <br></br>
      </label>
      <label>
        Enter Days:
        <input
          type="text"
          value={days}
          onChange={(e) => setDays(e.target.value)}
        />
      </label>
      <br></br>
      <label>
        Enter Location:
        <input
          type="text"
          value={location}
          onChange={(e) => setLocation(e.target.value)}
        />
      </label>
      <br></br>
      <label>
        Enter comments:
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
  );
}
export default Calories;
