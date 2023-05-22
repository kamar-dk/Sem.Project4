import { React, useState } from 'react';
import { useEffect } from 'react';
import Dropdown from './Dropdown';
import { Grid, Button, Typography, Card, CardContent, CardActions, IconButton } from "@material-ui/core";
import { Favorite, FavoriteBorder } from "@material-ui/icons";

function TrainingProgram() {
  const [data, setData] = useState([]);
  //const [programId, setId] = useState([]);
  const [selectedProgram, setSelectedProgram] = useState([]);

  // useEffect(() => {
  //   fetchData();
  // }, []);

  const fetchData = () => {
    var url = "https://localhost:7221/api/TraningPrograms";
    return fetch(url, {
      method: 'GET',
      mode: 'cors',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem("token"),
        'Content-Type': 'application/json'
      }
    })
      .then((response) => response.json())
      .then((data) => setData(data));
  }
  const handleToggleFavorite = (programId) => {
    setSelectedProgram(prevSelectedPrograms => {
      if (prevSelectedPrograms.includes(programId)) {
        return prevSelectedPrograms.filter(id => id !== programId);
      } else {
        return [...prevSelectedPrograms, programId];
      }
    });
  };


  // Send POST request to the API endpoint
  const postData = () => {

    const email = localStorage.getItem('email');
    const payload = {

      email: email,
      favoriteTraningProgramsID : 0,
      traningProgramID: 6,

    };
    fetch('https://localhost:7221/api/FavoriteTraningPrograms', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(payload )
    })
      .then((response) => response.json())
      .then((data) => {
        console.log('Response:', data);
      })
      .catch((error) => {
        console.log('Error:', error);
      });
  };

  useEffect(() => {
    if (selectedProgram.length > 0) {
      postData();
    }
  }, [selectedProgram]);

  return (
    <div className="gradient-background2" >

      <Grid ms={6} style={{ padding: 10 }}>
        <Typography variant="h3" component="h3" style={{ color: 'white', align: 'center' }}>TrainingProgram</Typography>
        <Grid container justifyContent="center" alignItems="center">
          <Button variant="contained" color="primary" onClick={fetchData} >
            Get Programs
          </Button>
        </Grid>

      </Grid>
      <Grid container spacing={3}>
        {data.map(item => (
          
          <Grid item xs={10} sm={6} md={4} key={item.traningProgramID}>
            <Card>
              <CardContent>
                <Typography variant="h5" component="h2">
                  {item.name}
                </Typography>
                {/* Add more content or details about the training program */}
              </CardContent>
              <CardActions>
                <IconButton
                  onClick={() => handleToggleFavorite(item.traningProgramID)}
                  color={selectedProgram.includes(item.traningProgramID) ? "secondary" : "default"}
                >
                  {selectedProgram.includes(item.traningProgramID) ? <Favorite /> : <FavoriteBorder />}
                </IconButton>
              </CardActions>
            </Card>
          </Grid>
        ))}
      </Grid>

    </div>

  );
}




export default TrainingProgram;
