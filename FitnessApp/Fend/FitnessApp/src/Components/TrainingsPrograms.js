import { React, useState } from 'react';
import { useEffect } from 'react';
import Dropdown from './Dropdown';
import { Grid, Button, Typography, Card, CardContent  , CardActions, IconButton} from "@material-ui/core";
import { Favorite, FavoriteBorder } from "@material-ui/icons";

function TrainingProgram() {
  const [data, setData] = useState([]);
  const [id, setId] = useState([])
  const [selectedPrograms, setSelectedPrograms] = useState([]);

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
    setSelectedPrograms((prevSelectedPrograms) => {
      if (prevSelectedPrograms.includes(programId)) {
        return prevSelectedPrograms.filter((id) => id !== programId);
      } else {
        return [...prevSelectedPrograms, programId];
      }
    });
  }

  // useEffect(() => {
  //     fetchData();
  // }, [id]);

  return (
    <div className="gradient-background2" justifyContent="center" alignItems="center" >

      <Grid ms={12} style={{ padding: 10 }}>
        <Typography variant="h3" component="h3" style={{ color: 'white', align: 'center' }}>TrainingProgram</Typography>
        <Grid container justifyContent="center" alignItems="center">
          <Button variant="contained" color="primary" onClick={fetchData} >
            Get Programs
          </Button>
        </Grid>
        
      </Grid>
      <Grid container spacing={3}>
          {data.map(item => (
            <Grid item xs={12} sm={6} md={4} key={item.trainingProgramID}>
              <Card>
                <CardContent>
                  <Typography variant="h5" component="h2">
                    {item.name}
                  </Typography>
                  {/* Add more content or details about the training program */}
                </CardContent>
                <CardActions>
                  <IconButton
                    onClick={() => handleToggleFavorite(item.trainingProgramID)}
                    color={selectedPrograms.includes(item.trainingProgramID) ? "secondary" : "default"}
                  >
                    {selectedPrograms.includes(item.trainingProgramID) ? <Favorite /> : <FavoriteBorder />}
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
