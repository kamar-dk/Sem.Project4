import React from 'react';
import { useState, useEffect } from 'react';
import { Parallax } from 'react-parallax';
import "../App.css";
import {
  Grid, Paper, Typography, Button,
  Select, FormControl, InputLabel, Box, TextField
} from "@material-ui/core";
import { lightBlue } from "@material-ui/core/colors";
import { OutlinedInput } from "@material-ui/core";



function User() {
  // useEffect(() => {
  //   fetchData();
  // }, []);

  // const fetchData = () => {
  //   var url = "https://localhost:7221/api/Userdata";
  //   return fetch(url, {
  //     method: 'GET',
  //     mode: 'cors',
  //     headers: {
  //       'Authorization': 'Bearer ' + localStorage.getItem("token"),
  //       'Content-Type': 'application/json'
  //     }
  //   })
  //     .then((response) => response.json())
  //     .then((data) => setUserData(data));
  // }
  const [userData, setUserData] = useState([
    {
      id: 1,
      name: "John Doe",
      email: "john.doe@example.com",
      age: 30,
      weight: 70,
      height: 175,
    },
  ]);
  const deleteUser = () => {
    // Implement your delete user functionality here
    alert("Delete User");
    const userId = User.id; // Assuming you have a user ID available

    fetch(`https://localhost:7221/api/User/${userId}`, {
      method: 'DELETE',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem("token"),
        'Content-Type': 'application/json'
      }
    })
      .then(response => {
        if (response.ok) {
          // User deleted successfully
          alert("User deleted");
          // Perform any additional actions if needed
        } else {
          // Failed to delete user
          alert("Failed to delete user");
        }
      })
      .catch(error => {
        console.error(error);
        alert("An error occurred while deleting user");
      });
  };

  const handleInputChange = (event, index, field) => {
    const updatedUserData = [...userData];
    updatedUserData[index][field] = event.target.value;
    setUserData(updatedUserData);
  };

  const saveUserData = (index) => {
    const updatedUser = userData[index];

    fetch(`https://localhost:7221/api/User/${updatedUser.id}`, {
      method: 'PUT',
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem("token"),
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(updatedUser)
    })
      .then(response => {
        if (response.ok) {
          alert("User data saved");
          // Perform any additional actions if needed
        } else {
          alert("Failed to save user data");
        }
      })
      .catch(error => {
        console.error(error);
        alert("An error occurred while saving user data");
      });
  };


  return (
    <div className="gradient-background" style={{
    
    }}>

      <Grid container spacing={2}  >
        {/* Left Container */}
        <Grid item xs={12} md={6}>
          <Paper style={{ padding: 20 }}>
            <Box display="flex" flexDirection="column" alignItems="center" marginBottom={4} >
              <h1 align="center" style={{ backgroundColor: "lightblue" }}>
                UserData (to be deleted)
              </h1>
              {userData.map((user, index) => (
                <div key={user.id} style={{ display: "flex", flexDirection: "column" }}>
                  <TextField
                    label="Name"
                    value={user.name}
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    onChange={(e) => handleInputChange(e, index, 'name')}
                  />
                  <TextField
                    label="Email"
                    value={user.email}
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    onChange={(e) => handleInputChange(e, index, 'email')}
                  />
                  <TextField
                    label="Age"
                    value={user.age}
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    onChange={(e) => handleInputChange(e, index, 'age')}
                  />
                  <TextField
                    label="Weight"
                    value={user.weight}
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    onChange={(e) => handleInputChange(e, index, 'weight')}
                  />
                  <TextField
                    label="Height"
                    value={user.height}
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    onChange={(e) => handleInputChange(e, index, 'height')}
                  />
                  {/* Render other user data fields */}
                </div>
              ))}
            </Box>
            <Button variant="contained" color="secondary" onClick={deleteUser}>
              Delete User
            </Button>
            <Button variant="contained" color="primary" onClick={saveUserData}>
              Save/Update
            </Button>
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
            }}
          >
            <div className="right-Container">
              <h1 align="center" style={{ backgroundColor: "lightblue" }}>Favorite Trainings programs</h1>

            </div>

          </Paper>


        </Grid>

        <Grid item xs={12} md={12}>
          <Paper style={{ padding: 20 }}>
            <Box display="flex" flexDirection="row" alignItems="center" marginBottom={4} >
              <h1 align="center" gutterBottom style={{ width: "100", backgroundColor: "lightblue" }}>
                Last Activity:
              </h1>

            </Box>
          </Paper>
        </Grid>
      </Grid>

    </div>
  );

}


export default User