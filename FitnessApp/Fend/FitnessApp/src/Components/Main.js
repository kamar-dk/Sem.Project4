import React from "react";
import { Parallax } from "react-parallax";
import "../App.css";
import { useTrail, animated } from 'react-spring';



function Main() {
  return (
    <div style={{ height: "100vh", position: "relative" }}>
      <Parallax bgImage="/images/1.jpg" strength={5}>
        <div
          style={{
            height: "100vh",
            display: "flex",
            flexDirection: "column",
            justifyContent: "center",
            alignItems: "center",
            color: "white",
            textShadow: "2px 2px 4px #000000",
          }}
        >
          <h1>Move your body, free your mind.</h1>
          <h2>Get Fit Today</h2>
          <button className="button">Sign Up/LogIn</button>
        </div>
        
      </Parallax>
      <Parallax bgImage="/images/caleneder.gif" strength={2} style={{ backgroundSize: "100%" }}>
        <div
          style={{
            height: "100vh",
            display: "flex",
            flexDirection: "column",
            justifyContent: "center",
            alignItems: "center",
            color: "white",
            textShadow: "2px 2px 4px #000000",
          }}
        >
          
          <h1>Why Our FitnessApp?</h1>
          
              <h3 >Add you daily/Weekly sessions</h3>
              <h3>Keep track of all Your activities</h3>

        </div>
        
      </Parallax>
      <Parallax bgImage="/Images/3.jpg" strength={2} >
        <div
          style={{
            height: "100vh",
            display: "flex",
            flexDirection: "column",
            
            justifyContent: "center",
            alignItems: "center",
            color: "white",
            textShadow: "2px 2px 4px #000000",
            
          }}
        >
          
          <h2>Diet and TrainingsPrograms</h2>
          <h3>Follow you calory intake, and wieght loss</h3>
          <h3>Add specific trainging programs</h3>
          
             
        </div>
        
      </Parallax>
      <Parallax bgImage="/images/2.jpg" strength={2} style={{ backgroundSize: "100%" }}>
        <div
          style={{
            height: "100vh",
            display: "flex",
            flexDirection: "column",
            justifyContent: "center",
            alignItems: "center",
            color: "white",
            textShadow: "2px 2px 4px #000000",
          }}
        >
          
          <h2>About Us</h2>
          <p>Our team, consisting of students from the 4th semester in SoftwareTechnology, is working with React, EfCore, and other backend Stuff. </p>
        </div>
        <div className="overlay"></div>
      </Parallax>
    </div>
  );
}

export default Main;


