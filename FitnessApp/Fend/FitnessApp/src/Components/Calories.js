import React from "react";
import { useState } from "react";
import "../App.css";
import {
  Grid, Paper, Typography, Button,
  Select, FormControl, InputLabel, Box
} from "@material-ui/core";
import { lightBlue } from "@material-ui/core/colors";
import { OutlinedInput } from "@material-ui/core";

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
    <div className="gradient-background">
      <Grid container spacing={2}  >
        {/* Left Container */}
        <Grid item xs={12} md={6}>
          <Paper style={{ padding: 20 }} >


            <Box display="flex" flexDirection="column" alignItems="center" marginBottom={4}>
              <h1 align="center" style={{ backgroundColor: "lightblue" }}>Calorie Calculator</h1>
              <form onSubmit={calculateCalories} style={{ display: 'flex', flexDirection: 'column' }} >
                <FormControl variant="outlined" style={{ marginBottom: 20, width: '100%' }}>
                  <InputLabel>Gender</InputLabel>
                  <Select native label="Gender" inputProps={{ name: 'gender' }}>
                    <option value="male">Male</option>
                    <option value="female">Female</option>
                  </Select>
                </FormControl>
                <FormControl variant="outlined" >
                  <InputLabel>Weight (kg)</InputLabel>
                  <OutlinedInput type="number" name="weight" label="Weight (kg)" />
                </FormControl>
                <FormControl variant="outlined" style={{ marginBottom: 20, width: '100%' }}>
                  <InputLabel>Height (cm)</InputLabel>
                  <OutlinedInput type="number" name="height" label="Height (cm)" />
                </FormControl>
                <FormControl variant="outlined" style={{ marginBottom: 20 }}>
                  <InputLabel>Age</InputLabel>
                  <OutlinedInput type="number" name="age" label="Age" />
                </FormControl>
                <FormControl variant="outlined" style={{ marginBottom: 20 }}>
                  <InputLabel>Activity Level</InputLabel>
                  <Select native label="Activity Level" inputProps={{ name: 'activity' }}>
                    <option value="1.2">Sedentary (little or no exercise)</option>
                    <option value="1.375">Lightly active (light exercise or sports 1-3 days a week)</option>
                    <option value="1.55">Moderately active (moderate exercise or sports 3-5 days a week)</option>
                    <option value="1.725">Very active (hard exercise or sports 6-7 days a week)</option>
                    <option value="1.9">Super active (very hard exercise or sports, physical job or training twice a day)</option>
                  </Select>
                </FormControl>
                <Button variant="contained" color="primary" type="submit" style={{ marginBottom: 20 }}>Calculate</Button>
              </form>
              {result && (
                <Typography variant="h5" align="center" >
                  Your daily calorie needs are: <strong>{result} calories</strong>
                </Typography>
              )}
            </Box>


          </Paper>
        </Grid>

        {/* Right Container */}
        <Grid item xs={12} md={6}>
          <Paper
            style={{
              padding: 50,
              maxHeight: "100vh",
              width: "80vh",
              overflow: "auto",
              display: "flex",
              alignItems: "center",
              backgroundSize: "cover",
              backgroundRepeat: "no-repeat",

            }}
          >
            <div className="right-Container">
              <h1 align="center" style={{ backgroundColor: "lightblue" }}>Understand Calories</h1>
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
// function GetSessionsCalories(event) {
//   event.preventDefault();
//   console.log(event.target[0].value);
//   console.log(new Date(event.target[1].value).toISOString());
//   console.log(event.target[2].value);
//   console.log(event.target[3].value);
//   console.log(event.target[4].value);

//   const payload = {
//     customer: event.target[0].value,
//     startDate: new Date(event.target[1].value).toISOString(),
//     days: parseInt(event.target[2].value),
//     location: event.target[3].value,
//     comments: event.target[4].value,
//   };
//   fetch("https://localhost:7181/api/Traningsdata", {
//     method: "POST",
//     headers: {
//       Accept: "application/json",
//       Authorization: "Bearer " + localStorage.getItem("token"),
//       "Content-Type": "application/json",
//     },
//     body: JSON.stringify(payload),
//   })
//     .then((res) => res.json())
//     .catch((error) => alert("Something bad happened: " + error));
// }

// function MyForm() {
//   const [customer, setCustomer] = useState("");
//   const [startDate, setStartDate] = useState(new Date());
//   const [days, setDays] = useState("");
//   const [location, setLocation] = useState("");
//   const [comments, setComments] = useState("");

//   return (
//     <form onSubmit={PutJobs}>
//       <label>
//         Enter Customer:
//         <input
//           type="text"
//           value={customer}
//           onChange={(e) => setCustomer(e.target.value)}
//         />
//       </label>
//       <br></br>
//       <label>
//         Enter Startdate:
//         <input
//           type={Date}
//           value={startDate}
//           onChange={(e) => setStartDate(e.target.value)}
//         />
//         <br></br>
//       </label>
//       <label>
//         Enter Days:
//         <input
//           type="text"
//           value={days}
//           onChange={(e) => setDays(e.target.value)}
//         />
//       </label>
//       <br></br>
//       <label>
//         Enter Location:
//         <input
//           type="text"
//           value={location}
//           onChange={(e) => setLocation(e.target.value)}
//         />
//       </label>
//       <br></br>
//       <label>
//         Enter comments:
//         <input
//           type="text"
//           value={comments}
//           onChange={(e) => setComments(e.target.value)}
//         />
//       </label>

//       <div>
//         <button type="submit">Submit</button>
//       </div>
//     </form>
//   );
// }

export default Calories;
