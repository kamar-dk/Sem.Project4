import React, { useEffect, useState, useRef } from "react";
import "../App.css";
import { useTrail, animated } from 'react-spring';
import { Parallax } from "react-parallax";
import { Calendar } from "react-calendar";

function Tracking(){
    const [data, setData] = useState([]);
    const [id, setId] = useState([])
    const fetchData = () => {
        var url = "https://localhost:7181/api/Jobs";
        return fetch(url, {
            method: 'GET',
            credentials: 'include',
            headers: {
              'Authorization': 'Bearer ' + localStorage.getItem("token"),
              'Content-Type': 'application/json'
            }
            })
              .then((response) => response.json())
              .then((data) => setData(data));
        }
    

    useEffect(() => {
        fetchData();
    }, [id]);
    return(
<div style={{ height: '100vh', position: 'relative' }}>
  <Parallax bgImage="/images/Bike.jpg" strength={2} style={{ backgroundSize: "100%" }}>
    <div
      style={{
        height: '100vh',
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'normal',
        alignItems: 'left',
        color: 'white',
        textShadow: '2px 2px 4px #000000',
      }}
    >
      <Calendar
      />
    </div>
  </Parallax>
      <Parallax bgImage="/images/Running.jpg" strength={2} style={{ backgroundSize: "100%" }}>
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
              <div>
      <h1>Calorie Counter</h1>
      <CalorieCounter />
      <button>Previous Week</button>
      <button>Next Week</button>
    </div>
        </div>
        
      </Parallax>
      <Parallax bgImage="/Images/Swimming.jpg" strength={2} >
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
        </div>
      </Parallax>
    </div>
  );

}

function CalorieCounter() {
  const [caloriesBurned, setCaloriesBurned] = useState([1000, 500, 200, 600, 700, 150, 0]); //dummydata
  const canvasRef = useRef(null); // Reference to the canvas element

  // Function to draw the bar chart
  const drawChart = () => {
    const canvas = canvasRef.current;
    const ctx = canvas.getContext('2d');
    const chartWidth = canvas.width;
    const chartHeight = canvas.height;
    const barWidth = chartWidth / caloriesBurned.length;
    let x = 0;

    // Clear the canvas
    ctx.clearRect(0, 0, chartWidth, chartHeight);

    // Draw the bars for each day
    caloriesBurned.forEach((calories, index) => {
      const barHeight = calories / 10;
      const y = chartHeight - barHeight;

      ctx.fillStyle = '#2196f3';
      ctx.fillRect(x, y, barWidth, barHeight);

      // Draw the label for the bar
      ctx.fillStyle = '#000';
      ctx.font = '12px Arial';
      ctx.textAlign = 'center';
      ctx.fillText(` ${index + 1}`, x + barWidth / 2, chartHeight - 5);

      x += barWidth;
    });
  };

  useEffect(() => {
    drawChart();
  }, [caloriesBurned]);

  return (
    <div>
      <canvas ref={canvasRef} width={600} height={400} />
    </div>
  );
}

export default Tracking;