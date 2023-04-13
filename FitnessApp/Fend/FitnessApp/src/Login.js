
import React, { Component, useState } from 'react';
import {useNavigate, redirect} from 'react-router-dom';
import './Login.css';

export default function Login() {


  return(

    <div className="login-wrapper">
      <h1>Please Log In</h1>
      <form onSubmit={Sendlogin}>
        <label>
          <p>Username</p>
          <input type="text"  id="Email"/> 
        </label>
        <label>
          <p>Password</p>
          <input type="password" id="Pass"/>
        </label>
        <div>
          <button type="submit">Submit</button>
        </div>
      </form>
    </div>
  )
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
