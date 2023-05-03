
import React, { Component, useState } from 'react';
import {useNavigate, redirect, Navigate, NavLink, useNavigation} from 'react-router-dom';
import { Link } from 'react-router-dom';
import './Login.css';
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
    <Container
  maxWidth="sm">
    <div className="login-wrapper">
    <h1>Please Log In</h1>
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
        Submit
      </Button>
    </form>
    <p>
      Don't have an account? <Link to="/SignUp">SignUp</Link>
    </p>
  </div>
  </Container>
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
    

    fetch('https://localhost:7181/api/Account/Login', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
    },
    body: JSON.stringify(payload)
  })
  .then(res => res.json())
  .then((token)=> {
    console.log(token.jwt);
    localStorage.setItem("token", token.jwt);
    let RoleExtracted = parseToJwt(token.jwt);
    console.log(RoleExtracted);
    let role =
    RoleExtracted["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    localStorage.setItem("role", role);
    let ModelId = RoleExtracted["ModelId"];
    localStorage.setItem("ModelId", ModelId);
    window.location.href = "/main";
  },
  (error) => {
      console.log(error);
      
    }
  
  );
  
    
  
}

function parseToJwt(token) {
  var base64Url = token.split('.')[1];
  var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  var jsonPayload = decodeURIComponent(
    atob(base64)
      .split('')
      .map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
      })
      .join('')
  );
  
  return JSON.parse(window.atob(base64));
}

