import React, { Component, useState } from 'react';
import {useNavigate, redirect, Navigate, NavLink, useNavigation} from 'react-router-dom';
import { Link } from 'react-router-dom';
import './Login.css';
import {TextField,Button,Container,Typography,} from "@material-ui/core";
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
  

export default function Login() {
  const classes = useStyles();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');



  return(
    <div className="gradient-background">
    <Container maxWidth="sm" style={{backgroundColor: "white"}} >
    <Typography variant="h3" align="center" color='white'> 
      Please Log In
    </Typography>

    <form className={classes.form} onSubmit={Sendlogin}>
      <TextField
        id="email"
        label="Email"
        variant="outlined"
        value={email}
        onChange={(event) => setEmail(event.target.value)}
      />
      <TextField
        id="password"
        label="Password"
        variant="outlined"
        type="password"
        value={password}
        onChange={(event) => setPassword(event.target.value)}
      />
      <Button
        type="submit"
        variant="contained"
        color="primary"
        className={classes.submit}
      >
        Login
      </Button>
    </form>
    <p align ="center">
      Don't have an account? <Link to="/SignUp">SignUp</Link>
    </p>
    </Container>
  </div>
  
);
}
function Sendlogin(event) {
   event.preventDefault()
    console.log(event.target[0].value)
    console.log(event.target[1].value)
    const  payload = {
      email: event.target[0].value,
      password: event.target[1].value
    }
    if (payload.email === "" || payload.password === "") {
      alert("Please fill out all fields");
      return;
    }

  // fetch('https://localhost:7221/api/Users/login', {
  //   method: 'POST',
  //   headers: {
  //     'Accept': 'application/json',
  //     'Content-Type': 'application/json',
  //   },
  //   body: JSON.stringify(payload)
  // })
  //   .then(res => res.json())
  //   .then((token) => {
  //     console.log(token.jwt);
  //     localStorage.setItem("token", token.jwt);
  //     localStorage.setItem("email", payload.email);
  //     localStorage.setItem("user", "user");

  //     window.location.href = "/User";
  //   })
  //   .catch(error => {
  //     console.log(error);
  //   });

  fetch('https://localhost:7221/api/Users/login', {
  method: 'POST',
  headers: {
    'Accept': 'application/json',
    'Content-Type': 'application/json',
  },
  body: JSON.stringify(payload)
})
  .then(res => {
    if (res.ok) {
      return res.json();
    } else {
      throw  alert('Login failed. Forkerte Brugerinformationer'); // Or handle the error in an appropriate way
    }
  })
  .then((token) => {
    localStorage.setItem("email", payload.email);
    localStorage.setItem("user", "user");

    window.location.href = "/User";
  })
  .catch(error => {
    console.log(error);
  });
}

