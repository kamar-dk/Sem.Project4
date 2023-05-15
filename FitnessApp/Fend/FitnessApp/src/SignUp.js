import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Login.css";
import {
  TextField,
  Button,
  Container,
  Typography,
} from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";


const useStyles = makeStyles((theme) => ({
    form: {
      display: "flex",
      flexDirection: "column",
      alignItems: "center",
      justifyContent: "center",
      marginTop: theme.spacing(3),
    },
    input: {
      color: "black",
      "&::placeholder": {
        color: "black",
      },
      "&:focus": {
        color: "black",
        borderColor: "#050505",
      },
      margin: theme.spacing(1),
      width: "100%",
    },
    button: {
      margin: theme.spacing(3, 0, 2),
    },
  }));

export default function SignUp() {
  const classes = useStyles();

  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [age, setAge] = useState("");
  const [height, setHeight] = useState("");
  const [weight, setWeight] = useState("");

  const handleSignUp = (event) => {
    event.preventDefault();
    const payload = {
      username: username,
      email: email,
      password: password,
      age: age,
      height: height,
      weight: weight,
    };
    // Send the payload to the server to sign up the user
    fetch("https://localhost:7181/api/Account/SignUp", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(payload),
    })
      .then((response) => response.json())
      .then((data) => {
        // Handle successful sign-up, e.g. display a success message
        console.log(data);
        navigate("/login");
      })
      .catch((error) => console.error(error));
  };

  return (
    <div className="pic-wrapper">
    <Container maxWidth="sm"  >
      <Typography variant="h3" align="center" gutterBottom>
        Sign Up
      </Typography>
      <form className={classes.form} onSubmit={handleSignUp}>
        <TextField
          label="Name"
          variant="outlined"
          className={classes.input}
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <TextField
          label="Email"
          type="email"
          variant="outlined"
          className={classes.input}
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <TextField
          label="Password"
          type="password"
          variant="outlined"
          className={classes.input}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <TextField
          label="Age"
          type="number"
          variant="outlined"
          className={classes.input}
          value={age}
          onChange={(e) => setAge(e.target.value)}
        />
        <TextField
          label="Height (cm)"
          type="number"
          variant="outlined"
          className={classes.input}
          value={height}
          onChange={(e) => setHeight(e.target.value)}
        />
        <TextField
          label="Weight (kg)"
          type="number"
          variant="outlined"
          className={classes.input}
          value={weight}
          onChange={(e) => setWeight(e.target.value)}
        />
        <Button
          variant="contained"
          color="primary"
          type="submit"
          className={classes.button}
        >
          Sign Up
        </Button>
      </form>
    </Container>
    </div>
  );
}
