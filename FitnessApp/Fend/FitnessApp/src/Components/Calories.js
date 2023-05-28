import React from "react";
import { useState } from "react";
import "../App.css";
import {
  Grid, Paper, Typography, Button,
  Select, FormControl, InputLabel, Box, Card, CardMedia, CardContent
} from "@material-ui/core";
import { lightBlue } from "@material-ui/core/colors";
import { OutlinedInput } from "@material-ui/core";
import bananaImage from "./calories/banana.jpg";
import tomatoImage from "./calories/tomato.jpg";
import celeryImage from "./calories/celery.jpg";
import broccoliImage from "./calories/broccoli.jpg";
import riceImage from "./calories/Rice.jpg";
import meatImage from "./calories/meat.jpg";
import chickenImage from "./calories/chicken.jpg";


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
              width: "80%",
              height: "76%",
              overflow: "auto",
            }}
          >
            <h1 align="center" style={{ backgroundColor: "lightblue", marginTop: "0" }}>
              Understand Calories
            </h1>
            <Box display="flex" justifyContent="center" flexWrap="wrap">
              {/* ...adding code... */}
              <div style={{ display: "flex", alignItems: "center", margin: 10 }}>
                <Card style={{ maxWidth: 120, maxHeight: 60 }}>
                  <CardMedia component="img" style={{ maxHeight: 50 }} image={bananaImage} alt="Banana" />
                </Card>
                <Typography variant="body2" color="textSecondary" component="p" style={{ marginLeft: 10 }}>
                  100 grams of banana: 89 calories
                </Typography>
              </div>
              <div style={{ display: "flex", alignItems: "center", margin: 10 }}>
                <Card style={{ maxWidth: 120, maxHeight: 60 }}>
                  <CardMedia component="img" style={{ maxHeight: 50 }} image={tomatoImage} alt="Tomato" />
                </Card>
                <Typography variant="body2" color="textSecondary" component="p" style={{ marginLeft: 10 }}>
                  100 grams of tomatoes: 18 calories
                </Typography>
              </div>
              <div style={{ display: "flex", alignItems: "center", margin: 10 }}>
                <Card style={{ maxWidth: 120, maxHeight: 60 }}>
                  <CardMedia component="img" style={{ maxHeight: 50 }} image={broccoliImage} alt="Broccoli" />
                </Card>
                <Typography variant="body2" color="textSecondary" component="p" style={{ marginLeft: 10 }}>
                  100 grams of broccoli: 34 calories
                </Typography>
              </div>
              <div style={{ display: "flex", alignItems: "center", margin: 10 }}>
                <Card style={{ maxWidth: 120, maxHeight: 60 }}>
                  <CardMedia component="img" style={{ maxHeight: 50 }} image={celeryImage} alt="Celery" />
                </Card>
                <Typography variant="body2" color="textSecondary" component="p" style={{ marginLeft: 10 }}>
                  100 grams of celery: 16 calories
                </Typography>
              </div>
              <div style={{ display: "flex", alignItems: "center", margin: 10 }}>
                <Card style={{ maxWidth: 120, maxHeight: 60 }}>
                  <CardMedia component="img" style={{ maxHeight: 50 }} image={riceImage} alt="Rice" />
                </Card>
                <Typography variant="body2" color="textSecondary" component="p" style={{ marginLeft: 10 }}>
                  100 grams of rice: 130 calories
                </Typography>
              </div>
              <div style={{ display: "flex", alignItems: "center", margin: 10 }}>
                <Card style={{ maxWidth: 120, maxHeight: 60 }}>
                  <CardMedia component="img" style={{ maxHeight: 50 }} image={meatImage} alt="Meat" />
                </Card>
                <Typography variant="body2" color="textSecondary" component="p" style={{ marginLeft: 10 }}>
                  100 grams of beef: 250 calories
                </Typography>
              </div>
              <div style={{ display: "flex", alignItems: "center", margin: 10 }}>
                <Card style={{ maxWidth: 120, maxHeight: 60 }}>
                  <CardMedia component="img" style={{ maxHeight: 50 }} image={chickenImage} alt="chicken" />
                </Card>
                <Typography variant="body2" color="textSecondary" component="p" style={{ marginLeft: 10 }}>
                  100 grams of chicken breast: 165 calories
                </Typography>
              </div>

            </Box>
          </Paper>
        </Grid>

      </Grid>
    </div>
  );
}



export default Calories;
